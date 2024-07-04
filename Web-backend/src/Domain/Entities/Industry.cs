using Domain.Abstractioins.Enities;

namespace Domain.Entities;
public class Industry : EntityBase<Guid>
{
    public string Name { get; set; }
    public List<Sector>? Sectors { get; set; }
    public List<EventIndustry>? EventIndustries { get; set; }
    private Industry()
    {
    }

    public static Industry Create(string name)
    {
        return new Industry()
        {
            Id = Guid.NewGuid(),
            Name = name,
        };
    }
}
