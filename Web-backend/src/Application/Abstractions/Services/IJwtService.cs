using System.Security.Claims;
using Domain.Entities;

namespace Application.Abstractions.Services;
public interface IJwtService
{
    //Task<string> CreateAccessToken(User user);
    //Task<string> CreateRefreshToken(User user);
    string? GetUserIdFromToken(string token);

    string GenerateAccessToken(Account account);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

}
