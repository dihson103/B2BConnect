using Domain.Abstractioins.Enities.Base;

namespace Domain.Abstractioins.Enities;
public abstract class EntityAuditBaseWithoutId : IAuditable
{
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}
