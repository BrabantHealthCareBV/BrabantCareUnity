using TMPro;
using UnityEngine;

public class BrabantApp : MonoBehaviour
{

    [Header("Settings")]
    public bool GenerateDate = true;
    public bool requiresSurgery;

    [Header("User Edit fields")]
    public GameObject PatientRegion;
    public GameObject GaurdianRegion;
    private TMP_InputField[] PatientFields;
    private TMP_InputField[] GuardianFields;

    [Header("User Login fields")]
    public GameObject AccountLoginRegion;

    [Header("User Register fields")]
    public GameObject PatientRegisterRegion;
    public GameObject GaurdianRegisterRegion;
    public GameObject AccountRegisterRegion;
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
        if (patientFields != null && patientFields.Length >= 2)
        {
            KeepAlive.Instance.StoredPatient.FirstName = patientFields[0].text;
            KeepAlive.Instance.StoredPatient.LastName = patientFields[1].text;
        }

        if (guardianFields != null && guardianFields.Length >= 2)
        {
            KeepAlive.Instance.StoredGuardian.FirstName = guardianFields[0].text;
            KeepAlive.Instance.StoredGuardian.LastName = guardianFields[1].text;
        }

        DisplayPatientInfo();
        updateUI();
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
    }

    private void DisplayPatientInfo()
    {
        // Assuming that you want to display the patient's name and next appointment info in the TMP_Text field
        string patientInfo = $"Patient Name: {KeepAlive.Instance.StoredPatient.FirstName} {KeepAlive.Instance.StoredPatient.LastName}\n";

        // Assuming you have the next appointment date as part of the treatment plan
        string nextAppointment = "Next Appointment: ";

        // Fetch the next appointment from the treatment plan
        string appointmentDate = "TBD"; // Replace this with actual logic to fetch the next appointment date
        nextAppointment += appointmentDate;

        // Combine the information
        patientInfo += nextAppointment;

        // Set the text of the TMP_Text component
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
    #region callbacks

    private void AddFieldListeners()
    {
        if (PatientFields != null && PatientFields.Length >= 2)
        {
            PatientFields[0].onValueChanged.AddListener(delegate { OnPatientDataChanged(); });
            PatientFields[1].onValueChanged.AddListener(delegate { OnPatientDataChanged(); });
        }

        if (GuardianFields != null && GuardianFields.Length >= 2)
        {
            GuardianFields[0].onValueChanged.AddListener(delegate { OnGuardianDataChanged(); });
            GuardianFields[1].onValueChanged.AddListener(delegate { OnGuardianDataChanged(); });
        }

        // Register Fields Listeners
        if (PatientRegisterFields != null && PatientRegisterFields.Length >= 2)
        {
            PatientRegisterFields[0].onValueChanged.AddListener(delegate { OnPatientRegisterDataChanged(); });
            PatientRegisterFields[1].onValueChanged.AddListener(delegate { OnPatientRegisterDataChanged(); });
        }

        if (GuardianRegisterFields != null && GuardianRegisterFields.Length >= 2)
        {
            GuardianRegisterFields[0].onValueChanged.AddListener(delegate { OnGuardianRegisterDataChanged(); });
            GuardianRegisterFields[1].onValueChanged.AddListener(delegate { OnGuardianRegisterDataChanged(); });
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
}
