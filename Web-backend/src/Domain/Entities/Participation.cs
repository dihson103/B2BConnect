using Contract.Services.Participation.Share;

namespace Domain.Entities;
public class Participation
{
    public Guid BusinessId { get; set; }
    public Business Business { get; set; }
    public Guid EventId { get; set; }
    public Event Event { get; set; }
    public DateOnly JoinDate { get; set; }
    public ParticipationStatus Status { get; set; }
    private Participation()
    {
    }

    public static Participation Create(Guid businessId, Guid eventId, bool isBusinessSectorsInEventIndustries)
    {
        return new Participation()
        {
            EventId = eventId,
            BusinessId = businessId,
            JoinDate = DateOnly.FromDateTime(DateTime.UtcNow.AddHours(7)),
            Status = isBusinessSectorsInEventIndustries ? ParticipationStatus.JOINED : ParticipationStatus.WAITING_FOR_APPROVAL,
        };
    }
}
