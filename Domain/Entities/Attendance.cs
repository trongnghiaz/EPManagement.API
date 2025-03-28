
using Domain.Enum;

namespace Domain.Entities
{
    public class Attendance
    {
        public Guid AttendanceId { get; set; }
        public Guid EmployeesEmployeeId { get; set; }
        public DateTime Date { get; set; }
        public AttendanceStatus Status { get; set; }    
        public DateTime Checkin { get; set; }
        public DateTime? Checkout { get; set; }
        public float? WorkHours { get; set; }
        public float? OvertimeHours { get; set; }
        public string? Note { get; set; }
        public virtual Employees Employees { get; set; }
    }
}
