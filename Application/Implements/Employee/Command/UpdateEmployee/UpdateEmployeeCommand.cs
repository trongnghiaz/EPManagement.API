using Domain.Enum;
using MediatR;

namespace Application.Implements.Employee.Command.UpdateEmployee
{
    public record UpdateEmployeeCommand(

        string employeeName,
        Gender gender,
        DateTime dateOfBirth,
        string phoneNumber,
        string address,
        string email,
        string jobTitle,
        float salaryBase,
        bool isActive,
        Guid departmentId) : IRequest<EmployeeCommandResult>
    {
        public void SetId(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; private set; }
    }
}
