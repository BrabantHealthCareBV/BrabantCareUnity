using System;

[System.Serializable]
public class Patient
{
    public string id;
    public string userID;
    public string firstName;
    public string lastName;
    public DateTime birthDate;
    public DateTime? nextAppointmentDate;
    public string guardianID;
    public string treatmentPlanID;
    public string doctorID;
    public int gameState;
    public int score;

    public Patient()
    {

    }
}