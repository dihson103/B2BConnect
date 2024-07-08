using Contract.Abstractions.Messages;
using Contract.Services.Account.SharedDto;

namespace Contract.Services.Account.Create;

public record CreateAccountCommand(string Password, string Email) : ICommand;

