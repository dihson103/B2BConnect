using Contract.Services.Account.SharedDto;

namespace Contract.Services.User.Login;

public record LoginResponse(AccountResponse account, string AccessToken, string RefreshToken);
