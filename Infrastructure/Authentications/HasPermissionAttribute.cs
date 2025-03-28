using Domain.Enum;
using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authentications
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(Permission permission) : base(policy: permission.ToString()) { }
    }
}
