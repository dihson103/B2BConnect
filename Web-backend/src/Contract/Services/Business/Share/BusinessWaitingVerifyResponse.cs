
using Contract.Services.Verification.Share;

namespace Contract.Services.Business.Share;
public record BusinessWaitingVerifyResponse(Guid Id, string TaxCode, string Name, bool IsVerified
    , VerificationResponse VerificationResponse);

