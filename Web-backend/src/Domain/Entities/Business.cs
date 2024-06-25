using Contract.Services.Business.Share;
using Domain.Abstractioins.Enities;

namespace Domain.Entities;
public class Business : EntityBase<string>
{
    public string Name { get; set; }
    public DateOnly DateOfEstablishment { get; set; }
    public string WebSite { get; set; }
    public string AvatarImage { get; set; }
    public string CoverImage { get; set; }
    public NumberOfEmployee NumberOfEmployee { get; set; }
    public bool IsVerified { get; set; }
    public Guid AccountId { get; set; }
    public Account Account { get; set; }    
    public string RepresentativeId { get; set; }
    public Representative Representative { get; set; }
    public List<Sector>? Sectors { get; set; }
    public List<Participation>? Participations { get; set; }
    private Business()
    {
    }
}
