
using MediatR;

namespace Application.Implements.Attendances.CheckOut
{
    public record CheckOutCommand(Guid employeeId) : IRequest<CheckOutResponse>;
    
}
