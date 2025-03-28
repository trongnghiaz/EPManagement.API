using Domain.Entities;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Build
{
    public class RolePermissionBuild : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.HasKey(x => new { x.RolesId, x.PermissionsId });

            builder.HasData(
                Create(Roles.UserView, Permission.ReadMember),
                Create(Roles.UserEdit, Permission.UpdateMember),

                Create(Roles.Manager, Permission.ReadMember),
                Create(Roles.Manager, Permission.UpdateMember),

                Create(Roles.Admin, Permission.ReadMember),
                Create(Roles.Admin, Permission.UpdateMember),
                Create(Roles.Admin, Permission.CreateMember),
                Create(Roles.Admin, Permission.DeleteMember));            
        }

        private static RolePermission Create(Roles role, Permission permission)
        {
            return new RolePermission
            {
                RolesId = role.Id,
                PermissionsId = (int)permission
            };
        }
    }
}
