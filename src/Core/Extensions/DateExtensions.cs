namespace Core.Extensions;

public static class DateExtensions
{
    public static DateTime StartOfDay(this DateTime theDate)
    {
        return theDate.Date;
    }
    public static DateTime EndOfDay(this DateTime theDate)
    {
        return theDate.Date.AddDays(1).AddTicks(-1);
    }

}