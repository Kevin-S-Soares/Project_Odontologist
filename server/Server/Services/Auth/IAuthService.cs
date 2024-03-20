using Server.Models;

namespace Server.Services;

public interface IAuthService 
{
    public Guid GetGuid();
    public Role GetRole();
}