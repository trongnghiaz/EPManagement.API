using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interface
{
    public interface IPermissionService
    {
        Task<HashSet<string>> GetPermissionsAsync(Guid memberId);
        
    }
}
