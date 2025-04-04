using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using TMPro;
using UI.Dates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AccountScreenLogic : MonoBehaviour
{
    [Header("Dropdowns")]
    public TMP_Dropdown patientDropdown;
    public TMP_Dropdown guardianDropdown;
    public TMP_Dropdown guardianPatientDropdown;
    public TMP_Dropdown treatmentplanDropdown;
    [Header("DatePickers")]
    public DatePicker birthday;
    public DatePicker nextAppointmentDay;

    [Header("Settings")]
    public string registerInfo;
    public string loginInfo;
    public string editInfo;

    [Header("Dependencies")]
    public BrabantApp brabantApp;
    public enum AccountState
    {
        Register,
        Login,
        Edit
    }
    public AccountState currentState;
    [Header("Accountscreen assets")]
    public GameObject PatientRegion;
    public TMP_InputField[] PatientFields;
    public GameObject GuardianRegion;
    public TMP_InputField[] GuardianFields;
    public GameObject AccountLoginRegion;
    public TMP_InputField[] LoginFields;
    public GameObject ButtonRegion;
    public Button[] Buttons;
    public TMP_Text infoText;

    void Start()
    {
        PatientFields = GetInputFieldsFromGameObject(PatientRegion);
        GuardianFields = GetInputFieldsFromGameObject(GuardianRegion);

        LoginFields = GetAccountInputFieldsFromGameObject(AccountLoginRegion);
        Buttons = GetButtonsFromGameObject(ButtonRegion);


        AddFieldListeners();
        setRegisterState();
        StartCoroutine(WaitForKeepAliveAndUpdateUI());
    }

    void OnEnable()
    {
        Debug.Log("enabled account screen");
        StartCoroutine(WaitForKeepAliveAndUpdateUI());
        //updateUI();
    }
    private void OnDisable()
    {
        Debug.Log("dissabeld account screen");
    }
    private IEnumerator WaitForKeepAliveAndUpdateUI()
    {
        while (KeepAlive.Instance == null)
        {
            Debug.Log("Waiting for KeepAlive to initialize...");
            yield return null;
        }

        Debug.Log("KeepAlive initialized! Fetching data...");

        brabantApp.FetchUserData();

        updateUI();
    }
    public void PopulateTreatmentPlanDropDown()
    {
        treatmentplanDropdown.ClearOptions();
        List<string> options = new List<string>();
        foreach (var treatmentPlan in KeepAlive.Instance.TreatmentPlans)
        {
            options.Add($"{treatmentPlan.name}");
        }

        treatmentplanDropdown.AddOptions(options);
        treatmentplanDropdown.onValueChanged.AddListener(OnTreatmentPlanSelected);
    }

    

    public void PopulatePatientDropdown()
    {
        patientDropdown.ClearOptions();
        List<string> options = new List<string>();
        options.Add("Create");
        foreach (var patient in KeepAlive.Instance.StoredPatients)
        {
            options.Add($"{patient.firstName} {patient.lastName}");
        }

        patientDropdown.AddOptions(options);
        patientDropdown.onValueChanged.AddListener(OnPatientSelected);
    }

    public void PopulateGuardianDropdown()
    {
        guardianDropdown.ClearOptions();
        List<string> options = new List<string>();
        options.Add("Create");
        foreach (var guardian in KeepAlive.Instance.StoredGuardians)
        {
            options.Add($"{guardian.firstName} {guardian.lastName}");
        }
        guardianDropdown.AddOptions(options);


        guardianDropdown.onValueChanged.AddListener(OnGuardianSelected);
    }
    public void PopulateGuardianPatientDropdown()
    {
        guardianPatientDropdown.ClearOptions();
        List<string> options = new List<string>();
        foreach (var guardian in KeepAlive.Instance.StoredGuardians)
        {
            options.Add($"{guardian.firstName} {guardian.lastName}");
        }
        guardianPatientDropdown.AddOptions(options);
        guardianPatientDropdown.onValueChanged.AddListener(OnGuardianPatientSelected);
    }
    public void SetGuardianDropdownTo(string targetGuardianid)
    {
        Guardian targetGuardian = KeepAlive.Instance.StoredGuardians.Find(g => g.id == targetGuardianid);

        if (targetGuardian == null)
            return;

        int index = KeepAlive.Instance.StoredGuardians.IndexOf(targetGuardian);

        guardianPatientDropdown.value = index;
        Debug.Log($"Dropdown set to: {targetGuardian.firstName} {targetGuardian.lastName}");

    }


    #region statesetters
    public void setRegisterState()
    {
        currentState = AccountState.Register;
        PatientRegion.SetActive(true);
        GuardianRegion.SetActive(true);
        AccountLoginRegion.SetActive(true);
        infoText.text = registerInfo;
        UpdateButtonStates("RegisterButton");
        Debug.Log($"Set State: {currentState}");
    }

    public void setLoginState()
    {
        currentState = AccountState.Login;
        PatientRegion.SetActive(false);
        GuardianRegion.SetActive(false);
        AccountLoginRegion.SetActive(true);
        infoText.text = loginInfo;
        UpdateButtonStates("LoginButton");
        Debug.Log($"Set State: {currentState}");
    }

    public void setEditState()
    {
        currentState = AccountState.Edit;
        PatientRegion.SetActive(true);
        GuardianRegion.SetActive(true);
        AccountLoginRegion.SetActive(false);
        infoText.text = editInfo;
        UpdateButtonStates("SaveButton");
        Debug.Log($"Set State: {currentState}");
    }

    private void UpdateButtonStates(string activeButtonName)
    {
        foreach (var button in Buttons)
        {
            button.gameObject.SetActive(true);
            button.transform.localScale = button.name == activeButtonName
                ? new Vector3(1.2f, 1.2f, 1)
                : new Vector3(1, 1, 1);
        }
    }
    #endregion
    public void updateUI()
    {
        Debug.Log("Updating accountscreen ui");

        if (PatientFields != null && PatientFields.Length >= 2)
        {
            string defaultFirstName = "Unknown";
            string defaultLastName = "Unknown";
            string defaultGuardianID = "";
            DateTime defaultBirthDate = DateTime.Now;
            DateTime defaultNextAppointmentDate = DateTime.Now;

            if (KeepAlive.Instance.StoredPatient != null)
            {
                defaultFirstName = !string.IsNullOrEmpty(KeepAlive.Instance.StoredPatient.firstName)
                    ? KeepAlive.Instance.StoredPatient.firstName
                    : defaultFirstName;

                defaultLastName = !string.IsNullOrEmpty(KeepAlive.Instance.StoredPatient.lastName)
                    ? KeepAlive.Instance.StoredPatient.lastName
                    : defaultLastName;

                defaultGuardianID = !string.IsNullOrEmpty(KeepAlive.Instance.StoredPatient.guardianID)
                    ? KeepAlive.Instance.StoredPatient.guardianID
                    : defaultGuardianID;

                defaultBirthDate = KeepAlive.Instance.StoredPatient.BirthDate != DateTime.MinValue
                    ? KeepAlive.Instance.StoredPatient.BirthDate
                    : defaultBirthDate;

                defaultNextAppointmentDate = KeepAlive.Instance.StoredPatient.NextAppointmentDate != DateTime.MinValue
                    ? KeepAlive.Instance.StoredPatient.NextAppointmentDate
                    : defaultNextAppointmentDate;

            }

            PatientFields[0].text = defaultFirstName;
            PatientFields[1].text = defaultLastName;
            SetGuardianDropdownTo(defaultGuardianID);
            birthday.SelectedDate = defaultBirthDate;
            birthday.VisibleDate = defaultBirthDate;
            nextAppointmentDay.SelectedDate = defaultNextAppointmentDate;
            nextAppointmentDay.VisibleDate = defaultNextAppointmentDate;
            Debug.Log($"Set values - Name: {defaultFirstName} {defaultLastName}, GuardianID: {defaultGuardianID}, BirthDate: {defaultBirthDate}");
        }


        if (GuardianFields != null && GuardianFields.Length >= 2 && KeepAlive.Instance.StoredGuardian != null)
        {
            GuardianFields[0].text = KeepAlive.Instance.StoredGuardian.firstName;
            GuardianFields[1].text = KeepAlive.Instance.StoredGuardian.lastName;
        }
        brabantApp.updateUI();

        if (KeepAlive.Instance.UserToken == "" || KeepAlive.Instance.UserToken == null)
        {
            setLoginState();
        }
        else
        {
            setEditState();
        }
        Debug.Log($"Current State: {currentState}");
    }


    #region buttons
    public void registerButtonClick()
    {
        if (currentState == AccountState.Register)
        {
            brabantApp.Register();
        }
        else
        {
            setRegisterState();
        }
        WaitForKeepAliveAndUpdateUI();

    }
    public void loginButtonClick()
    {
        if (currentState == AccountState.Login)
        {
            brabantApp.Login();
        }
        else
        {
            setLoginState();
        }
        WaitForKeepAliveAndUpdateUI();
    }
    public async void patientSaveButtonClickAsync()
    {

        Debug.Log("Patient saving to api.");
        if (PatientFields[0] != null && PatientFields.Length >= 2)
        {
            KeepAlive.Instance.StoredPatient.firstName = PatientFields[0].text;
            KeepAlive.Instance.StoredPatient.lastName = PatientFields[1].text;
        }
        IWebRequestReponse response = await brabantApp.savetoApiPatientAsync();

        if (response is WebRequestData<Patient>)
        {
            Debug.Log("Patient saved successfully.");
            brabantApp.FetchUserData();
        }
        else if (response is WebRequestError errorResponse)
        {
            Debug.LogError("Failed to save patient: " + errorResponse.ErrorMessage );
        }
        else
        {
            Debug.Log($"Got webresponse {response.GetType()}");
        }
        KeepAlive.Instance.StoredPatient = null;

    }
    public async void guardianSaveButtonClickAsync()
    {

        if (PatientFields[0] != null && PatientFields.Length >= 2)
        {
            KeepAlive.Instance.StoredGuardian.firstName = GuardianFields[0].text;
            KeepAlive.Instance.StoredGuardian.lastName = GuardianFields[1].text;
        }
        IWebRequestReponse response = await brabantApp.savetoApiGuardianAsync();

        if (response is WebRequestData<Guardian>)
        {
            Debug.Log("Guardian saved successfully.");
            brabantApp.FetchUserData();
        }
        else if (response is WebRequestError errorResponse)
        {
            Debug.LogError("Failed to save guardian: " + errorResponse.ErrorMessage);
        }
        else
        {
            Debug.Log($"Got webresponse {response.GetType()}");
        }
        KeepAlive.Instance.StoredGuardian = null;

    }
    #endregion

    #region helpermethods

    private TMP_InputField[] GetInputFieldsFromGameObject(GameObject mainGameObject)
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
    private TMP_InputField[] GetAccountInputFieldsFromGameObject(GameObject mainGameObject)
    {
        Transform fieldsGameObject = mainGameObject.transform.Find("Fields");
        if (fieldsGameObject != null)
        {
            TMP_InputField nameField = fieldsGameObject.transform.Find("UsernameInput")?.GetComponent<TMP_InputField>();
            TMP_InputField surnameField = fieldsGameObject.transform.Find("PasswordInput")?.GetComponent<TMP_InputField>();

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
    private Button[] GetButtonsFromGameObject(GameObject mainGameObject)
    {
        if (mainGameObject == null)
        {
            Debug.LogError("Main GameObject is null.");
            return new Button[0];
        }

        Button[] buttons = mainGameObject.GetComponentsInChildren<Button>();

        if (buttons.Length == 0)
        {
            Debug.LogWarning("No buttons found in mainGameObject.");
        }

        return buttons;
    }
    #endregion

    #region callbacks
    private void AddFieldListeners()
    {
        if (PatientFields != null && PatientFields.Length >= 2)
        {
            //PatientFields[0].onValueChanged.AddListener(delegate { OnPatientDataChanged(); });
            PatientFields[0].onEndEdit.AddListener(delegate { OnPatientDataChanged(); });

            //PatientFields[1].onValueChanged.AddListener(delegate { OnPatientDataChanged(); });
            PatientFields[1].onEndEdit.AddListener(delegate { OnPatientDataChanged(); });
        }

        if (GuardianFields != null && GuardianFields.Length >= 2)
        {
            //GuardianFields[0].onValueChanged.AddListener(delegate { OnGuardianDataChanged(); });
            GuardianFields[0].onEndEdit.AddListener(delegate { OnGuardianDataChanged(); });

            //GuardianFields[1].onValueChanged.AddListener(delegate { OnGuardianDataChanged(); });
            GuardianFields[1].onEndEdit.AddListener(delegate { OnGuardianDataChanged(); });
        }
        birthday.Config.Events.OnDaySelected.AddListener(OnBirthdayPicked);
        nextAppointmentDay.Config.Events.OnDaySelected.AddListener(OnNextAppointmentDayPicked);
    }
    public void OnBirthdayPicked(DateTime date)
    {
        KeepAlive.Instance.StoredPatient.BirthDate = date;
    }
    public void OnNextAppointmentDayPicked(DateTime date)
    {
        KeepAlive.Instance.StoredPatient.NextAppointmentDate = date;
    }
    private void OnPatientSelected(int index)
    {
        if (index == 0)
        {
            KeepAlive.Instance.StoredPatient = null;
            Debug.Log("Create a new patient selected.");
        }
        else if (index - 1 < KeepAlive.Instance.StoredGuardians.Count)
        {
            KeepAlive.Instance.StoredPatient = KeepAlive.Instance.StoredPatients[index - 1];
            Debug.Log($"Selected patient: {KeepAlive.Instance.StoredPatient.firstName} {KeepAlive.Instance.StoredPatient.lastName}");
        }
        updateUI();
    }
    private void OnGuardianSelected(int index)
    {
        if (index == 0)
        {
            KeepAlive.Instance.StoredGuardian = null;
            Debug.Log("Create a new guardian selected.");
        }
        else if (index - 1 < KeepAlive.Instance.StoredGuardians.Count)
        {
            KeepAlive.Instance.StoredGuardian = KeepAlive.Instance.StoredGuardians[index - 1];
            Debug.Log($"Guardian selected:{KeepAlive.Instance.StoredGuardian.id}");
        }
        updateUI();
    }
    private void OnGuardianPatientSelected(int index)
    {
        if (index < KeepAlive.Instance.StoredGuardians.Count)
        {
            KeepAlive.Instance.StoredPatient.guardianID = KeepAlive.Instance.StoredGuardians[index].id;
            Debug.Log($"Guardian selected:{KeepAlive.Instance.StoredGuardian.id}");
        }
        updateUI();
    }
    private void OnTreatmentPlanSelected(int index)
    {
        if (index < KeepAlive.Instance.TreatmentPlans.Count)
        {
            KeepAlive.Instance.StoredPatient.treatmentPlanID = KeepAlive.Instance.TreatmentPlans[index].id;
            Debug.Log($"Guardian selected:{KeepAlive.Instance.StoredPatient.treatmentPlanID}");
        }
        updateUI();
    }



    private void OnPatientDataChanged()
    {
        if (PatientFields[0] != null && PatientFields.Length >= 2)
        {
            KeepAlive.Instance.StoredPatient.firstName = PatientFields[0].text;
            KeepAlive.Instance.StoredPatient.lastName = PatientFields[1].text;
        }
        Debug.Log("Patient data updated in KeepAlive.");
    }

    private void OnGuardianDataChanged()
    {
        if (PatientFields[0] != null && PatientFields.Length >= 2)
        {
            KeepAlive.Instance.StoredGuardian.firstName = GuardianFields[0].text;
            KeepAlive.Instance.StoredGuardian.lastName = GuardianFields[1].text;
        }
        Debug.Log("Guardian data updated in KeepAlive.");
    }
    #endregion
}
