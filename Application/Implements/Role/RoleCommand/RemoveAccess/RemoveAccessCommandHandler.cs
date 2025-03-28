using Application.Common.Handler;
using Application.Common.Interface;
using Domain.Helper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Implements.Role.RoleCommand.RemoveAccess
{
    public class RemoveAccessCommandHandler : BaseHandler<RemoveAccessCommand, RoleCommandResult>
    {
        private readonly ISender _mediator;
        public RemoveAccessCommandHandler(ISender mediator, IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext) : base(writeDbcontext, readDbcontext)
        {
            _mediator = mediator;
        }
        public override async Task<RoleCommandResult> Handle(RemoveAccessCommand request, CancellationToken cancellationToken)
        {
            if (!await _readDbcontext.Employees.AnyAsync(x => x.EmployeeId == request.id)) return new RoleCommandResult(false, "Nhân sự này không tồn tại");            

            var access = await _readDbcontext.EmployeeRoles.Where(e => e.EmployeesEmployeeId == request.id).FirstOrDefaultAsync();
            if (access != null)
            {
                _writeDbcontext.EmployeeRoles.Remove(access);
                await _writeDbcontext.SaveChangesAsync();
                return new RoleCommandResult(true, Message.ActionSuccess);
            }
            else
            {
                return new RoleCommandResult(false, "Chưa tồn tại truy cập này");
            }
        }
    }
}
