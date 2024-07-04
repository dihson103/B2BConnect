namespace Domain.Entities;
public class EventIndustry
{
    public Guid EventId { get; set; }
    public Guid IndustryId { get; set; }
    public Event Event { get; set; }
    public Industry Industry { get; set; }
    private EventIndustry() { }
    public static EventIndustry Create(Guid eventId, Guid industryId)
    {
        return new()
        {
            EventId = eventId,
            IndustryId = industryId
        };
    }
}
