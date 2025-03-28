using MediatR;

namespace Application.Implements.Departments.Command.DeleteFromBin
{
    public record DeleteDepertmentFromBinCommand(Guid id) : IRequest<DepartmentCommandResult>;   
}
