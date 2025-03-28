using MediatR;

namespace Application.Implements.Attendances.GetWeeklyAttendace
{
    public record WeeklyAttendanceQuerry(Guid employeeId) : IRequest<List<AttendanceQueryModel>>;
    
}
