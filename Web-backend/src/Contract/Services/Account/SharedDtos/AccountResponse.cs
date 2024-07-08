namespace Contract.Services.Account.SharedDto;

public record AccountResponse
(
    string Id,
    string Email,
    int RoleId
);
