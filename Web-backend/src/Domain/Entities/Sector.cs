namespace Domain.Entities;
public class Sector
{
    public string BusinessId { get; set; }
    public Business Business { get; set; }
    public Guid IndustryId { get; set; }
    public Industry Industry { get; set; }
}
