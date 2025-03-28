using Application.Common.Interface;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Authentications
{
    public class PermissionService : IPermissionService
    {
        private readonly IManageWriteDbContext _context;

        public PermissionService(IManageWriteDbContext context)
        {
            _context = context;
        }        

        public async Task<HashSet<string>> GetPermissionsAsync(Guid Id)
        {
            ICollection<Roles>[] roles = await _context.Employees
            .Include(x => x.Roles)
            .ThenInclude(x => x.Permissions)
            .Where(x => x.EmployeeId == Id)
            .Select(x => x.Roles)
            .ToArrayAsync();

            return roles
                .SelectMany(x => x)
                .SelectMany(x => x.Permissions)
                .Select(x => x.Name)
                .ToHashSet();
        }
    }
}
