namespace Domain.Entities;
public class EventMedia
{
    public Event Event { get; set; }
    public Guid EventId { get; set; }
    public Media Media { get; set; }
    public Guid MediaId { get; set; }
}
