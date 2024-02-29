using Server.Models;

namespace Server.Services;

public interface IEmailService
{
    public Task<bool> ConfirmRegisterAsync(User user, HashStorage storage);
    public Task<bool> ConfirmEmailAsync(User user, HashStorage storage);
    public Task<bool> ForgetPasswordAsync(User user, HashStorage storage);
}