using System;
using System.Collections.Generic;

[System.Serializable]
public class Guardian
{
    public string ID;
    public string FirstName;
    public string LastName;
    
    public Guardian()
    {

    }

    public Guardian(string FirstName, string LastName)
    {
        this.FirstName = FirstName;
        this.LastName = LastName;
    }
}