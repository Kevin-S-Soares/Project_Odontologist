using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services;

namespace Server.Controllers
{
    [ApiController, Route("api/v1/[controller]"), Authorize]
    public class SchedulesController(IScheduleService service) : ControllerBase
    {
        private readonly IScheduleService _service = service;

        [HttpGet]
        public ActionResult Get()
        {
            return _service.FindAll();
        }
    }
}