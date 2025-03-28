using Domain.Entities;
using Domain.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Persistence.Build
{
    public class EmployeeRolesBuild : IEntityTypeConfiguration<EmployeeRoles>
    {
        
        public void Configure(EntityTypeBuilder<EmployeeRoles> builder)
        {
            builder.HasKey(er => new { er.EmployeesEmployeeId, er.RolesId });
            builder.HasData(new EmployeeRoles { EmployeesEmployeeId = Guid.Parse(Admin.AdminId), RolesId = 1 });
        }
    }
}
