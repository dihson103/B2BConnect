
namespace Contract.Services.Verification.Share;
public record VerificationResponse(string? BusinessLicense, string? EstablishmentCertificate, string? Note,
    BusinessType BusinessType, DateTime CreatedDate);
