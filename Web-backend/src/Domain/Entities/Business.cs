using System.ComponentModel.DataAnnotations.Schema;
using Contract.Services.Business.Create;
using Contract.Services.Business.Share;
using Domain.Abstractioins.Enities;

namespace Domain.Entities;
public class Business : EntityAuditBase<Guid>
{
    public string TaxCode { get; set; }
    public string Name { get; set; }
    public DateOnly DateOfEstablishment { get; set; }
    public string? WebSite { get; set; }
    public string? AvatarImage { get; set; }
    public string? CoverImage { get; set; }
    public NumberOfEmployee NumberOfEmployee { get; set; }
    public bool IsVerified { get; set; }
    [ForeignKey(nameof(Account))]
    public Guid AccountId { get; set; }
    public Account Account { get; set; }    
    public Guid? RepresentativeId { get; set; }
    public Representative? Representative { get; set; }
    public List<Sector>? Sectors { get; set; }
    public List<Participation>? Participations { get; set; }
    public List<Branch>? Branches { get; set; }

    public static Business Create(CreateBusinessCommand businessCommand)
    {
        var Id = Guid.NewGuid();
        var sectors = businessCommand.IndustryIds!
           .Select(industryId => Sector.Create(Id, industryId))
           .ToList();
        var branches = businessCommand.Branches.Select(b => Branch.Create(b)).ToList();

        return new Business()
        {
            Id = Id,
            TaxCode = businessCommand.TaxCode!,
            Name = businessCommand.Name!,
            DateOfEstablishment = businessCommand.DateOfEstablishments,
            WebSite = businessCommand.WebSite,
            AvatarImage = businessCommand.AvatarImage,
            CoverImage = businessCommand.CoverImage,
            Sectors = sectors,
            Representative = Representative.Create(businessCommand.Representative),
            Branches = branches
        };
    }
}
