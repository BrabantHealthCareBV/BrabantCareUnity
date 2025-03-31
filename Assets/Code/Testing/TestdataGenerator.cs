using UnityEngine;
using System;
using System.Collections.Generic;

public static class TestdataGenerator
{
    private static readonly List<string> doctorNames = new List<string> { "Alice", "Bob", "Charlie", "Diana", "Ethan" };
    private static readonly List<string> specializations = new List<string> { "Cardiology", "Neurology", "Pediatrics", "Oncology", "Orthopedics" };

    private static readonly List<string> firstNames = new List<string> { "Emma", "Noah", "Olivia", "Liam", "Sophia", "Mason" };
    private static readonly List<string> lastNames = new List<string> { "Smith", "Johnson", "Williams", "Brown", "Jones" };


    public static Doctor GenerateDoctor()
    {
        return new Doctor
        {
            ID = Convert.ToString(Guid.NewGuid()),
            Name = doctorNames[UnityEngine.Random.Range(0, doctorNames.Count)],
            Specialization = specializations[UnityEngine.Random.Range(0, specializations.Count)],
            PatientIDs = new List<string>()
        };
    }

    public static Guardian GenerateGuardian()
    {
        return new Guardian
        {
            ID = Convert.ToString(Guid.NewGuid()),
            FirstName = firstNames[UnityEngine.Random.Range(0, firstNames.Count)],
            LastName = lastNames[UnityEngine.Random.Range(0, lastNames.Count)]
        };
    }

    public static Patient GeneratePatient(string guardianID, string doctorID)
    {
        return new Patient
        {
            ID = Convert.ToString(Guid.NewGuid()),
            FirstName = firstNames[UnityEngine.Random.Range(0, firstNames.Count)],
            LastName = lastNames[UnityEngine.Random.Range(0, lastNames.Count)],
            GuardianID = guardianID,
            TreatmentPlanID = Convert.ToString(Guid.NewGuid()),
            DoctorID = doctorID
        };
    }
}
