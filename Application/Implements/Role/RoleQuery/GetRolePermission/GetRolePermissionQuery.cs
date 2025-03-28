using MediatR;

namespace Application.Implements.Role.RoleQuery.GetRolePermission
{
    public record GetRolePermissionQuery : IRequest<List<RoleWithPermissionsDto>>;    
}
