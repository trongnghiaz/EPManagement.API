using Application.Common.Model;
using MediatR;

namespace Application.Implements.Role.RoleQuery.GetEmployeeRoles
{
    public record EmployeeRolesQuery(string searchEmail,
                                     string roleId,
                                     int page = 1,
                                     int pageSize = 10) : IRequest<PagedList<EmployeeRolesModel>>;

}
