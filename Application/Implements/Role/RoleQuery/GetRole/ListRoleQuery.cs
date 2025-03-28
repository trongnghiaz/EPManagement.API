
using MediatR;

namespace Application.Implements.Role.RoleQuery.GetRole
{
    public class ListRoleQuery : IRequest<List<RoleQueryModel>>;   
}
