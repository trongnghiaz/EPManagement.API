
using MediatR;

namespace Application.Implements.Role.RoleCommand.RemoveRolePermission
{
    public record RemoveRolePermissionCommand(int permissionId) : IRequest<RoleCommandResult>
    {
        public void SetId(int id)
        {
            RoleId = id;
        }
        public int RoleId { get; private set; }
    }
}
