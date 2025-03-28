
using MediatR;

namespace Application.Implements.Departments.Command.RecycleDepartment
{
    public record RecycleDepartmentCommand(Guid departmentId): IRequest<DepartmentCommandResult>;    
}
