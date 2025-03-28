
using MediatR;

namespace Application.Implements.Salaries.SalaryCounting
{
    public record SalaryCountingCommand(Guid id) : IRequest<SalaryCountedResponse>;
    
}
