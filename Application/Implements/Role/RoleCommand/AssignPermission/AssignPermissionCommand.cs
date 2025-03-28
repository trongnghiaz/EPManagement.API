using MediatR;

namespace Application.Implements.Role.RoleCommand.AssignPermission
{
    public record AssignPermissionCommand( int permissionId) : IRequest<RoleCommandResult>
    {
        public void SetId(int id)
        {
            RoleId = id;
        }
        public int RoleId { get; private set; }
    }
}
