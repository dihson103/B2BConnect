namespace Contract.Services.Business.Share;
public record BusinessesResponse(Guid Id, string TaxCode, string Name,
    DateOnly DateOfEstablishment, string? WebSite, string? AvatarImage
    , string? CoverImage, string NumberOfEmployee, bool IsVerified);
