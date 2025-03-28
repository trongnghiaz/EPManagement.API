
using Application.Common.Handler;
using Application.Common.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Application.Implements.Attendances.GetWeeklyAttendace
{
    public class WeeklyAttendanceQuerryHandler : BaseHandler<WeeklyAttendanceQuerry, List<AttendanceQueryModel>>
    {
        private readonly ISender _mediator;
        public WeeklyAttendanceQuerryHandler(ISender mediator, IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext) : base(writeDbcontext, readDbcontext)
        {
            _mediator = mediator;
        }

        public override async Task<List<AttendanceQueryModel>> Handle(WeeklyAttendanceQuerry request, CancellationToken cancellationToken)
        {
            
            DateTime startDate = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);
            var record = await _readDbcontext.Attendance
                .Where(a => a.EmployeesEmployeeId == request.employeeId && a.Date >= startDate)
                .OrderBy(a => a.Date)
                .Select(a => new AttendanceQueryModel
                {
                    AttendanceId = a.AttendanceId,
                    EmployeeId = a.EmployeesEmployeeId,
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
