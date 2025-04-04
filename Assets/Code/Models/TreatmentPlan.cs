using System;
using System.Collections.Generic;

[System.Serializable]
public class TreatmentPlan
{
    public string id;
    public string name;
    public List<string> patientIDs;
    public List<string> careMomentIDs;

}