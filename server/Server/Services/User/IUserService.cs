using Server.Models;
using Server.Models.Client;

namespace Server.Services;

public interface IUserService {
    public Task<ServiceResponse<User>> CreateAsync(ClientUser request);
    public Task<ServiceResponse<bool>> VerifyRegistrationAsync(ClientHashOperation request);
    public ServiceResponse<IEnumerable<User>> GetAll();
    public Task<ServiceResponse<bool>> RemoveByIdAsync(Guid id);
    public ServiceResponse<string> Authenticate(ClientAuthentication request);
    public Task<ServiceResponse<bool>> ForgetPasswordAsync(string email);
    public Task<ServiceResponse<bool>> ResetPasswordAsync(ClientResetPassword request);
    public Task<ServiceResponse<bool>> ChangePasswordAsync(string request);
}