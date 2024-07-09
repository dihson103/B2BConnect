namespace Domain.Entities;
public class Sector
{
    public Guid BusinessId { get; set; }
    public Business Business { get; set; }
    public Guid IndustryId { get; set; }
    public Industry Industry { get; set; }

    private Sector() { }
    public static Sector Create(Guid businessId, Guid industryId)
    {
        return new Sector() {  BusinessId = businessId, IndustryId = industryId };
    }
}
