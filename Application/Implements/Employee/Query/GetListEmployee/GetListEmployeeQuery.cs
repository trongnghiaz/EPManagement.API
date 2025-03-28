using Application.Common.Model;
using Domain.Enum;
using MediatR;

namespace Application.Implements.Employee.Query.GetListEmployee
{
    public record GetListEmployeeQuery(
        string? searchTerm,
        Gender? sortGender,
        bool? sortActive,
        string? sortDepartment,
        int page, 
        int pageSize ) : IRequest<PagedList<EmployeeQueryModel>>;
}
