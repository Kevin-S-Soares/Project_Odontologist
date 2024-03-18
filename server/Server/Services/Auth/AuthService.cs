using System.Security.Claims;

namespace Server.Services;

public class AuthService : IAuthService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetGuid()
    {
        Guid result = Guid.Empty;
        if (_httpContextAccessor.HttpContext is not null)
        {
            _ = Guid.TryParse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out result);
        }
        return result;
    }
}
