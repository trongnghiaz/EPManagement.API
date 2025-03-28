
using MediatR;

namespace Application.Implements.Attendances.CheckIn
{
    public record CheckInCommand(Guid employeeId) : IRequest<CheckInResponse>;
    
}
