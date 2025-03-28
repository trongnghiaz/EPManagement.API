using Application.Common.Interface;
using Domain.Entities;
using Domain.Enum;

namespace Application.Implements.Employee.Query
{
    public class EmployeeQueryModel : IMapFrom<Employees>
    {
        public Guid EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
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
        public string? Departments { get; set; }

    }
}
