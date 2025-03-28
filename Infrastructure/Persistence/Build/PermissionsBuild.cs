using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Build
{
    public sealed class PermissionsBuild : IEntityTypeConfiguration<Permissions>
    {
        public void Configure(EntityTypeBuilder<Permissions> builder)
        {
            builder.ToTable(nameof(Permissions));
            builder.HasKey(x => x.Id);

            IEnumerable<Permissions> permissions = Enum
            .GetValues<Domain.Enum.Permission>()
            .Select(p => new Permissions
            {
                Id = (int)p,
                Name = p.ToString()
            });

            builder.HasData(permissions);
        }
    }
}
