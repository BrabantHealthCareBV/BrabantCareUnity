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
        if (GenerateDate && KeepAlive.Instance.StoredPatient == null)
        {
            KeepAlive.Instance.StoredDoctor = TestdataGenerator.GenerateDoctor();
            KeepAlive.Instance.StoredGuardian = TestdataGenerator.GenerateGuardian();
            KeepAlive.Instance.StoredPatient = TestdataGenerator.GeneratePatient(
                KeepAlive.Instance.StoredGuardian.ID,
                KeepAlive.Instance.StoredDoctor.ID
            );

            Debug.Log($"Generated Doctor: {KeepAlive.Instance.StoredDoctor.Name}, Specialization: {KeepAlive.Instance.StoredDoctor.Specialization}, ID: {KeepAlive.Instance.StoredDoctor.ID}");
            Debug.Log($"Generated Guardian: {KeepAlive.Instance.StoredGuardian.FirstName} {KeepAlive.Instance.StoredGuardian.LastName}, ID: {KeepAlive.Instance.StoredGuardian.ID}");
            Debug.Log($"Generated Patient: {KeepAlive.Instance.StoredPatient.FirstName} {KeepAlive.Instance.StoredPatient.LastName}, GuardianID: {KeepAlive.Instance.StoredPatient.GuardianID}, DoctorID: {KeepAlive.Instance.StoredPatient.DoctorID}");

        }
        FetchUserData();
    }


    

    public async void postData()
    {
        if (KeepAlive.Instance.UserToken != "")
        {
            IWebRequestReponse webRequestResponse = await patientApiClient.Create(KeepAlive.Instance.StoredPatient);

            switch (webRequestResponse)
            {
                case WebRequestData<Patient> dataResponse:
                    // TODO: Handle succes scenario.
                    break;
                case WebRequestError errorResponse:
                    string errorMessage = errorResponse.ErrorMessage;
                    Debug.Log("Create patient error: " + errorMessage);
                    // TODO: Handle error scenario. Show the errormessage to the user.
                    break;
                default:
                    throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
            }

            webRequestResponse = await guardianApiClient.Create(KeepAlive.Instance.StoredGuardian);

            switch (webRequestResponse)
            {
                case WebRequestData<Guardian> dataResponse:
                    // TODO: Handle succes scenario.
                    break;
                case WebRequestError errorResponse:
                    string errorMessage = errorResponse.ErrorMessage;
                    Debug.Log("Create guardian error: " + errorMessage);
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
        if (patientResponse is WebRequestData<List<Patient>> patientData && patientData.Data != null)
        {
            // Filter for the correct patient (adjust this based on your API structure)
            KeepAlive.Instance.StoredPatient = patientData.Data.FirstOrDefault(p => p.UserID == KeepAlive.Instance.StoredPatient.UserID);

            if (KeepAlive.Instance.StoredPatient != null)
            {
                Debug.Log("Fetched existing patient data.");
            }
            else
            {
                KeepAlive.Instance.StoredPatient = new Patient();
                Debug.Log("No matching patient found. Created new empty patient.");
            }
        }
        else if (patientResponse is WebRequestError patientError)
        {
            Debug.LogError($"Error fetching patient data: {patientError.ErrorMessage}");
        }

        // Fetch guardian data
        IWebRequestReponse guardianResponse = await guardianApiClient.GetAll();
        if (guardianResponse is WebRequestData<List<Guardian>> guardianData && guardianData.Data != null)
        {
            // Filter for the correct guardian
            KeepAlive.Instance.StoredGuardian = guardianData.Data.FirstOrDefault(g => g.ID == KeepAlive.Instance.StoredPatient.GuardianID);

            if (KeepAlive.Instance.StoredGuardian != null)
            {
                Debug.Log("Fetched existing guardian data.");
            }
            else
            {
                KeepAlive.Instance.StoredGuardian = new Guardian();
                Debug.Log("No matching guardian found. Created new empty guardian.");
            }
        }
        else if (guardianResponse is WebRequestError guardianError)
        {
            Debug.LogError($"Error fetching guardian data: {guardianError.ErrorMessage}");
        }

        // Update UI after fetching data
        updateUI();
    }


    private void HandleApiResponse(IWebRequestReponse response, string action)
    {
        switch (response)
        {
            case WebRequestData<object> successResponse:
                Debug.Log($"{action} success.");
                break;
            case WebRequestError errorResponse:
                Debug.LogError($"{action} error: {errorResponse.ErrorMessage}");
                break;
            default:
                throw new NotImplementedException($"No implementation for response type: {response.GetType()}");
        }
    }

    public async void SaveData()
    {
        if (string.IsNullOrEmpty(KeepAlive.Instance.UserToken))
        {
            Debug.Log("User is not logged in. Cannot save data.");
            return;
        }

        Debug.Log("Saving user data...");

        // Check if patient already exists, if so, update instead of create
        if (KeepAlive.Instance.StoredPatient.ID != "")
        {
            var patientResponse = await patientApiClient.Update(KeepAlive.Instance.StoredPatient);
            HandleApiResponse(patientResponse, "Update patient");
        }
        else
        {
            var patientResponse = await patientApiClient.Create(KeepAlive.Instance.StoredPatient);
            HandleApiResponse(patientResponse, "Create patient");
        }

        // Check if guardian already exists, if so, update instead of create
        if (KeepAlive.Instance.StoredGuardian.ID != "")
        {
            var guardianResponse = await guardianApiClient.Update(KeepAlive.Instance.StoredGuardian);
            HandleApiResponse(guardianResponse, "Update guardian");
        }
        else
        {
            var guardianResponse = await guardianApiClient.Create(KeepAlive.Instance.StoredGuardian);
            HandleApiResponse(guardianResponse, "Create guardian");
        }
    }

    public void updateUI()
    {
        if (KeepAlive.Instance.StoredPatient == null)
            return;

        string patientInfo = $"Patient Name: {KeepAlive.Instance.StoredPatient.FirstName} {KeepAlive.Instance.StoredPatient.LastName}\n";
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
