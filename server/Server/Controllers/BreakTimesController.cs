using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services;

namespace Server.Controllers
{
    [ApiController, Route("api/v1/breakTimes"), Authorize]
    public class BreakTimesController(IBreakTimeService service) : ControllerBase
    {
        private readonly IBreakTimeService _service = service;

        [HttpGet]
        public ActionResult Get(long? scheduleId, string? name, DayOfWeek? startDay, TimeSpan? startTime, DayOfWeek? endDay, TimeSpan? endTime)
        {
            return _service.FindAll(scheduleId, name, startDay, startTime, endDay, endTime);
        }
    }
}