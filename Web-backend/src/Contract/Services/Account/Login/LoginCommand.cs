using Contract.Abstractions.Messages;

namespace Contract.Services.User.Login;

public record LoginCommand(string Id, string Password) : ICommand<LoginResponse>;
