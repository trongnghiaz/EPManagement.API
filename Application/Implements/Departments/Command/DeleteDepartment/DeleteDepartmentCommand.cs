using MediatR;

namespace Application.Implements.Departments.Command.DeleteDepartment
{
    public record DeleteDepartmentCommand(Guid id) : IRequest<DepartmentCommandResult>;

}
