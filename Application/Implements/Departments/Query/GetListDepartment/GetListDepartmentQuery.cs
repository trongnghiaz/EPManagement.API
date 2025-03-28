using Application.Common.Model;
using MediatR;

namespace Application.Implements.Departments.Query.GetListDepartment
{
    public record GetListDepartmentQuery(string? searchTerm,string? sortColumn, string? sortOrder, int page =1, int pageSize = 10) : IRequest<PagedList<DepartmentQueryModel>>;
}
