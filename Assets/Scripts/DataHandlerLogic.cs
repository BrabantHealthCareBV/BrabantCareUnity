using TMPro;
using UnityEngine;

public class DataHandlerLogic : MonoBehaviour
{
    public bool GenerateDate = true;
    public bool requiresSurgery;
    public GameObject PatientRegion;
    public GameObject GaurdianRegion;
    public TMP_Text personalInfo; // The TMP_Text UI component to display patient info
    // 0 name 1 surname
    private TMP_InputField[] PatientFields;
    private TMP_InputField[] GaurdianFields;
    private Doctor testDoctor;
    public Guardian guardian;
    public Patient patient;

    void Start()
    {
        PatientFields = GetInputFieldsFromGameObject(PatientRegion);
        GaurdianFields = GetInputFieldsFromGameObject(GaurdianRegion);

        if (GenerateDate)
        {
            testDoctor = TestdataGenerator.GenerateDoctor();
            guardian = TestdataGenerator.GenerateGuardian();
            patient = TestdataGenerator.GeneratePatient(guardian.ID, testDoctor.ID);

            Debug.Log($"Generated Doctor: {testDoctor.Name}, Specialization: {testDoctor.Specialization}, ID: {testDoctor.ID}");
            Debug.Log($"Generated Guardian: {guardian.FirstName} {guardian.LastName}, ID: {guardian.ID}");
            Debug.Log($"Generated Patient: {patient.FirstName} {patient.LastName}, GuardianID: {patient.GuardianID}, DoctorID: {patient.DoctorID}");
            //TreatmentPlanCreator.GenerateFractureTreatmentPlan(patient.ID,testDoctor.ID,requiresSurgery);
            UpdateData();
        }
        // Add listeners to update patient and guardian data when fields are modified
        if (PatientFields != null && PatientFields.Length >= 2)
        {
            PatientFields[0].onValueChanged.AddListener((text) => UpdatePatientData());
            PatientFields[1].onValueChanged.AddListener((text) => UpdatePatientData());
        }

        if (GaurdianFields != null && GaurdianFields.Length >= 2)
        {
            GaurdianFields[0].onValueChanged.AddListener((text) => UpdateGuardianData());
            GaurdianFields[1].onValueChanged.AddListener((text) => UpdateGuardianData());
        }
    }

    public void UpdateData()
    {
        // Update Patient fields (name and surname)
        if (PatientFields != null && PatientFields.Length >= 2)
        {
            PatientFields[0].text = patient.FirstName;  // namefield
            PatientFields[1].text = patient.LastName;   // surnamefield
        }

        // Update Guardian fields (name and surname)
        if (GaurdianFields != null && GaurdianFields.Length >= 2)
        {
            GaurdianFields[0].text = guardian.FirstName;  // namefield
            GaurdianFields[1].text = guardian.LastName;   // surnamefield
        }

        // Format and display data in the TMP_Text
        DisplayPatientInfo();
    }

    private void DisplayPatientInfo()
    {
        // Assuming that you want to display the patient's name and next appointment info in the TMP_Text field
        string patientInfo = $"Patient Naam: {patient.FirstName} {patient.LastName}\n\n";

        // Assuming you have the next appointment date as part of the treatment plan
        string nextAppointment = "Volgende Afspraak: \n";

        // Fetch the next appointment from the treatment plan
        string appointmentDate = "TBD\n"; // Replace this with actual logic to fetch the next appointment date
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

    private void UpdatePatientData()
    {
        if (PatientFields != null && PatientFields.Length >= 2)
        {
            patient.FirstName = PatientFields[0].text;
            patient.LastName = PatientFields[1].text;
            Debug.Log($"Updated Patient: {patient.FirstName} {patient.LastName}");
        }
    }

    // Method to update guardian data from input fields
    private void UpdateGuardianData()
    {
        if (GaurdianFields != null && GaurdianFields.Length >= 2)
        {
            guardian.FirstName = GaurdianFields[0].text;
            guardian.LastName = GaurdianFields[1].text;
            Debug.Log($"Updated Guardian: {guardian.FirstName} {guardian.LastName}");
        }
    }

    public TMP_InputField[] GetInputFieldsFromGameObject(GameObject mainGameObject)
    {
        // Find the child object that contains the fields
        Transform fieldsGameObject = mainGameObject.transform.Find("Fields");
        if (fieldsGameObject != null)
        {
            // Find the InputFields by name
            TMP_InputField nameField = fieldsGameObject.Find("NameField")?.GetComponent<TMP_InputField>();
            TMP_InputField surnameField = fieldsGameObject.Find("SurnameField")?.GetComponent<TMP_InputField>();

            // Ensure that both fields are found
            if (nameField == null || surnameField == null)
            {
                Debug.LogError("Name field or surname field is missing.");
            }

            // Return an array of InputFields (you can add more fields as needed)
            return new TMP_InputField[] { nameField, surnameField };
        }
        else
        {
            Debug.LogWarning("Fields not found in mainGameObject");
            return null;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
