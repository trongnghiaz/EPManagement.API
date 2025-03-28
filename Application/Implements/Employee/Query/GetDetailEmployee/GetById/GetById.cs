using MediatR;

namespace Application.Implements.Employee.Query.GetDetailEmployee.GetById
{
    public record GetById(Guid id) : IRequest<EmployeeQueryModel>;
}

