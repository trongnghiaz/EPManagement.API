using MediatR;

namespace Application.Implements.Role.RoleCommand.ChangeRole
{
    public record UpdateRoleCommand(int roleId) : IRequest<RoleCommandResult>
    {
        public void SetId(Guid id)
        {
            EmployeeId = id;
        }
        public Guid EmployeeId { get; private set; }
    }
}
