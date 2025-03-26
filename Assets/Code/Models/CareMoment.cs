using System;

[System.Serializable]
public class CareMoment
{
    public Guid ID;
    public string Name;
    public string Url;
    public byte[] Image;
    public int? DurationInMinutes;
}