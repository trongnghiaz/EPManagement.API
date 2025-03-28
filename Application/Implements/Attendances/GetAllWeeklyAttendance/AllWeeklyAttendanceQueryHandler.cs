
using Application.Common.Handler;
using Application.Common.Interface;
using Application.Common.Model;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Implements.Attendances.GetAllWeeklyAttendance
{
    public class AllWeeklyAttendanceQueryHandler : BaseHandler<AllWeeklyAttendanceQuery, PagedList<AllAtendanceModel>>
    {
        private readonly ISender _mediator;
        public AllWeeklyAttendanceQueryHandler(ISender mediator, IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext) : base(writeDbcontext, readDbcontext)
        {
            _mediator = mediator;
        }

        public override async Task<PagedList<AllAtendanceModel>> Handle(AllWeeklyAttendanceQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Attendance> attendanceQuery = _readDbcontext.Attendance;
            IQueryable<Employees> employeesQuery = _readDbcontext.Employees;
            if (!string.IsNullOrWhiteSpace(request.searchName))
            {
                var searched = await employeesQuery.Where(e => e.EmployeeName.Contains(request.searchName)).ToListAsync();
                foreach (var item in searched)
                {
                    attendanceQuery = attendanceQuery.Where(a => a.EmployeesEmployeeId == item.EmployeeId);
                }                
            }
            if (!string.IsNullOrWhiteSpace(request.filterDepartment.ToString()))
            {
                var searched = await employeesQuery.Where(e => e.DepartmentId == Guid.Parse(request.filterDepartment.ToString())).ToListAsync();
                foreach (var item in searched)
                {
                    attendanceQuery = attendanceQuery.Where(a => a.EmployeesEmployeeId == item.EmployeeId);
                }
            }

            DateTime startDate = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);
            var record =  _readDbcontext.Attendance
                .Where(a =>  a.Date >= startDate)
                .OrderBy(a => a.Date)
                .Select(a => new AllAtendanceModel
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
                }).AsQueryable();

            return await PagedList<AllAtendanceModel>.CreateAsync(record, request.page, request.pageSize);
        }
    }
}
