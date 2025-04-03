using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BrabantApp : MonoBehaviour
{
    [Header("Test Data")]
    public User user;
    public Guardian guardian;
    public Patient patient;

    [Header("Dependencies")]
    public UserApiClient userApiClient;
    public PatientApiClient patientApiClient;
    public GuardianApiClient guardianApiClient;
    public ScreenLogic screenLogic;
    public AccountScreenLogic accountScreenLogic;

    [Header("Settings")]
    public bool GenerateDate;
    public bool requiresSurgery;
    [Header("Personal Info")]
    public TMP_Text personalInfo;


    void Start()
    {
        //if (GenerateDate && KeepAlive.Instance.StoredPatient == null)
        //{
        //    KeepAlive.Instance.StoredDoctor = TestdataGenerator.GenerateDoctor();
        //    KeepAlive.Instance.StoredGuardian = TestdataGenerator.GenerateGuardian();
        //    KeepAlive.Instance.StoredPatient = TestdataGenerator.GeneratePatient(
        //        KeepAlive.Instance.StoredGuardian.ID,
        //        KeepAlive.Instance.StoredDoctor.ID
        //    );

        //    Debug.Log($"Generated Doctor: {KeepAlive.Instance.StoredDoctor.Name}, Specialization: {KeepAlive.Instance.StoredDoctor.Specialization}, ID: {KeepAlive.Instance.StoredDoctor.ID}");
        //    Debug.Log($"Generated Guardian: {KeepAlive.Instance.StoredGuardian.FirstName} {KeepAlive.Instance.StoredGuardian.LastName}, ID: {KeepAlive.Instance.StoredGuardian.ID}");
        //    Debug.Log($"Generated Patient: {KeepAlive.Instance.StoredPatient.FirstName} {KeepAlive.Instance.StoredPatient.LastName}, GuardianID: {KeepAlive.Instance.StoredPatient.GuardianID}, DoctorID: {KeepAlive.Instance.StoredPatient.DoctorID}");

        //}
        FetchUserData();
    }




    public async void postData()
    {
        if (KeepAlive.Instance.UserToken != "")
        {
            IWebRequestReponse webRequestResponse = await patientApiClient.CreatePatient(KeepAlive.Instance.StoredPatient);

            switch (webRequestResponse)
            {
                case WebRequestData<Patient> dataResponse:
                    // TODO: Handle succes scenario.
                    break;
                case WebRequestError errorResponse:
                    string errorMessage = errorResponse.ErrorMessage;
                    Debug.Log("CreateGuardian patient error: " + errorMessage);
                    // TODO: Handle error scenario. Show the errormessage to the user.
                    break;
                default:
                    throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
            }

            webRequestResponse = await guardianApiClient.CreateGuardian(KeepAlive.Instance.StoredGuardian);

            switch (webRequestResponse)
            {
                case WebRequestData<Guardian> dataResponse:
                    // TODO: Handle succes scenario.
                    break;
                case WebRequestError errorResponse:
                    string errorMessage = errorResponse.ErrorMessage;
                    Debug.Log("CreateGuardian guardian error: " + errorMessage);
                    // TODO: Handle error scenario. Show the errormessage to the user.
                    break;
                default:
                    throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
            }
        }
    }

    public async void FetchUserData()
    {
        if (string.IsNullOrEmpty(KeepAlive.Instance.UserToken))
        {
            Debug.Log("User is not logged in. Skipping data fetch.");
            return;
        }

        Debug.Log("Fetching user data...");

        // Fetch patient data
        IWebRequestReponse patientResponse = await patientApiClient.GetAll();
        //patientResponse = ParsePatientResponse(patientResponse);  // Parse the response

        switch (patientResponse)
        {
            case WebRequestData<List<Patient>> dataResponse:
                List<Patient> patients = dataResponse.Data;
                Debug.Log("List of Patients: ");
                patients.ForEach(patient => Debug.Log(patient.id));
                KeepAlive.Instance.StoredPatients = patients;
                break;
            case WebRequestError errorResponse:
                string errorMessage = errorResponse.ErrorMessage;
                Debug.Log("Read patients error: " + errorMessage);
                // TODO: Handle error scenario. Show the errormessage to the user.
                break;
            default:
                throw new NotImplementedException("No implementation for webRequestResponse of class: " + patientResponse.GetType());
        }

        // Update UI after fetching data
        updateUI();
    }

    

    






    public void updateUI()
    {
        if (KeepAlive.Instance.StoredPatient == null)
            return;

        string patientInfo = $"Patient Name: {KeepAlive.Instance.StoredPatient.firstName} {KeepAlive.Instance.StoredPatient.lastName}\n";
        patientInfo += "Next Appointment: TBD";

        if (personalInfo != null)
        {
            personalInfo.text = patientInfo;
        }
        else
        {
            Debug.LogError("PersonalInfo TMP_Text component is not assigned.");
        }
    }

    #region Login

    [ContextMenu("User/Register")]
    public async void Register()
    {
        user = new User(accountScreenLogic.LoginFields[0].text, accountScreenLogic.LoginFields[1].text);


        IWebRequestReponse webRequestResponse = await userApiClient.Register(user);

        switch (webRequestResponse)
        {
            case WebRequestData<string> dataResponse:
                Debug.Log("Register succes!");

                IWebRequestReponse loginWebRequestResponse = await userApiClient.Login(user);

                switch (loginWebRequestResponse)
                {
                    case WebRequestData<string> loginDataResponse:
                        Debug.Log("Login succes!");

                        FetchUserData();
                        screenLogic.ShowHomeScreen();

                        // TODO: Todo handle succes scenario.
                        // TODO: doorverwijzen naar de tijdlijn.
                        break;
                    case WebRequestError errorResponse:
                        string loginErrorMessage = errorResponse.ErrorMessage;
                        Debug.Log("Login error: " + loginErrorMessage);
                        // TODO: Handle error scenario. Show the errormessage to the user.
                        //messageText.text = "Login error: " + loginErrorMessage;
                        break;
                    default:
                        throw new NotImplementedException("No implementation for loginWebRequestResponse of class: " + loginWebRequestResponse.GetType());
                }
                // TODO: Handle succes scenario;
                //messageText.text = "Register succes!";
                break;
            case WebRequestError errorResponse:
                string errorMessage = errorResponse.ErrorMessage;
                Debug.Log("Register error: " + errorMessage);
                // TODO: Handle error scenario. Show the errormessage to the user.
                //messageText.text = "Register error: " + ErrorMessage;
                break;
            default:
                throw new NotImplementedException("No implementation for loginWebRequestResponse of class: " + webRequestResponse.GetType());
        }
    }

    [ContextMenu("User/Login")]
    public async void Login()
    {
        user = new User(accountScreenLogic.LoginFields[0].text, accountScreenLogic.LoginFields[1].text);

        IWebRequestReponse webRequestResponse = await userApiClient.Login(user);

        switch (webRequestResponse)
        {
            case WebRequestData<string> dataResponse:
                Debug.Log("Login succes!");
                FetchUserData();
                screenLogic.ShowHomeScreen();
                // TODO: Todo handle succes scenario.
                // TODO: doorverwijzen naar de tijdlijn.
                break;
            case WebRequestError errorResponse:
                string errorMessage = errorResponse.ErrorMessage;
                Debug.Log("Login error: " + errorMessage);
                // TODO: Handle error scenario. Show the errormessage to the user.
                //messageText.text = "Login error: " + loginErrorMessage;
                break;
            default:
                throw new NotImplementedException("No implementation for loginWebRequestResponse of class: " + webRequestResponse.GetType());
        }
    }

    #endregion
}
