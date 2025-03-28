namespace Domain.Entities
{
    public class EmployeeRoles
    {
        public Guid EmployeesEmployeeId { get; set; }
        public int RolesId { get; set; }


        public virtual Employees Employees { get; set; }
        public virtual Roles? Roles { get; set; }
    }
}
