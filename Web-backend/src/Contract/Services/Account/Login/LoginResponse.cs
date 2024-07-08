using Contract.Services.Account.SharedDto;

namespace Contract.Services.Account.Login;

public record LoginResponse(AccountResponse account, string AccessToken, string RefreshToken);
