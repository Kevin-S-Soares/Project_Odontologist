using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Services;

namespace Server.Controllers;

[ApiController]
public class UsersController(IUserService _service) : ControllerBase
{

    [HttpGet, Route("/api/v1/users"), Authorize]
    public ActionResult GetAll(string? name = null) => _service.GetAll(name);

    [HttpGet, Route("api/v1/users/others"), Authorize]
    public ActionResult GetOtherUsers() => _service.GetOtherUsers();
}