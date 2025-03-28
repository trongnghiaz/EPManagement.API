using Domain.Enum;

namespace Domain.Entities
{
    public class Employees 
    {        
        public Guid EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string? AvatarUrl { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? JobTitle { get; set; }
        public float? SalaryBase { get; set; }
        public bool IsActive { get; set; }              
        public Guid DepartmentId { get; set; }

        public virtual Department? Department { get; set; }
        public virtual ICollection<Roles>? Roles { get; set; }
        public virtual bool IsDeleted { get; set; }
    }
}
