using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services;

namespace Server.Controllers
{
    [ApiController, Route("api/v1/odontologist"), Authorize]
    public class OdontologistController(IOdontologistService service) : ControllerBase
    {
        private readonly IOdontologistService _service = service;

        [HttpGet("{id}")]
        public ActionResult Get(long id)
        {
            return _service.FindById(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Odontologist odontologist)
        {
            return await _service.CreateAsync(odontologist);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Odontologist odontologist)
        {
            return await _service.UpdateAsync(odontologist);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(long id)
        {
            return await _service.DeleteAsync(id);
        }
    }
}