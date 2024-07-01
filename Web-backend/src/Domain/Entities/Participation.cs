using Contract.Services.Participation.Share;
using Domain.Abstractioins.Enities;

namespace Domain.Entities;
public class Participation : EntityAuditBaseWithoutId
{
    public int BusinessId { get; set; }
    public Business Business { get; set; }
    public int EventId { get; set; }
    public Event Event { get; set; }
    public DateOnly JoinDate { get; set; }
    public ParticipationStatus Status { get; set; }
    private Participation()
    {
    }
}
