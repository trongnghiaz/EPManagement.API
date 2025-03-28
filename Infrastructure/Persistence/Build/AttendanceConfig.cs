
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Build
{
    public class AttendanceConfig : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.HasKey(e => e.AttendanceId);
            //builder.Property(e => e.Checkout).IsRequired(false);
        }
    }
}
