using System;

[System.Serializable]
public class Patient
{
    public string ID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime NextAppointmentDate { get; set; }
    public string GuardianID { get; set; }
    public string TreatmentPlanID { get; set; }
    public string? DoctorID { get; set; }

    public Patient()
    {

    }

    public Patient(string FirstName, string LastName)
    {
        this.FirstName = FirstName;
        this.LastName = LastName;
    }
}