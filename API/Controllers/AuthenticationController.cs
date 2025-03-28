using Application.Common.Interface;
using Application.Common.Model;
using Application.Implements.Auth.ChangePassword;
using Application.Implements.Auth.Login;
using Application.Implements.Auth.SetAvatar;
using Application.Implements.Auth.UserLogin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class AuthenticationController : ApiControllerBase
    {
        private readonly IJwtTokenGenerator _jwt;
        private readonly HttpClient _httpClient;
        public AuthenticationController(IJwtTokenGenerator jwt, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jwt = jwt;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResult>> LoginAsync (LoginCommand command)
        {            
            var result = await Mediator.Send(command);
            return Ok(result);                        
        }
        [Authorize]
        [HttpGet("user")]
        public async Task<IActionResult> GetUser()
        {
            string auth = Request.Headers.Authorization.ToString();

            Task<string> taskstring = _jwt.DeGenerate(auth, "sub");

            string stringId = await taskstring;
            var Id = Guid.Parse(stringId);            

            if (auth == null)
            {
                return Unauthorized();
            }
            else
            {
                var jwt = auth.Split(" ")[1];
                var user = await Mediator.Send(new UserLogin() { UserId = Id });
                return Ok(user);
            }
        }
        [Authorize]
        [HttpPut("change-password")] 
        public async Task<IActionResult> ChangePassword([FromBody]PasswordUpdateCommand command)
        {
            string auth = Request.Headers.Authorization.ToString();
            Task<string> taskstring = _jwt.DeGenerate(auth, "sub");

            string stringId = await taskstring;

            var userId = Guid.Parse(stringId);
            command.SetId(userId);
            Console.WriteLine(command);
            return Ok(await Mediator.Send(command));
        }

        [Authorize]
        [HttpPut("change-avatar")]
        public async Task<IActionResult> ChangeAvatar(IFormFile file)
        {
            string auth = Request.Headers.Authorization.ToString();
            Task<string> taskstring = _jwt.DeGenerate(auth, "sub");
            string stringId = await taskstring;
            var userId = Guid.Parse(stringId);


            //command.SetId(userId);
            var result = await Mediator.Send(new SetAvatarCommand { EmployeeId = userId, File = file});
            return Ok(result);
        }


    }
}
