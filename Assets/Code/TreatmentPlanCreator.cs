using UnityEngine;
using System;
using System.Collections.Generic;

public static class TreatmentPlanCreator
{
    static public TreatmentPlan GenerateFractureTreatmentPlan(string patientID, string doctorID, bool requiresSurgery)
    {
        string treatmentPlanID = Convert.ToString(Guid.NewGuid());
        List<string> careMomentIDs = new List<string>();
        List<TreatmentPlan_CareMoment> treatmentPlanCareMoments = new List<TreatmentPlan_CareMoment>();

        List<string> generalSteps = new List<string>
        {
            "Aankomst bij de Spoedeisende Hulp",
            "Eerste beoordeling",
            "Medische onderzoeken"
        };

        List<string> routeASteps = new List<string>
        {
            "Beoordeling door de orthopedisch arts",
            "Gipsaanleg",
            "Nazorginstructies",
            "Terug naar huis",
            "Follow-up afspraak"
        };

        List<string> routeBSteps = new List<string>
        {
            "Consultatie van specialist",
            "Besluit over operatie",
            "Informed consent",
            "Pre-operatieve zorg",
            "Anesthesie",
            "Chirurgische ingreep",
            "Ontwaken uit anesthesie",
            "Pijnmanagement",
            "Nazorginstructies",
            "Terug naar huis"
        };

        List<string> closingSteps = new List<string>
        {
            "Activiteiten aanpassen",
            "Gipsverwijdering",
            "Beoordeling van de genezing",
            "Instructies voor nazorg"
        };

        List<string> selectedSteps = new List<string>(generalSteps);
        selectedSteps.AddRange(requiresSurgery ? routeBSteps : routeASteps);
        selectedSteps.AddRange(closingSteps);

        // Generate care moments
        for (int i = 0; i < selectedSteps.Count; i++)
        {
            string careMomentID = Convert.ToString(Guid.NewGuid());
            careMomentIDs.Add(careMomentID);

            CareMoment newCareMoment = new CareMoment
            {
                ID = careMomentID,
                Name = selectedSteps[i],
                Url = "",
                Image = new byte[0],
                DurationInMinutes = UnityEngine.Random.Range(10, 60)
            };

            treatmentPlanCareMoments.Add(new TreatmentPlan_CareMoment
            {
                TreatmentPlanID = treatmentPlanID,
                CareMomentID = careMomentID,
                Order = i + 1
            });

            Debug.Log($"Generated CareMoment: {newCareMoment.Name}, ID: {newCareMoment.ID}");
        }

        // Create TreatmentPlan
        TreatmentPlan treatmentPlan = new TreatmentPlan
        {
            ID = treatmentPlanID,
            Name = "Gipsbehandeling",
            PatientIDs = new List<string> { patientID },
            CareMomentIDs = careMomentIDs
        };

        Debug.Log($"Generated TreatmentPlan: {treatmentPlan.Name}, Assigned Patient: {patientID}, Doctor: {doctorID}");

        return treatmentPlan;
    }
}
