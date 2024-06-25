using Contract.Services.Participation.Share;
using Domain.Abstractioins.Enities;

namespace Domain.Entities;
public class Participation : EntityAuditBaseWithoutId
{
    public string BusinessId { get; set; }
    public Business Business { get; set; }
    public Guid EventId { get; set; }
    public Event Event { get; set; }
    public ParticipationStatus Status { get; set; }
    private Participation()
    {
    }
}
