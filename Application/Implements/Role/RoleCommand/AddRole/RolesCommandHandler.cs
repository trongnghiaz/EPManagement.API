using Application.Common.Handler;
using Application.Common.Interface;
using Domain.Entities;
using Domain.Helper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Implements.Role.RoleCommand.AddRole
{
    public class RolesCommandHandler : BaseHandler<RolesCommand, RoleCommandResult>
    {
        private readonly ISender _mediator;
        public RolesCommandHandler(ISender mediator, IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext) : base(writeDbcontext, readDbcontext)
        {
            _mediator = mediator;
        }
        public override async Task<RoleCommandResult> Handle(RolesCommand request, CancellationToken cancellationToken)
        {
            var checkexist = await _readDbcontext.Roles.AnyAsync(r => r.Id == request.roleId);

            if (!checkexist)
            {
                return new RoleCommandResult(false,"Role chưa tồn tại");
            }

            var userExist = await _readDbcontext.Employees.Where(e => e.Email == request.email).FirstOrDefaultAsync();
            if(userExist is not null)
            {
                if (await _readDbcontext.EmployeeRoles.AnyAsync(e => e.EmployeesEmployeeId == userExist.EmployeeId))
                {
                    return new RoleCommandResult(false, "Nhân sự này đã được cấp quyền");
                }
                var role = new EmployeeRoles { EmployeesEmployeeId = userExist.EmployeeId, RolesId = request.roleId };
                await _writeDbcontext.EmployeeRoles.AddAsync(role);
                await _writeDbcontext.SaveChangesAsync();
                return new RoleCommandResult(true, Message.CreatedSuccess);
            }
            else
            {
                return new RoleCommandResult(false, Message.NotExistEmail);
            }                                    
            
        }
    }
}
