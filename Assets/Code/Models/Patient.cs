using System;

[System.Serializable]
public class Patient
{
    public string id;
    public string userID;
    public string firstName;
    public string lastName;
    public string birthdate;
    public string nextAppointmentDate;
    public string guardianID;
    public string treatmentPlanID;
    public string doctorID;
    public int gameState;
    public int score;
    public DateTime BirthDate
    {
        get => DateTime.TryParse(birthdate, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime parsedDate)
            ? parsedDate
            : DateTime.MinValue;
        set => birthdate = value.ToString("yyyy-MM-ddTHH:mm:ss");
    }
    public DateTime NextAppointmentDate
    {
        get => DateTime.TryParse(nextAppointmentDate, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime parsedDate)
            ? parsedDate
            : DateTime.MinValue;
        set => nextAppointmentDate = value.ToString("yyyy-MM-ddTHH:mm:ss");
    }
    public Patient()
    {

    }
    public void CleanData()
    {
        if (string.IsNullOrEmpty(id)) id = null;
        if (string.IsNullOrEmpty(userID)) userID = null;
        if (string.IsNullOrEmpty(firstName)) firstName = null;
        if (string.IsNullOrEmpty(lastName)) lastName = null;
        if (string.IsNullOrEmpty(birthdate)) birthdate = null;
        if (string.IsNullOrEmpty(nextAppointmentDate)) nextAppointmentDate = null;
        if (string.IsNullOrEmpty(guardianID)) guardianID = null;
        if (string.IsNullOrEmpty(treatmentPlanID)) treatmentPlanID = null;
        if (string.IsNullOrEmpty(doctorID)) doctorID = null;
    }
}