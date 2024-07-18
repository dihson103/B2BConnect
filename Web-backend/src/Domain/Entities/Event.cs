using Contract.Services.Event.Create;
using Contract.Services.Event.Share;
using Contract.Services.Event.Update;
using Domain.Abstractioins.Enities;

namespace Domain.Entities;
public class Event : EntityAuditBase<Guid>
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
    public EventStatus Status { get; set; }
    public string Location { get; set; }
    public List<EventMedia>? EventMedias { get; set; }
    public List<Participation>? Participations { get; set; }
    public List<EventIndustry>? EventIndustries { get; set; }
    private Event()
    {
    }

    public static Event Create()
    {
        return new();
    }

    public static Event Create(CreateEventCommand request)
    {
        return new Event()
        {
            Name = request.Name,
            Description = request.Description,
            StartAt = request.StartAt,
            EndAt = request.EndAt,
            Status = EventStatus.PLANNING,
            Location = request.Location,
            Id = Guid.NewGuid(),
        };
    }

    public void Update(UpdateEventRequest request)
    {
        var newEventIndustries = request.IndustryIds
            .Select(industryId => EventIndustry.Create(Id, industryId))
            .ToList();

        Name = request.Name;
        Description = request.Description;
        StartAt = request.StartAt;
        EndAt = request.EndAt;
        Status = request.Status;
        Location = request.Location;
        EventIndustries = newEventIndustries;
    }

    public void UpdateStatus(EventStatus status)
    {
        Status = status;
    }
}
