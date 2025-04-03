using System;
using System.Collections.Generic;

[System.Serializable]
public class TreatmentPlan
{
    public Guid ID;
    public string Name;
    public List<Guid> PatientIDs;
    public List<Guid> CareMomentIDs;

}