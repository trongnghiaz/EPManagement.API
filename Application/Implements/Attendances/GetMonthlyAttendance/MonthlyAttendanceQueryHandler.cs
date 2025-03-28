
using Application.Common.Handler;
using Application.Common.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Implements.Attendances.GetMonthlyAttendance
{
    public class MonthlyAttendanceQueryHandler : BaseHandler<MonthlyAttendanceQuery, List<AttendanceQueryModel>>
    {
        private readonly ISender _mediator;
        public MonthlyAttendanceQueryHandler(ISender mediator, IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext) : base(writeDbcontext, readDbcontext)
        {
            _mediator = mediator;
        }

        public override async Task<List<AttendanceQueryModel>> Handle(MonthlyAttendanceQuery request, CancellationToken cancellationToken)
        {
            DateTime startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime toTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));

            if (new DateTime(request.fromTime.Year, request.fromTime.Month, request.fromTime.Day) != new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day))
            {
                startTime = request.fromTime;
            }

            if (new DateTime(request.toTime.Year, request.toTime.Month, request.toTime.Day) != new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day))
            {
                toTime = request.toTime;
            }

            var record = await _readDbcontext.Attendance
                .Where(a => a.EmployeesEmployeeId == request.Id && a.Date >= startTime && a.Date <= toTime)
                .OrderBy(a => a.Date)
                .Select(a => new AttendanceQueryModel
                {
                    AttendanceId = a.AttendanceId,
                    EmployeesEmployeeId = a.EmployeesEmployeeId,
                    Date = a.Date,
                    Status = a.Status,
                    Checkin = a.Checkin,
                    Checkout = a.Checkout,
                    WorkHours = a.WorkHours,
                    OvertimeHours = a.OvertimeHours,
                    Note = a.Note
                }).ToListAsync(cancellationToken);
            return record;
        }
    }
}
