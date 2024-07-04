namespace Application.Utils;
public class DateUtil
{
    public static DateTime FromDateTimeClientToDateTimeUtc(DateTime dateTimeClient)
    {
        return dateTimeClient.AddHours(-7).ToUniversalTime();
    }

    public static DateTime GetDateTimeForClient(DateTime dateTime)
    {
        return dateTime.AddHours(7);
    }
}
