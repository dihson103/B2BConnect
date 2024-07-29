using Contract.Services.Account.SharedDto;
using Contract.Services.Branch.Share;
using Contract.Services.Representative.Share;
using Contract.Services.Sector.Share;

namespace Contract.Services.Business.Share;
public record BusinessResponse(Guid Id, string TaxCode, string Name, 
    DateOnly DateOfEstablishment, string? WebSite, string? AvatarImage
    , string? CoverImage,NumberOfEmployee NumberOfEmployee, string NumberOfEmployeeStr, bool IsVerified, AccountResponse AccountResponse,
    RepresentativeResponse RepresentativeResponse, List<BranchResponse> BranchResponses, List<SectorResponse> SectorResponses);
