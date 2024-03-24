using Server.Models;
using Server.Models.Client;

namespace Server.Services;

public interface IUserService {
    public Task<ServiceResponse<User>> CreateAsync(ClientRegisterUser request);
    public Task<ServiceResponse<bool>> VerifyRegistrationAsync(ClientHashOperation request);
    public ServiceResponse<IEnumerable<User>> GetAll(string? name);
    public Task<ServiceResponse<bool>> RemoveByIdAsync(Guid id);
    public Task<ServiceResponse<string>> AuthenticateAsync(ClientAuthentication request);
    public Task<ServiceResponse<bool>> ForgetPasswordAsync(string email);
    public Task<ServiceResponse<bool>> ResetPasswordAsync(ClientResetPassword request);
    public Task<ServiceResponse<bool>> ChangePasswordAsync(string request);
    public ServiceResponse<User> GetUserById(Guid guid);
    public ServiceResponse<User> GetCurrentUser();
    public ServiceResponse<IEnumerable<User>> GetOtherUsers();
    public Task<ServiceResponse<bool>> UpdateUserAsync(ClientUpdateUser request);
    public Task<ServiceResponse<bool>> ChangeEmailAsync(string request);
    public Task<ServiceResponse<bool>> ConfirmEmailChangeAsync(ClientHashOperation request);
}