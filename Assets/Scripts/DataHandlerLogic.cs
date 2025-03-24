using TMPro;
using UnityEngine;

public class DataHandlerLogic : MonoBehaviour
{
    public bool GenerateDate;
    public bool requiresSurgery;
    public TMP_Text PatientRegion;
    public TMP_Text GaurdianRegion;



    void Start()
    {
        if (GenerateDate)
        {
            Doctor testDoctor = TestdataGenerator.GenerateDoctor();
            Guardian testGuardian = TestdataGenerator.GenerateGuardian();
            Patient testPatient = TestdataGenerator.GeneratePatient(testGuardian.ID, testDoctor.ID);

            Debug.Log($"Generated Doctor: {testDoctor.Name}, Specialization: {testDoctor.Specialization}, ID: {testDoctor.ID}");
            Debug.Log($"Generated Guardian: {testGuardian.FirstName} {testGuardian.LastName}, ID: {testGuardian.ID}");
            Debug.Log($"Generated Patient: {testPatient.FirstName} {testPatient.LastName}, GuardianID: {testPatient.GuardianID}, DoctorID: {testPatient.DoctorID}");
            TreatmentPlanCreator.GenerateFractureTreatmentPlan(testPatient.ID,testDoctor.ID,requiresSurgery);
        }
    }

    

    public void UpdateData()
    {

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
