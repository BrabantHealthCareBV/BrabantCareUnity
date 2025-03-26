using System;
using System.Collections.Generic;

[System.Serializable]
public class Doctor
{
    public Guid ID;
    public string Name;
    public string Specialization;
    public List<Guid> PatientIDs;

}