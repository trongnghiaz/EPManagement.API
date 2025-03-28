using Application.Common.Handler;
using Application.Common.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Implements.Role.RoleQuery.GetRole
{
    public class ListRoleQueryHandler : BaseHandler<ListRoleQuery, List<RoleQueryModel>>
    {
        private readonly ISender _mediator;
        public ListRoleQueryHandler(ISender mediator, IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext) : base(writeDbcontext, readDbcontext)
        {
            _mediator = mediator;
        }

        public override async Task<List<RoleQueryModel>> Handle(ListRoleQuery request, CancellationToken cancellationToken)
        {
            var role = await(from r in _readDbcontext.Roles
                       select new RoleQueryModel { RoleId = r.Id, Name = r.Name }).ToListAsync(cancellationToken);
            return role;
        }
    }
}
