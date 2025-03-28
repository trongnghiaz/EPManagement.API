using Domain.Enum;

namespace Application.Implements.Attendances.GetAllMonthlyAttendance
{
    public class AllAttendanceQueryModel
    {
        public Guid AttendanceId { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public Guid DepartmentId { get; set; }
        public DateTime Date { get; set; }
        public AttendanceStatus Status { get; set; }
        public DateTime Checkin { get; set; }
        public DateTime? Checkout { get; set; }
        public float? WorkHours { get; set; }
        public float? OvertimeHours { get; set; }
        public string? Note { get; set; }
    }


}
