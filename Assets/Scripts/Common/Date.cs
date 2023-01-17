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
    public Date(DateStr value)
    {
        year = Int32.Parse(value.year);
        month = Int32.Parse(value.month);
        day = Int32.Parse(value.day);
    }

    public int year;
    public int month;
    public int day;
    public string character
    {
        get
        {
            return $"{year}/{month}/{day}";
        }
    }
}

[Serializable]
public class DateStr
{
    public DateStr(Date value)
    {
        year = value.year.ToString();
        month = value.month.ToString();
        day = value.day.ToString();
    }

    public string year;
    public string month;
    public string day;
}
