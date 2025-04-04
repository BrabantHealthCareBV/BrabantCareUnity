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
    [Header("User Login fields")]
    public GameObject AccountLoginRegion;

    [Header("User Register fields")]
    public GameObject PatientRegisterRegion;
    public GameObject GaurdianRegisterRegion;
    public GameObject AccountRegisterRegion;

    [Header("Personal Info")]
    public TMP_Text personalInfo;


    private TMP_InputField[] PatientFields;
    private TMP_InputField[] GuardianFields;
    void Start()
    {
        PatientFields = GetInputFieldsFromGameObject(PatientRegion);
        GuardianFields = GetInputFieldsFromGameObject(GaurdianRegion);

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

        UpdateData();

        AddFieldListeners();
    }


    public void UpdateData()
    {
        if (KeepAlive.Instance.StoredPatient == null || KeepAlive.Instance.StoredGuardian == null)
        {
            Debug.LogWarning("No stored patient or guardian data found.");
            return;
        }

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
        DisplayPatientInfo();
    }

    private void DisplayPatientInfo()
    {
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
    }

    private void OnPatientDataChanged()
    {
        if (KeepAlive.Instance.StoredPatient != null)
        {
            KeepAlive.Instance.StoredPatient.FirstName = PatientFields[0].text;
            KeepAlive.Instance.StoredPatient.LastName = PatientFields[1].text;
        }
        Debug.Log("Patient data updated in KeepAlive.");
        DisplayPatientInfo();
    }

    private void OnGuardianDataChanged()
    {
        if (KeepAlive.Instance.StoredGuardian != null)
        {
            KeepAlive.Instance.StoredGuardian.FirstName = GuardianFields[0].text;
            KeepAlive.Instance.StoredGuardian.LastName = GuardianFields[1].text;
        }
        Debug.Log("Guardian data updated in KeepAlive.");
    }
}
