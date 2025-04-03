using System;
using System.Collections.Generic;

[System.Serializable]
public class Guardian
{
    public Guid ID;
    public string FirstName;
    public string LastName;
    public List<Guid> PatientIDs;
}