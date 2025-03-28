using Domain.Entities;
using Domain.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Build
{
    public partial class DepartmentBuild : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasMany(e => e.Employees)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(new Department
            {
                DepartmentId = Guid.Parse(Admin.DefaultDepartment),
                DepartmentName = "Default",
                Address = "Default",
                EstablishDate = DateTime.Now,
                IsDeleted = false,
            });
        }
    }
}
