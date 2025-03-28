
using Application.Common.Model;
using MediatR;

namespace Application.Implements.Attendances.GetAllWeeklyAttendance
{
    public record AllWeeklyAttendanceQuery(
        string searchName, 
        Guid filterDepartment, 
        int page, 
        int pageSize) : IRequest<PagedList<AllAtendanceModel>>;
    
}
