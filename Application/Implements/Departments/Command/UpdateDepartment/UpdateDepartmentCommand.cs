using MediatR;

namespace Application.Implements.Departments.Command.UpdateDepartment
{
    public record UpdateDepartmentCommand(string departmentName = null!, string address = null!) : IRequest<DepartmentCommandResult>
    {
        public void SetId(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; private set; }
    }
}
