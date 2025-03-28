using Domain.Entities;
using MediatR;

namespace Application.Implements.Role.RoleQuery.GetPermissions
{
    public record GetListPermissionQuery : IRequest<List<Permissions>>;    
}
