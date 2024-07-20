using Contract.Abstractions.Messages;

namespace Contract.Services.Representative.Create;
public record CreateRepresentativeCommand(string GovernmentId, string Fullname, DateOnly Dob,
    bool Gender, string Address) :ICommand;
