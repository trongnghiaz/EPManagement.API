
using MediatR;

namespace Application.Implements.Role.RoleCommand.RemoveAccess
{
    public record RemoveAccessCommand(Guid id) : IRequest<RoleCommandResult>;
   
}
