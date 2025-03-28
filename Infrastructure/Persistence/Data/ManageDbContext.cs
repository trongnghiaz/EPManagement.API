using Domain.Entities;

using Infrastructure.Persistence.Build;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Data
{
    public class ManageDbContext<TContext> : DbContext where TContext : ManageDbContext<TContext>
    {
        public ManageDbContext(DbContextOptions<TContext> options) : base(options)
        {
        }


        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Permissions> Permissions { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<EmployeeRoles> EmployeeRoles { get; set; }
        public virtual DbSet<Attendance> Attendance { get; set; }
        public virtual DbSet<Salaries> Salaries { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DepartmentBuild());
            modelBuilder.ApplyConfiguration(new EmployeeBuild());
            modelBuilder.ApplyConfiguration(new RoleBuild());
            modelBuilder.ApplyConfiguration(new PermissionsBuild());
            modelBuilder.ApplyConfiguration(new RolePermissionBuild());
            modelBuilder.ApplyConfiguration(new EmployeeRolesBuild());
            modelBuilder.ApplyConfiguration(new AttendanceConfig());
            modelBuilder.ApplyConfiguration(new SalariesConfig());
            base.OnModelCreating(modelBuilder);
        }
    }
}
