
using MediatR;

namespace Application.Implements.Employee.Command.DeleteFromBin
{
    public record DeleteEmployeeFromBinCommand(Guid id) : IRequest<EmployeeCommandResult>;    
}
