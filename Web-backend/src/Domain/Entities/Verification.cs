using Domain.Abstractioins.Enities;

namespace Domain.Entities;
public class Verification : EntityAuditBase<Guid>
{
    public string BusinessLicense {  get; set; }
    public bool IsChecked { get; set; }
    public string BusinessId { get; set; }
    public Business Business { get; set; }
}
