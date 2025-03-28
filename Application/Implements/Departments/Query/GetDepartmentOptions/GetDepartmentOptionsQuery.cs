using MediatR;

namespace Application.Implements.Departments.Query.GetDepartmentOptions
{
    public record GetDepartmentOptionsQuery : IRequest<List<DepartmentQueryModel>>;    
}
