using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Models.Client;
using Server.Services;

namespace Server.Controllers;

[ApiController]
public class UserController(IUserService _service) : ControllerBase
{
    [HttpPost, Route("/api/v1/user")]
    public async Task<ActionResult> CreateAsync(ClientRegisterUser user) => await _service.CreateAsync(user);

    [HttpDelete, Route("/api/v1/user/{guid}"), Authorize]
    public async Task<ActionResult> RemoveById(Guid guid) => await _service.RemoveByIdAsync(guid);

    [HttpPost, Route("/api/v1/user/authenticate")]
    public ActionResult Authenticate(ClientAuthentication request) => _service.Authenticate(request);

    [HttpPost, Route("/api/v1/user/verify_registration")]
    public async Task<ActionResult> VerifyRegistration(ClientHashOperation request) => await _service.VerifyRegistrationAsync(request);

    [HttpPost, Route("/api/v1/user/forget_password")]
    public async Task<ActionResult> ForgetPasswordAsync(string email) => await _service.ForgetPasswordAsync(email);

    [HttpPost, Route("/api/v1/user/reset_password")]
    public async Task<ActionResult> ResetPasswordAsync(ClientResetPassword request) => await _service.ResetPasswordAsync(request);

    [HttpPost, Route("/api/v1/user/change_password"), Authorize]
    public async Task<ActionResult> ChangePasswordAsync(string password) => await _service.ChangePasswordAsync(password);

    [HttpGet, Route("/api/v1/user/{guid}"), Authorize]
    public ActionResult GetUserById(Guid guid) => _service.GetUserById(guid);

    [HttpGet, Route("api/v1/user/me"), Authorize]
    public ActionResult GetCurrentUser() => _service.GetCurrentUser();
}