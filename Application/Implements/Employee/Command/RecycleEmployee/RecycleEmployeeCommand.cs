using MediatR;

namespace Application.Implements.Employee.Command.RecycleEmployee
{
    public record RecycleEmployeeCommand(Guid departmentId) : IRequest<EmployeeCommandResult>
    {
        public void SetId(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; private set; }
    }    
}
