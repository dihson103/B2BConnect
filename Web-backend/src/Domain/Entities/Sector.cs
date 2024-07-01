namespace Domain.Entities;
public class Sector
{
    public int BusinessId { get; set; }
    public Business Business { get; set; }
    public int IndustryId { get; set; }
    public Industry Industry { get; set; }
}
