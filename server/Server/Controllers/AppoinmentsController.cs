using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services;

namespace Server.Controllers
{
    [ApiController, Route("api/v1/appointments"), Authorize]
    public class AppointmentsController(IAppointmentService service) : ControllerBase
    {
        private readonly IAppointmentService _service = service;

        [HttpGet]
        public ActionResult Get()
        {
            return _service.FindAll();
        }
    }
}