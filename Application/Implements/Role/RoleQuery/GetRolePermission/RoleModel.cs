
using Application.Common.Interface;
using Domain.Entities;
using Domain.Enum;

namespace Application.Implements.Role.RoleQuery.GetRolePermission
{
    public class RolePermissionDto 
    { 
        public int RoleId { get; set; } 
        public string RoleName { get; set; } 
        public int PermissionId { get; set; } 
        public string PermissionName { get; set; } 
    }
    public class RoleWithPermissionsDto 
    { 
        public int RoleId { get; set; } 
        public string RoleName { get; set; } 
        public List<PermissionDto> Permission { get; set; } 
    }
    public class PermissionDto 
    { 
        public int PermissionId { get; set; } 
        public string PermissionName { get; set; } 
    }

}
