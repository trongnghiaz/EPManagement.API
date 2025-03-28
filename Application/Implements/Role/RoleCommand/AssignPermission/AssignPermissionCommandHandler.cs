using Application.Common.Handler;
using Application.Common.Interface;
using Domain.Entities;
using Domain.Helper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Implements.Role.RoleCommand.AssignPermission
{
    public class AssignPermissionCommandHandler : BaseHandler<AssignPermissionCommand, RoleCommandResult>
    {
        private readonly ISender _mediator;
        public AssignPermissionCommandHandler(ISender mediator, IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext) : base(writeDbcontext, readDbcontext)
        {
            _mediator = mediator;
        }
        public override async Task<RoleCommandResult> Handle(AssignPermissionCommand request, CancellationToken cancellationToken)
        {
            var checkRole = await _readDbcontext.Roles.AnyAsync(r => r.Id == request.RoleId);
            var checkPermission = await _readDbcontext.Permissions.AnyAsync(p => p.Id == request.permissionId);
            if (checkRole && checkPermission)
            {
                if(!await _readDbcontext.RolePermissions.AnyAsync(x => x.RolesId == request.RoleId && x.PermissionsId == request.permissionId))
                {
                    var rolepermission = new RolePermission { RolesId = request.RoleId, PermissionsId = request.permissionId };
                    await _writeDbcontext.RolePermissions.AddAsync(rolepermission);
                    await _writeDbcontext.SaveChangesAsync();
                    return new RoleCommandResult(true, Message.UpdatedSuccess);
                }
                else
                {
                    return new RoleCommandResult(false, "Quyền truy cập này đã tồn tại");
                }
                
            }else
            {
                return new RoleCommandResult(false, Message.NotExistID);
            }            

        }
    }
}
