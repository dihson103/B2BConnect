using Contract.Services.Verification.Share;
using Contract.Services.Verifications.Create;
using Domain.Abstractioins.Enities;

namespace Domain.Entities;
public class Verification : EntityAuditBase<Guid>
{
    public string BusinessLicense {  get; set; }
    public string EstablishmentCertificate { get; set; }
    public string? Note {  get; set; }
    public bool IsChecked { get; set; }
    public Guid BusinessId { get; set; }
    public DateTime? CheckedDate { get; set; }
    public BusinessType BusinessType { get; set; }
    public Business Business { get; set; }

    public static Verification Create(CreateVerificationCommand command)
    {
        return new Verification()
        {
            BusinessLicense = command.businessLicense,
            EstablishmentCertificate = command.establishmentCertificate,
            BusinessType = command.businessType,
            IsChecked = false,
            Note = command.note,
            BusinessId = command.BusinessId,
            Id = Guid.NewGuid(),
            CheckedDate = DateTime.UtcNow,
        };
    }
}
