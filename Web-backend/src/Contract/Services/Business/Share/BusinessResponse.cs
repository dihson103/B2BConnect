using Contract.Services.Branch.Share;
using Contract.Services.Representative.Share;

namespace Contract.Services.Business.Share;
public record BusinessResponse(Guid Id,string TaxCode, string Name, 
    DateOnly DateOfEstablishmen, string? WebSite, string? AvatarImage
    , string? CoverImage, NumberOfEmployee NumberOfEmployee, bool IsVerified,
    RepresentativeResponse RepresentativeResponse, List<BranchResponse> BranchResponses);
