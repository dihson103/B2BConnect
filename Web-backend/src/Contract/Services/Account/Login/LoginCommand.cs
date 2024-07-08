using Contract.Abstractions.Messages;

namespace Contract.Services.Account.Login;

public record LoginCommand(string Email, string Password) : ICommand<LoginResponse>;
