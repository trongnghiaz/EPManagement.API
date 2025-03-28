using Application.Common.Interface;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Data
{
    public class ManageWriteDbContext : ManageDbContext<ManageWriteDbContext>, IManageWriteDbContext
    {
        public ManageWriteDbContext(DbContextOptions<ManageWriteDbContext> options) : base(options)
        {
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
