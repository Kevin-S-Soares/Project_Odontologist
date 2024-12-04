using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services;

namespace Server.Controllers
{
    [ApiController, Route("api/v1/[controller]"), Authorize]
    public class BreakTimeController(IBreakTimeService service) : ControllerBase
    {
        private readonly IBreakTimeService _service = service;

        [HttpGet("{id}")]
        public ActionResult Get(long id)
        {
            return _service.FindById(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(BreakTime breakTime)
        {
            return await _service.CreateAsync(breakTime);
        }

        [HttpPut]
        public async Task<ActionResult> Put(BreakTime breakTime)
        {
            return await _service.UpdateAsync(breakTime);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(long id)
        {
            return await _service.DeleteAsync(id);
        }
    }
}