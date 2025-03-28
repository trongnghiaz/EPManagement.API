using Application.Common.Handler;
using Application.Common.Interface;
using Domain.Helper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Implements.Role.RoleCommand.ChangeRole
{
    public class UpdateRoleCommandHandler : BaseHandler<UpdateRoleCommand, RoleCommandResult>
    {
        private readonly ISender _mediator;
        public UpdateRoleCommandHandler(IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext, ISender mediator) : base(writeDbcontext, readDbcontext)
        {
            _mediator = mediator;
        }
        public override async Task<RoleCommandResult> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {                                    
            var check = await _readDbcontext.EmployeeRoles.Where(e => e.EmployeesEmployeeId == request.EmployeeId).AnyAsync();
            if (request.EmployeeId == Guid.Parse(Admin.AdminId)) return new RoleCommandResult(false, Message.DefaultRecord);

            if (!check)
            {                                
                return new RoleCommandResult(false, "Nhân sự chưa có quyền truy cập, vui lòng cấp quyền trước");                                           
            }
            else
            {
                var update = await _writeDbcontext.EmployeeRoles
                .Where(er => er.EmployeesEmployeeId == request.EmployeeId)
                .ExecuteUpdateAsync(u => u.SetProperty(r => r.RolesId, request.roleId));
                return new RoleCommandResult(true,"Cập nhật vai trò thành công");
            }
        }
    }
}
