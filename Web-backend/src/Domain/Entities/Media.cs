using Domain.Abstractioins.Enities;

namespace Domain.Entities;
public class Media : EntityAuditBase<Guid>
{
    public string Path { get; set; }
    public List<EventMedia>? EventMedias { get; set; }
    private Media() { }
    public static Media Create(string path, string createdBy)
    {
        return new()
        {
            Id = Guid.NewGuid(),
            Path = path,
            CreatedBy = createdBy,
            CreatedDate = DateTime.UtcNow,
        };
    }
}
