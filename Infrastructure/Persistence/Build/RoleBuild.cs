using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Build
{
    public class RoleBuild : IEntityTypeConfiguration<Roles>
    {
        public void Configure(EntityTypeBuilder<Roles> builder)
        {
            builder.ToTable(nameof(Roles));

            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Permissions)
                .WithMany()
                .UsingEntity<RolePermission>();
            builder.HasMany(x => x.Employees)
                .WithMany(x => x.Roles)
                .UsingEntity<EmployeeRoles>();
          

            builder.HasData(Roles.GetValues());
        }
    }
}
