using Application.Common.Interface;
using Application.Implements.Attendances.CheckIn;
using Application.Implements.Attendances.CheckOut;
using Application.Implements.Attendances.GetAllMonthlyAttendance;
using Application.Implements.Attendances.GetAllWeeklyAttendance;
using Application.Implements.Attendances.GetMonthlyAttendance;
using Application.Implements.Attendances.GetWeeklyAttendace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AttendanceController : ApiControllerBase
    {
        private readonly IJwtTokenGenerator _jwt;

        public AttendanceController(IJwtTokenGenerator jwt)
        {
            _jwt = jwt;
        }
        [HttpPost("check-in")]
        public async Task<IActionResult> CheckIn([FromBody]CheckInCommand command)
        {
            var checkInCommand = await Mediator.Send(command);
            return Ok(checkInCommand);
        }
        [HttpPost("check-out")]
        public async Task<IActionResult> CheckOut([FromBody]CheckOutCommand command)
        {
            var checkOutCommand = await Mediator.Send(command);
            return Ok(checkOutCommand);
        }
        [HttpGet("weekly-list-attendance")]
        public async Task<IActionResult> WeeklyListAttendance()
        {
            string auth = Request.Headers.Authorization.ToString();

            Task<string> taskstring = _jwt.DeGenerate(auth, "sub");

            string stringId = await taskstring;
            var Id = Guid.Parse(stringId);
            var weeklyListAttendanceQuery = await Mediator.Send(new WeeklyAttendanceQuerry(Id));
            return Ok(weeklyListAttendanceQuery);
        }
        [HttpGet("monthly-list-attendance")]
        public async Task<IActionResult> MonthlyListAttendance([FromQuery]MonthlyAttendanceQuery query)
        {
            string auth = Request.Headers.Authorization.ToString();

            Task<string> taskstring = _jwt.DeGenerate(auth, "sub");

            string stringId = await taskstring;
            var Id = Guid.Parse(stringId);
            query.SetId(Id);
            var monthlyListAttendanceQuery = await Mediator.Send(query);
            return Ok(monthlyListAttendanceQuery);
        }
        [HttpGet("all-weekly-attendance")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AllWeeklyListAttendance([FromQuery]AllWeeklyAttendanceQuery query)
        {
            var allWeeklyListAttendanceQuery = await Mediator.Send(query);
            return Ok(allWeeklyListAttendanceQuery);
        }
        [HttpGet("all-monthly-attendance")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AllMonthlyListAttendance([FromQuery] AllMonthlyAttendanceQuery query)
        {
            var allMonthlyListAttendanceQuery = await Mediator.Send(query);
            return Ok(allMonthlyListAttendanceQuery);
        }
    }
}
