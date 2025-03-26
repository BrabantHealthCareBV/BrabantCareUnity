using System;

[System.Serializable]
public class Patient
{
    public Guid ID;
    public string FirstName;
    public string LastName;
    public Guid GuardianID;
    public Guid TreatmentPlanID;
    public Guid? DoctorID;
}