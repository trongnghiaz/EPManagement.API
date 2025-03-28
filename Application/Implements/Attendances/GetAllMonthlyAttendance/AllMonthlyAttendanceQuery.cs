using Application.Common.Model;

using MediatR;

namespace Application.Implements.Attendances.GetAllMonthlyAttendance
{
    public record AllMonthlyAttendanceQuery(
        string searchName,
        Guid filterDepartment,
        int page,
        int pageSize) : IRequest<PagedList<AllAttendanceQueryModel>>
    {
        public DateTime FromTime { get; set; } = DateTime.Now;
        public DateTime ToTime { get; set; } = DateTime.Now;
    }

}
