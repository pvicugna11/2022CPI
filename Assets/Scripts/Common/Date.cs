using System;

[Serializable]
public class Date
{
    public Date() { }
    public Date(DateTime now)
    {
        year = now.Year;
        month = now.Month;
        day = now.Day;
    }

    public int year;
    public int month;
    public int day;
}
