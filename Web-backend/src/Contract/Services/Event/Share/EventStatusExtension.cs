namespace Contract.Services.Event.Share;
public static class EventStatusExtension
{
    private static readonly Dictionary<EventStatus, string> _statusDescriptions = new() 
    { 
        {EventStatus.PLANNING , "Sắp diễn ra" },
        {EventStatus.ONGOING , "Đang diễn ra" },
        {EventStatus.FINISHED , "Đã kết thúc" },
        {EventStatus.CANCELLED , "Đã bị hủy" },
    };

    public static string GetDescription(this EventStatus status)
    {
        return _statusDescriptions[status];
    }
}
