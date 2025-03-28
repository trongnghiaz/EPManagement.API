
using Application.Common.Handler;
using Application.Common.Interface;
using Domain.Helper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Implements.Role.RoleCommand.RemoveRolePermission
{
    public class RemoveRolePermissionCommandHandler : BaseHandler<RemoveRolePermissionCommand, RoleCommandResult>
    {
        private readonly ISender _mediator;
        public RemoveRolePermissionCommandHandler(ISender mediator, IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext) : base(writeDbcontext, readDbcontext)
        {
            _mediator = mediator;
        }

        public override async Task<RoleCommandResult> Handle(RemoveRolePermissionCommand request, CancellationToken cancellationToken)
        {
            if (!await _readDbcontext.Roles.AnyAsync(x => x.Id == request.RoleId)) return new RoleCommandResult(false, "Role này không tồn tại");
            if (!await _readDbcontext.Permissions.AnyAsync(p => p.Id == request.permissionId)) return new RoleCommandResult(false, "Permission này không tồn tại");
            var checkRP = await _readDbcontext.RolePermissions.Where(x => x.RolesId == request.RoleId && x.PermissionsId == request.permissionId).FirstOrDefaultAsync();
            if (checkRP != null)
            {
                _writeDbcontext.RolePermissions.Remove(checkRP);
                await _writeDbcontext.SaveChangesAsync();
                return new RoleCommandResult(true, Message.ActionSuccess);
            }
            else
            {
                return new RoleCommandResult(false, "Quyền truy cập này không tồn tại");
            }
        }
    }
}
