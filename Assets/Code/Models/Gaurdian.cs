using System;
using System.Collections.Generic;

[System.Serializable]
public class Guardian
{
    public string ID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<string> PatientIDs { get; set; }
    
    public Guardian()
    {

    }

    public Guardian(string FirstName, string LastName)
    {
        this.FirstName = FirstName;
        this.LastName = LastName;
    }
}