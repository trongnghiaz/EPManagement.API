using Application.Common.Handler;
using Application.Common.Interface;
using Application.Common.Model;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Implements.Attendances.GetAllMonthlyAttendance
{
    public class AllMonthlyAttendanceQueryHandler : BaseHandler<AllMonthlyAttendanceQuery, PagedList<AllAttendanceQueryModel>>
    {
        private readonly ISender _mediator;
        public AllMonthlyAttendanceQueryHandler(ISender mediator, IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext) : base(writeDbcontext, readDbcontext)
        {
            _mediator = mediator;
        }

        public override async Task<PagedList<AllAttendanceQueryModel>> Handle(AllMonthlyAttendanceQuery request, CancellationToken cancellationToken)
        {
            DateTime startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime toTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));

            if (new DateTime(request.FromTime.Year, request.FromTime.Month, request.FromTime.Day) != new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day))
            {
                startTime = request.FromTime;
            }

            if (new DateTime(request.ToTime.Year, request.ToTime.Month, request.ToTime.Day) != new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day))
            {
                toTime = request.ToTime;
            }
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

            var record = (from attendance in attendanceQuery
                         join employee in employeesQuery on attendance.EmployeesEmployeeId equals employee.EmployeeId into emp
                         from m in emp.DefaultIfEmpty()                         
                        //where attendance.Date >= startTime 
                        //where attendance.Date <= toTime
                        orderby attendance.Date
                        select new AllAttendanceQueryModel
                        {
                            AttendanceId = attendance.AttendanceId,
                            EmployeeId = attendance.EmployeesEmployeeId,
                            EmployeeName = m.EmployeeName,
                            DepartmentId = m.DepartmentId,
                            Date = attendance.Date,
                            Status = attendance.Status,
                            Checkin = attendance.Checkin,
                            Checkout = attendance.Checkout,
                            WorkHours = attendance.WorkHours,
                            OvertimeHours = attendance.OvertimeHours,
                            Note = attendance.Note
                        }).AsQueryable();
            return await PagedList<AllAttendanceQueryModel>.CreateAsync(record, request.page, request.pageSize);
        }
    }
}
