using Application.Common.Model;
using MediatR;

namespace Application.Implements.Employee.Query.ListDeleted
{
    public record ListEmployeeDeletedQuery(string searchTerm, int page =1, int pageSize =10) : IRequest<PagedList<ListEmployeeDeletedModel>>
    {
    }
}
