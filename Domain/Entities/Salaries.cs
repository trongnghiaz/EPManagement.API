
namespace Domain.Entities
{
    public class Salaries
    {
        public Guid SalaryId { get; set; }
        public Guid EmployeesEmployeeId { get; set; }
        public DateTime Month { get; set; }
        public float WorkHours { get; set; }
        public float SalaryBase { get; set; }
        public float? OvertimeHours { get; set; }
        public float? Allowance { get; set; }
        public float? Deduction { get; set; }
        public float? TotalSalary { get; set; }
        public virtual Employees Employees { get; set; }
    }
}
