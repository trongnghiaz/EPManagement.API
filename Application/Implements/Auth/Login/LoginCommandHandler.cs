using Application.Common.Handler;
using Application.Common.Interface;
using Application.Common.Model;
using Domain.Helper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Implements.Auth.Login
{
    public class LoginCommandHandler : BaseHandler<LoginCommand, LoginResult>
    {
        private readonly ISender _mediator;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public LoginCommandHandler(IJwtTokenGenerator jwtTokenGenerator, ISender mediator, IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext) : base(writeDbcontext, readDbcontext)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _mediator = mediator;
        }
        public override async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {            
            var getUser = await _writeDbcontext.Employees.Where(e => e.Email == request.Email).FirstOrDefaultAsync(cancellationToken);
            if (getUser == null) return new LoginResult(false, Message.LoginFailed);
            if (!getUser.IsActive) return new LoginResult(false, Message.AccessDenied);
            if(getUser.IsDeleted) return new LoginResult(false, Message.AccessDenied);
            bool checkPass = BCrypt.Net.BCrypt.Verify(request.Password, getUser.Password);
            if (checkPass)
            {
                var token = await _jwtTokenGenerator.GenerateJwtToken(getUser);
                return new LoginResult(true, Message.LoginSuccess, token);
            }
            else
            {
                return new LoginResult(false, Message.LoginFailed);
            }            
        }
    }
}
