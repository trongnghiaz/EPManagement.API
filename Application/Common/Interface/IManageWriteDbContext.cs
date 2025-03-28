

namespace Application.Common.Interface
{
    public interface IManageWriteDbContext:IManageDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
