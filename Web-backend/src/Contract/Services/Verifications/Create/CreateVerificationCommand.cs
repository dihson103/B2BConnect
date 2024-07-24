using Contract.Abstractions.Messages;
using Contract.Services.Business.Share;
using Contract.Services.Verification.Share;

namespace Contract.Services.Verifications.Create;
public record CreateVerificationCommand(
    Guid BusinessId, string name, string website, string email, string phone, DateOnly dateOfEstablishment,
    string address, string taxNumber, BusinessType businessType, string representativeName,
    string govermentId, NumberOfEmployee NumberOfEmployee,
     string businessLicense, string establishmentCertificate, string note
    ) : ICommand;
