using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccountScreenLogic : MonoBehaviour
{
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
    }

    void OnEnable()
    {
        StartCoroutine(WaitForKeepAliveAndUpdateUI());
        //updateUI();
    }
    private IEnumerator WaitForKeepAliveAndUpdateUI()
    {
        while (KeepAlive.Instance == null)
        {
            Debug.Log("Waiting for KeepAlive to initialize...");
            yield return null; // Wait for the next frame
        }

        Debug.Log("KeepAlive initialized! Updating UI.");
        updateUI();
    }
    public void setRegisterState()
    {
        currentState = AccountState.Register; // Update the state
        PatientRegion.SetActive(true);
        GuardianRegion.SetActive(true);
        AccountLoginRegion.SetActive(true);
        infoText.text = registerInfo;
        UpdateButtonStates("RegisterButton");
        Debug.Log($"Set State: {currentState}");
    }

    public void setLoginState()
    {
        currentState = AccountState.Login; // Update the state
        PatientRegion.SetActive(false);
        GuardianRegion.SetActive(false);
        AccountLoginRegion.SetActive(true);
        infoText.text = loginInfo;
        UpdateButtonStates("LoginButton");
        Debug.Log($"Set State: {currentState}");
    }

    public void setEditState()
    {
        currentState = AccountState.Edit; // Update the state
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

    public void registerButtonClick()
    {
        if (currentState == AccountState.Register)
        {
            saveData(PatientFields, GuardianFields);
            brabantApp.Register();
        }
        else
        {
            setRegisterState();
        }
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
    }
    public void saveButtonClick()
    {
        if (currentState == AccountState.Edit)
        {
            saveData(PatientFields, GuardianFields);
            brabantApp.postData();
        }
    }


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
            PatientFields[0].onEndEdit.AddListener(delegate { OnPatientDataChanged(); });
            PatientFields[1].onEndEdit.AddListener(delegate { OnPatientDataChanged(); });
        }

        if (GuardianFields != null && GuardianFields.Length >= 2)
        {
            GuardianFields[0].onEndEdit.AddListener(delegate { OnGuardianDataChanged(); });
            GuardianFields[1].onEndEdit.AddListener(delegate { OnGuardianDataChanged(); });
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
    #endregion
}
