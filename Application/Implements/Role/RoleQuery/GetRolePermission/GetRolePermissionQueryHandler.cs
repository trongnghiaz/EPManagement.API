using Application.Common.Handler;
using Application.Common.Interface;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Implements.Role.RoleQuery.GetRolePermission
{
    public class GetRolePermissionQueryHandler : BaseHandler<GetRolePermissionQuery, List<RoleWithPermissionsDto>>
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public GetRolePermissionQueryHandler(IMapper mapper, ISender mediator, IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext) : base(writeDbcontext, readDbcontext)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        public override async Task<List<RoleWithPermissionsDto>> Handle(GetRolePermissionQuery request, CancellationToken cancellationToken)
        {
            var rolePermission = await (from r in _readDbcontext.Roles
                                        join rp in _readDbcontext.RolePermissions on r.Id equals rp.RolesId
                                        join p in _readDbcontext.Permissions on rp.PermissionsId equals p.Id                                         
                                        select new RolePermissionDto
                                        {
                                            RoleId = r.Id,
                                            RoleName = r.Name,
                                            PermissionId = p.Id,
                                            PermissionName = p.Name,
                                        }).ToListAsync();

            var roleWithPermissions = rolePermission
                .GroupBy(r => new {r.RoleId, r.RoleName})
                .Select(g => new RoleWithPermissionsDto
                {
                    RoleId = g.Key.RoleId,
                    RoleName = g.Key.RoleName,
                    Permission =g.Select(rp => new PermissionDto
                    {
                        PermissionId = rp.PermissionId,
                        PermissionName = rp.PermissionName
                    }).ToList()
                }).ToList();            
            return roleWithPermissions;
        }
    }
}
