using Contract.Abstractions.Messages;

namespace Contract.Services.Account.Logout;

public record LogoutCommand(string currentUserId, string logoutUserId) : ICommand;
