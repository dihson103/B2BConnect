using Contract.Abstractions.Messages;
using Contract.Services.Branch.Create;
using Contract.Services.Business.Share;
using Contract.Services.Representative.Create;


namespace Contract.Services.Business.Create;
public record SaveBusinessCommand(
    Guid accountId,
    string? TaxCode,
    string? Name,
    DateOnly DateOfEstablishments,
     string? WebSite,
    string? AvatarImage, string? CoverImage,NumberOfEmployee NumberOfEmployee, List<Guid> IndustryIds,
    CreateRepresentativeCommand? RepresentativeSave, List<CreateBranchCommand> Branches) : ICommand;
