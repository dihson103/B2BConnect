using Application.Abstractions.Services;

namespace WebApi.Services;

public class RequestContextService : IRequestContext
{
    private readonly HttpContext _httpContext;

    public RequestContextService(IHttpContextAccessor accessor)
    {
        _httpContext = accessor.HttpContext;
    }

    public string UserLoggedIn => _httpContext?.User?.FindFirst("UserID")?.Value ?? string.Empty;
}
