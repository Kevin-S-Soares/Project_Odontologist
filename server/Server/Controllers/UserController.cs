using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Models.Client;
using Server.Services;

namespace Server.Controllers;

[ApiController]
public class UsersController(IUserService service) : ControllerBase
{
    private readonly IUserService _service = service;

    [HttpPost, Route("/api/v1/user")]
    public async Task<ActionResult> CreateAsync(ClientUser user) =>
        await _service.CreateAsync(user);


    [HttpGet, Route("/api/v1/users"), Authorize]
    public ActionResult GetAll() => _service.GetAll();

    [HttpDelete, Route("/api/v1/user/{id}"), Authorize]
    public async Task<ActionResult> RemoveById(Guid id) => await _service.RemoveByIdAsync(id);

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
}