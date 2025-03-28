using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Build
{
    public class SalariesConfig : IEntityTypeConfiguration<Salaries>
    {
        public void Configure(EntityTypeBuilder<Salaries> builder)
        {
            builder.HasKey(e => e.SalaryId);
        }
    }
}
