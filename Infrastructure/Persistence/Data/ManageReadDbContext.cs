using Application.Common.Interface;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Data
{
    public class ManageReadDbContext : ManageDbContext<ManageReadDbContext>, IManageReadDbContext
    {
        public ManageReadDbContext(DbContextOptions<ManageReadDbContext> options) : base(options)
        {
        }
    }
}
