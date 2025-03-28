using Domain.Entities;
using Domain.Enum;
using Domain.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Build
{
    public partial class EmployeeBuild : IEntityTypeConfiguration<Employees>
    {
        public void Configure(EntityTypeBuilder<Employees> builder)
        {
            builder.HasKey(e => e.EmployeeId);

            builder.Property(e => e.Gender);
            

            builder.HasMany(x => x.Roles)
                .WithMany()
                .UsingEntity<EmployeeRoles>();

            var adminpass = BCrypt.Net.BCrypt.HashPassword("admin1234");
            builder.HasData( new Employees
            {
                EmployeeId = Guid.Parse(Admin.AdminId),
                EmployeeName = "admin",
                Gender = Gender.Nam,
                DateOfBirth = new DateTime(2000, 01, 01),
                PhoneNumber = "1234567890",
                Address = "admin",
                Email = "admin@gmail.com",
                Password = adminpass,
                IsActive = true,
                DepartmentId = Guid.Parse(Admin.DefaultDepartment),
                IsDeleted = false,
            });            
        }
    }
}
