using Contract.Abstractions.Messages;

namespace Contract.Services.Verifications.Update;
public record UpdateVerificationCommand(
    Guid VerificationId) : ICommand;
