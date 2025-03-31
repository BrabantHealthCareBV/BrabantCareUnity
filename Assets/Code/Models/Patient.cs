using System;

[System.Serializable]
public class Patient
{
    public string ID;
    public string FirstName;
    public string LastName;
    public DateTime BirthDate;
    public DateTime NextAppointmentDate;
    public string GuardianID;
    public string TreatmentPlanID;
    public string? DoctorID;

    public Patient()
    {

    }

    public Patient(string FirstName, string LastName)
    {
        this.FirstName = FirstName;
        this.LastName = LastName;
    }
}