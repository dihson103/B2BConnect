using Contract.Abstractions.Messages;

namespace Contract.Services.User.Logout;

public record LogoutCommand(string currentUserId, string logoutUserId) : ICommand;
