using System;
using System.Collections.Generic;

[System.Serializable]
public class TreatmentPlan
{
    public string ID;
    public string Name;
    public List<string> PatientIDs;
    public List<string> CareMomentIDs;

}