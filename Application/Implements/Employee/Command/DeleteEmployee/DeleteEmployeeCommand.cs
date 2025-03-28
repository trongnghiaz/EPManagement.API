using MediatR;

namespace Application.Implements.Employee.Command.DeleteEmployee
{
    public record DeleteEmployeeCommand(Guid employeeId) : IRequest<EmployeeCommandResult>;

}
