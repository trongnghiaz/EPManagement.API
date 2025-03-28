
using Application.Common.Handler;
using Application.Common.Interface;
using Domain.Entities;
using Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Implements.Attendances.CheckIn
{
    public class CheckInCommandHandler : BaseHandler<CheckInCommand, CheckInResponse>
    {
        private readonly ISender _mediator;
        public CheckInCommandHandler(ISender mediator, IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext) : base(writeDbcontext, readDbcontext)
        {
            _mediator = mediator;
        }

        public override async Task<CheckInResponse> Handle(CheckInCommand request, CancellationToken cancellationToken)
        {
            if(await _readDbcontext.Employees.FindAsync(request.employeeId) == null)
            {
                return new CheckInResponse(false, "Dữ liệu nhân sự chưa tồn tại!");
            }
            if(await _readDbcontext.Attendance.Where(x => x.EmployeesEmployeeId == request.employeeId && x.Date == DateTime.Today).FirstOrDefaultAsync() != null)
            {
                return new CheckInResponse(false, "Nhân sự đã check-in hôm nay!");
            }
            var checkin = DateTime.Now;
            var status = AttendanceStatus.Present;
            if(checkin.Hour >= 9)
            {
                status = AttendanceStatus.Late;
            }
            var attendance = new Attendance
            {
                AttendanceId = Guid.NewGuid(),
                EmployeesEmployeeId = request.employeeId,
                Date = DateTime.Today,
                Checkin = checkin,
                Status = status
            };
            await _writeDbcontext.Attendance.AddAsync(attendance);
            await _writeDbcontext.SaveChangesAsync(cancellationToken);
            return new CheckInResponse(true, "Checkin thành công");
        }
    }
}
