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

    public bool IsAdmin()
    {
        User? user = GetRequester();
        return user is not null && user.Role == Role.ADMIN;
    }

    public bool IsOdontologist()
    {
        User? user = GetRequester();
        return user is not null && user.Role == Role.ODONTOLOGIST;
    }

    public bool IsAttendant()
    {
        User? user = GetRequester();
        return user is not null && user.Role == Role.ATTENDANT;
    }

    public long GetContextID()
    {
        User? user = GetRequester();
        return user is not null && user.ContextId is not null? user.ContextId.Value: -1L ;
    }

    public bool IsGuest()
    {
        User? user = GetRequester();
        return user is not null && user.Role == Role.GUEST;
    }
}
