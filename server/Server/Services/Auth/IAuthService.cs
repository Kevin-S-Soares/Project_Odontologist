using Server.Models;

namespace Server.Services;

public interface IAuthService 
{
    public User? GetRequester();
    public DateTime? GetExpirationDate();
}