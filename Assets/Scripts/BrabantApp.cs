using System;
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

    [Header("Settings")]
    public bool GenerateDate;
    public bool requiresSurgery;

    [Header("User Edit fields")]
    public GameObject PatientRegion;
    public GameObject GaurdianRegion;
    private TMP_InputField[] PatientFields;
    private TMP_InputField[] GuardianFields;

    [Header("User Login fields")]
    public GameObject AccountLoginRegion;
    private TMP_InputField[] LoginFields;

    [Header("User Register fields")]
    public GameObject AccountRegisterRegion;
    public GameObject PatientRegisterRegion;
    public GameObject GaurdianRegisterRegion;
    private TMP_InputField[] RegisterFields;
    private TMP_InputField[] PatientRegisterFields;
    private TMP_InputField[] GuardianRegisterFields;

    [Header("Personal Info")]
    public TMP_Text personalInfo;


    void Start()
    {
        PatientFields = GetInputFieldsFromGameObject(PatientRegion);
        GuardianFields = GetInputFieldsFromGameObject(GaurdianRegion);
        PatientRegisterFields = GetInputFieldsFromGameObject(PatientRegisterRegion);
        GuardianRegisterFields = GetInputFieldsFromGameObject(GaurdianRegisterRegion);

        LoginFields = GetAccountInputFieldsFromGameObject(AccountLoginRegion);
        RegisterFields = GetAccountInputFieldsFromGameObject(AccountRegisterRegion);


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

        saveData(PatientFields, GuardianFields);

        AddFieldListeners();
    }


    public void saveData(TMP_InputField[] patientFields, TMP_InputField[] guardianFields)
    {

        // Update Patient fields (name and surname)
        if (patientFields[0] != null && patientFields.Length >= 2)
        {
            KeepAlive.Instance.StoredPatient.FirstName = patientFields[0].text;
            KeepAlive.Instance.StoredPatient.LastName = patientFields[1].text;
        }

        if (guardianFields[0] != null && guardianFields.Length >= 2)
        {
            KeepAlive.Instance.StoredGuardian.FirstName = guardianFields[0].text;
            KeepAlive.Instance.StoredGuardian.LastName = guardianFields[1].text;
        }

        updateUI();
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

    public void updateUI()
    {
        if (PatientFields != null && PatientFields.Length >= 2)
        {
            PatientFields[0].text = KeepAlive.Instance.StoredPatient.FirstName;
            PatientFields[1].text = KeepAlive.Instance.StoredPatient.LastName;
        }

        if (GuardianFields != null && GuardianFields.Length >= 2)
        {
            GuardianFields[0].text = KeepAlive.Instance.StoredGuardian.FirstName;
            GuardianFields[1].text = KeepAlive.Instance.StoredGuardian.LastName;
        }

        string patientInfo = $"Patient Name: {KeepAlive.Instance.StoredPatient.FirstName} {KeepAlive.Instance.StoredPatient.LastName}\n";

        string nextAppointment = "Next Appointment: ";

        string appointmentDate = "TBD";
        nextAppointment += appointmentDate;

        patientInfo += nextAppointment;

        if (personalInfo != null)
        {
            personalInfo.text = patientInfo;
        }
        else
        {
            Debug.LogError("PersonalInfo TMP_Text component is not assigned.");
        }
    }

    public TMP_InputField[] GetInputFieldsFromGameObject(GameObject mainGameObject)
    {
        Transform fieldsGameObject = mainGameObject.transform.Find("Fields");
        if (fieldsGameObject != null)
        {
            TMP_InputField nameField = fieldsGameObject.Find("NameField")?.GetComponent<TMP_InputField>();
            TMP_InputField surnameField = fieldsGameObject.Find("SurnameField")?.GetComponent<TMP_InputField>();

            if (nameField == null || surnameField == null)
            {
                Debug.LogError("Name field or surname field is missing.");
            }

            return new TMP_InputField[] { nameField, surnameField };
        }
        else
        {
            Debug.LogWarning("Fields not found in mainGameObject");
            return null;
        }
    }
    public TMP_InputField[] GetAccountInputFieldsFromGameObject(GameObject mainGameObject)
    {
        if (mainGameObject != null)
        {
            TMP_InputField nameField = mainGameObject.transform.Find("UsernameInput")?.GetComponent<TMP_InputField>();
            TMP_InputField surnameField = mainGameObject.transform.Find("PasswordInput")?.GetComponent<TMP_InputField>();

            if (nameField == null || surnameField == null)
            {
                Debug.LogError("Name field or surname field is missing.");
            }


            return new TMP_InputField[] { nameField, surnameField };
        }
        else
        {
            Debug.LogWarning("Fields not found in mainGameObject");
            return null;
        }
    }

    #region callbacks

    private void AddFieldListeners()
    {
        if (PatientFields != null && PatientFields.Length >= 2)
        {
            PatientFields[0].onEndEdit.AddListener(delegate { OnPatientDataChanged(); });
            PatientFields[1].onEndEdit.AddListener(delegate { OnPatientDataChanged(); });
        }

        if (GuardianFields != null && GuardianFields.Length >= 2)
        {
            GuardianFields[0].onEndEdit.AddListener(delegate { OnGuardianDataChanged(); });
            GuardianFields[1].onEndEdit.AddListener(delegate { OnGuardianDataChanged(); });
        }

        // Register Fields Listeners
        if (PatientRegisterFields != null && PatientRegisterFields.Length >= 2)
        {
            PatientRegisterFields[0].onEndEdit.AddListener(delegate { OnPatientRegisterDataChanged(); });
            PatientRegisterFields[1].onEndEdit.AddListener(delegate { OnPatientRegisterDataChanged(); });
        }

        if (GuardianRegisterFields != null && GuardianRegisterFields.Length >= 2)
        {
            GuardianRegisterFields[0].onEndEdit.AddListener(delegate { OnGuardianRegisterDataChanged(); });
            GuardianRegisterFields[1].onEndEdit.AddListener(delegate { OnGuardianRegisterDataChanged(); });
        }
    }


    private void OnPatientDataChanged()
    {
        saveData(PatientFields, GuardianFields);
        Debug.Log("Patient data updated in KeepAlive.");
    }

    private void OnGuardianDataChanged()
    {
        saveData(PatientFields, GuardianFields);
        Debug.Log("Guardian data updated in KeepAlive.");
    }

    private void OnPatientRegisterDataChanged()
    {
        saveData(PatientRegisterFields, GuardianRegisterFields);
        Debug.Log("Patient registration data updated in KeepAlive.");
    }

    private void OnGuardianRegisterDataChanged()
    {
        saveData(PatientRegisterFields, GuardianRegisterFields);
        Debug.Log("Guardian registration data updated in KeepAlive.");
    }
    #endregion

    #region Login

    [ContextMenu("User/Register")]
    public async void Register()
    {
        user = new User(RegisterFields[0].text, RegisterFields[1].text);


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
        user = new User(LoginFields[0].text, LoginFields[1].text);

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
