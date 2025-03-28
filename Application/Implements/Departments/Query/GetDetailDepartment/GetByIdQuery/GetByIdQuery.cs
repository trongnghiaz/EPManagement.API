using MediatR;

namespace Application.Implements.Departments.Query.GetDetailDepartment.GetByIdQuery
{
    public record GetByIdQuery(Guid id) : IRequest<DepartmentQueryModel>;

}
