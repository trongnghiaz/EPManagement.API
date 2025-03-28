using Application.Common.Interface;
using Application.Implements.Salaries.GetListSalaries;
using Application.Implements.Salaries.SalaryCounting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : ApiControllerBase
    {
        private readonly IJwtTokenGenerator _jwt;
        public SalaryController(IJwtTokenGenerator jwt)
        {
            _jwt = jwt;
        }
        [HttpPost("counting")]
        public async Task<IActionResult> Counting()
        {
            string auth = Request.Headers.Authorization.ToString();

            Task<string> taskstring = _jwt.DeGenerate(auth, "sub");

            string stringId = await taskstring;
            var Id = Guid.Parse(stringId);
            var response = await Mediator.Send(new SalaryCountingCommand(Id));
            return Ok(response);
        }
        [HttpGet("list")]
        public async Task<IActionResult> List([FromQuery] ListSalariesQuery query)
        {
            string auth = Request.Headers.Authorization.ToString();

            Task<string> taskstring = _jwt.DeGenerate(auth, "sub");

            string stringId = await taskstring;
            var Id = Guid.Parse(stringId);
            query.SetId(Id);
            var response = await Mediator.Send(query);
            return Ok(response);
        }
    }
}
