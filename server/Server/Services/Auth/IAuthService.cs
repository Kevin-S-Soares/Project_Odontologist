using Server.Models;

namespace Server.Services;

public interface IAuthService 
{
    public User? GetRequester();
    public DateTime? GetExpirationDate();
    public long GetContextID();
    public bool IsAdmin();
    public bool IsOdontologist();
    public bool IsAttendant();
    public bool IsGuest();
}