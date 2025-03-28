using Application.Common.Model;
using Domain.Enum;
using MediatR;

namespace Application.Implements.Employee.Query.GetListByUser
{
    public record GetListByUserQuery(
        string? searchTerm,
        Gender? sortGender,
        bool? sortActive, 
        Guid departmentId,
        int page,
        int pageSize) : IRequest<PagedList<EmployeeQueryModel>>;
}
