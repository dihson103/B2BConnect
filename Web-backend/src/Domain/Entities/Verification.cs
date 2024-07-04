using Contract.Services.Verification.Share;
using Domain.Abstractioins.Enities;

namespace Domain.Entities;
public class Verification : EntityBase<Guid>
{
    public string BusinessLicense {  get; set; }
    public string EstablishmentCertificate { get; set; }
    public string? Note {  get; set; }
    public bool IsChecked { get; set; }
    public Guid BusinessId { get; set; }
    public DateTime? CheckedDate { get; set; }
    public BusinessType BusinessType { get; set; }
    public Business Business { get; set; }
}
