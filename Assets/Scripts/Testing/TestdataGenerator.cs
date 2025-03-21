using UnityEngine;
using System;
using System.Collections.Generic;

public class TestdataGenerator : MonoBehaviour
{
    private readonly List<string> doctorNames = new List<string> { "Alice", "Bob", "Charlie", "Diana", "Ethan" };
    private readonly List<string> specializations = new List<string> { "Cardiology", "Neurology", "Pediatrics", "Oncology", "Orthopedics" };

    private readonly List<string> firstNames = new List<string> { "Emma", "Noah", "Olivia", "Liam", "Sophia", "Mason" };
    private readonly List<string> lastNames = new List<string> { "Smith", "Johnson", "Williams", "Brown", "Jones" };

    void Start()
    {
        Doctor testDoctor = GenerateDoctor();
        Guardian testGuardian = GenerateGuardian();
        Patient testPatient = GeneratePatient(testGuardian.ID, testDoctor.ID);

        Debug.Log($"Generated Doctor: {testDoctor.Name}, Specialization: {testDoctor.Specialization}, ID: {testDoctor.ID}");
        Debug.Log($"Generated Guardian: {testGuardian.FirstName} {testGuardian.LastName}, ID: {testGuardian.ID}");
        Debug.Log($"Generated Patient: {testPatient.FirstName} {testPatient.LastName}, GuardianID: {testPatient.GuardianID}, DoctorID: {testPatient.DoctorID}");

        TreatmentPlanCreator creator = new TreatmentPlanCreator();

        bool requiresSurgery = false; // Set to true if the patient needs surgery

        TreatmentPlan treatmentPlan = creator.GenerateFractureTreatmentPlan(testPatient.ID, testDoctor.ID, requiresSurgery);

    }

    Doctor GenerateDoctor()
    {
        return new Doctor
        {
            ID = Guid.NewGuid(),
            Name = doctorNames[UnityEngine.Random.Range(0, doctorNames.Count)],
            Specialization = specializations[UnityEngine.Random.Range(0, specializations.Count)],
            PatientIDs = new List<Guid>()
        };
    }

    Guardian GenerateGuardian()
    {
        return new Guardian
        {
            ID = Guid.NewGuid(),
            FirstName = firstNames[UnityEngine.Random.Range(0, firstNames.Count)],
            LastName = lastNames[UnityEngine.Random.Range(0, lastNames.Count)],
            PatientIDs = new List<Guid>()
        };
    }

    Patient GeneratePatient(Guid guardianID, Guid? doctorID)
    {
        return new Patient
        {
            ID = Guid.NewGuid(),
            FirstName = firstNames[UnityEngine.Random.Range(0, firstNames.Count)],
            LastName = lastNames[UnityEngine.Random.Range(0, lastNames.Count)],
            GuardianID = guardianID,
            TreatmentPlanID = Guid.NewGuid(),
            DoctorID = doctorID
        };
    }
}
