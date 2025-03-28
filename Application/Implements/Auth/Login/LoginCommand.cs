using Application.Common.Model;
using MediatR;

namespace Application.Implements.Auth.Login
{
    public class LoginCommand : IRequest<LoginResult>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
    
}
