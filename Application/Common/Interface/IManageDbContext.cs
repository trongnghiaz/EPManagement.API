using Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interface
{
    public interface IManageDbContext
    {
        DbSet<Employees> Employees { get; set; }
        DbSet<Department> Department { get; set; }
        DbSet<Roles> Roles { get; set; }
        DbSet<Permissions> Permissions { get; set; }
        DbSet<RolePermission> RolePermissions { get; set; }
        DbSet<EmployeeRoles> EmployeeRoles { get; set; }  
        DbSet<Attendance> Attendance { get; set; }
        DbSet<Salaries> Salaries { get; set; }
    }
}
