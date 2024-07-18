namespace Domain.Entities;
public class EventMedia
{
    public Event Event { get; set; }
    public Guid EventId { get; set; }
    public Media Media { get; set; }
    public Guid MediaId { get; set; }
    public bool IsMain { get; set; }
    private EventMedia() { }    
    public static EventMedia Create(Guid eventId, Guid mediaId, bool isMain)
    {
        return new EventMedia()
        {
            EventId = eventId,
            MediaId = mediaId,
            IsMain = isMain
        };
    }
}
