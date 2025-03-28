using Application.Common.Handler;
using Application.Common.Interface;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Implements.Role.RoleQuery.GetPermissions
{
    public class GetListPermissionQueryHandler : BaseHandler<GetListPermissionQuery, List<Permissions>>
    {
        private readonly ISender _mediator;
        public GetListPermissionQueryHandler(IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext, ISender mediator) : base(writeDbcontext, readDbcontext)
        {
            _mediator = mediator;
        }

        public override async Task<List<Permissions>> Handle(GetListPermissionQuery request, CancellationToken cancellationToken)
        {
            var permission = await _readDbcontext.Permissions.AsNoTracking().ToListAsync();
            return permission;
        }
    }
}
