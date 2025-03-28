using MediatR;

namespace Application.Implements.Departments.Command.AddDepartment
{
    public record AddDepartmentCommand(string departmentName, string address) : IRequest<DepartmentCommandResult>;
}
