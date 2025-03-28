
using Application.Common.Handler;
using Application.Common.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Implements.Attendances.CheckOut
{
    public class CheckOutCommandHandler : BaseHandler<CheckOutCommand, CheckOutResponse>
    {
        private readonly ISender _mediator;
        public CheckOutCommandHandler(ISender mediator, IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext) : base(writeDbcontext, readDbcontext)
        {
            _mediator = mediator;
        }

        public override async Task<CheckOutResponse> Handle(CheckOutCommand request, CancellationToken cancellationToken)
        {
            var attendance = await _readDbcontext.Attendance
                .Where(x => x.EmployeesEmployeeId == request.employeeId && x.Date == DateTime.Today)
                .FirstOrDefaultAsync();
            if(attendance == null)
            {
                return new CheckOutResponse(false, "Bạn chưa check-in hôm nay");
            }
            if(attendance.Checkout != null)
            {
                return new CheckOutResponse(false, "Bạn đã check-out rồi");
            }
            attendance.Checkout = DateTime.Now;
            var workDuration = attendance.Checkout.Value - attendance.Checkin;
            attendance.WorkHours = (int)workDuration.TotalHours;
            _writeDbcontext.Attendance.Update(attendance);
            await _writeDbcontext.SaveChangesAsync();
            return new CheckOutResponse(true, "Check-out thành công");
        }
    }
}
