using Contract.Abstractions.Messages;
using Contract.Services.Branch.Create;
using Contract.Services.Representative.Create;


namespace Contract.Services.Business.Create;
public record SaveBusinessCommand(
    Guid id,
    string? TaxCode,
    string? Name,
    DateOnly DateOfEstablishments,
     string? WebSite,
    string? AvatarImage, string? CoverImage, List<Guid> IndustryIds,
    CreateRepresentativeCommand? RepresentativeSave, List<CreateBranchCommand> Branches) : ICommand;
