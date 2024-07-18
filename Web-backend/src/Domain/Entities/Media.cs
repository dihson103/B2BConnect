using Domain.Abstractioins.Enities;

namespace Domain.Entities;
public class Media : EntityAuditBase<Guid>
{
    public string Path { get; set; }
    public List<EventMedia>? EventMedias { get; set; }
}
