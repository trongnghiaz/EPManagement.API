using Domain.Enum;
using MediatR;

namespace Application.Implements.Employee.Command.AddEmployee
{
    public record AddEmployeeCommand
    (
         string employeeName,
         Gender gender,
         DateTime dateOfBirth,
         string phoneNumber,
         string address,
         string email,
         string jobTitle,
         float salaryBase,
         bool isActive,
         Guid departmentId 

    ) : IRequest<EmployeeCommandResult>;


}
