using Application.Common.Handler;
using Application.Common.Interface;
using Domain.Helper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Implements.Auth.ChangePassword
{
    public class PasswordUpdateCommandHandler : BaseHandler<PasswordUpdateCommand, PasswordUpdateResult>
    {
        private readonly ISender _mediator;
        public PasswordUpdateCommandHandler(ISender mediator, IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext) : base(writeDbcontext, readDbcontext)
        {
            _mediator = mediator;
        }
        public override async Task<PasswordUpdateResult> Handle(PasswordUpdateCommand request, CancellationToken cancellationToken)
        {
            var checkUser = await _readDbcontext.Employees.Where(e => e.EmployeeId == request.Id).FirstOrDefaultAsync(cancellationToken);
            if (checkUser is null) return new PasswordUpdateResult(false, Message.UserNotFound);
            if(checkUser.EmployeeId == Guid.Parse(Admin.AdminId)) return new PasswordUpdateResult(false, Message.DefaultRecord);
            bool checkPass = BCrypt.Net.BCrypt.Verify(request.password, checkUser.Password);
            if (checkPass)
            {
                var paswordHash = BCrypt.Net.BCrypt.HashPassword(request.newPassword);
                var update = await _writeDbcontext.Employees
                    .Where(e => e.EmployeeId == request.Id)
                    .ExecuteUpdateAsync(u => u
                    .SetProperty(p => p.Password, paswordHash));
                return new PasswordUpdateResult(true, Message.UpdatedSuccess);                
            }
            else
            {
                return new PasswordUpdateResult(false, Message.FailedInformation);
            }                            
        }
    }
}
