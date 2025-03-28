using MediatR;

namespace Application.Implements.Role.RoleCommand.AddRole
{
    public record RolesCommand(string email, int roleId) : IRequest<RoleCommandResult>;    
}
