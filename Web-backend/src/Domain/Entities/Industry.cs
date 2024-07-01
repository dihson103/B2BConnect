using Domain.Abstractioins.Enities;

namespace Domain.Entities;
public class Industry : EntityBase<int>
{
    public string Name { get; set; }
    public List<Sector>? Sectors { get; set; }
    private Industry()
    {
    }
}
