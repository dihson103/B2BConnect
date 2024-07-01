using Contract.Services.Event.Create;
using Contract.Services.Event.Share;
using Domain.Abstractioins.Enities;

namespace Domain.Entities;
public class Event : EntityAuditBase<int>
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
    public EventStatus Status { get; set; }
    public List<Participation>? Participations { get; set; }
    private Event()
    {
    }

    public static Event Create(CreateEventCommand request)
    {
        return new Event()
        {
            Name = request.Name,
            Description = request.Description,
            StartAt = request.StartAt,
            EndAt = request.EndAt,
        };
    }
}
