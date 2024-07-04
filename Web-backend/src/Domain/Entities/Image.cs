using Domain.Abstractioins.Enities;

namespace Domain.Entities;
public class Image : EntityBase<Guid>
{
    public string Value { get; set; }
    public Guid BusinessId { get; set; }
    public Business Business { get; set; }
}
