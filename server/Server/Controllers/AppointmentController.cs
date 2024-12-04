using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services;

namespace Server.Controllers
{
    [ApiController, Route("api/v1/appointment"), Authorize]
    public class AppointmentController(IAppointmentService service) : ControllerBase
    {
        private readonly IAppointmentService _service = service;

        [HttpGet]
        public ActionResult Get()
        {
            return _service.FindAll();
        }

        [HttpGet("{id}")]
        public ActionResult Get(long id)
        {
            return _service.FindById(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Appointment appointment)
        {
            return await _service.CreateAsync(appointment);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Appointment appointment)
        {
            return await _service.UpdateAsync(appointment);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(long id)
        {
            return await _service.DeleteAsync(id);
        }
    }
}