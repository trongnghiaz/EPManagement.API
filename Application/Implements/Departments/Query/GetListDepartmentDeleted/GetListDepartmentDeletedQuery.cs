
using Application.Common.Model;
using MediatR;

namespace Application.Implements.Departments.Query.GetListDepartmentDeleted
{
    public record GetListDepartmentDeletedQuery(string? searchTerm, int page =1, int pageSize = 10) : IRequest<PagedList<DeletedDepartmentModel>>;    
}
