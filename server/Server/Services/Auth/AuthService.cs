using System.Security.Claims;
using Server.Contexts;
using Server.Models;

namespace Server.Services;

public class AuthService (ApplicationContext _context, IHttpContextAccessor _httpContextAccessor) : IAuthService
{
    public User? GetRequester()
    {
        Guid result = Guid.Empty;
        if (_httpContextAccessor.HttpContext is not null)
        {
            _ = Guid.TryParse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out result);
        }
        return _context.Users.FirstOrDefault(search => search.Id == result);
    }

    public DateTime? GetExpirationDate()
    {
        DateTime? result = null;
        if (_httpContextAccessor.HttpContext is not null)
        {
            result = DateTimeOffset.FromUnixTimeSeconds(long.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue("exp")?? "")).Date;
        }
        return result;
    }
}
