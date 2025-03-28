

using MediatR;

namespace Application.Implements.Attendances.GetMonthlyAttendance
{
    public record MonthlyAttendanceQuery() : IRequest<List<AttendanceQueryModel>>
    {
        public DateTime fromTime { get; set; } = DateTime.Now;
        public DateTime toTime { get; set; } = DateTime.Now;
        public void SetId(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; private set; }
    }
    
}
