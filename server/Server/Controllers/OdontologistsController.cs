using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services;

namespace Server.Controllers
{
    [ApiController, Route("api/v1/odontologists"), Authorize]
    public class OdontologistsController(IOdontologistService service) : ControllerBase
    {
        private readonly IOdontologistService _service = service;

        [HttpGet]
        public ActionResult Get()
        {
            return _service.FindAll();
        }
    }
}