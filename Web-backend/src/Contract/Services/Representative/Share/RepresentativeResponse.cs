
namespace Contract.Services.Representative.Share;
public record RepresentativeResponse(Guid Id, string GovernmentId, string Fullname, DateOnly Dob, bool Gender, 
    string Address, Guid BusinessId);
