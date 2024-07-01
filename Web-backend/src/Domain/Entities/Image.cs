using Domain.Abstractioins.Enities;

namespace Domain.Entities;
public class Image : EntityBase<int>
{
    public string Value { get; set; }
    public int BusinessId { get; set; }
    public Business Business { get; set; }
}
