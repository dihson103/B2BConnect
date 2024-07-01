using Contract.Services.Business.Share;
using Domain.Abstractioins.Enities;

namespace Domain.Entities;
public class Business : EntityBase<int>
{
    public string TaxCode { get; set; }
    public string Name { get; set; }
    public DateOnly DateOfEstablishment { get; set; }
    public string? WebSite { get; set; }
    public string? AvatarImage { get; set; }
    public string? CoverImage { get; set; }
    public NumberOfEmployee NumberOfEmployee { get; set; }
    public bool IsVerified { get; set; }
    public int AccountId { get; set; }
    public Account Account { get; set; }    
    public int? RepresentativeId { get; set; }
    public Representative? Representative { get; set; }
    public List<Sector>? Sectors { get; set; }
    public List<Participation>? Participations { get; set; }
    public List<Image>? Images { get; set; }
    private Business()
    {
    }
}
