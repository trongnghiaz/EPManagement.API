using Application.Common.Handler;
using Application.Common.Interface;
using AutoMapper;
using Domain.Helper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Implements.Departments.Query.GetDepartmentOptions
{
    public class GetDepartmentOptionsQueryHandler : BaseHandler<GetDepartmentOptionsQuery, List<DepartmentQueryModel>>
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public GetDepartmentOptionsQueryHandler(IMapper mapper, ISender mediator, IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext) : base(writeDbcontext, readDbcontext)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        public override async Task<List<DepartmentQueryModel>> Handle(GetDepartmentOptionsQuery request, CancellationToken cancellationToken)
        {
            var listOption = await _readDbcontext.Department
                .Where(x => x.DepartmentId != Guid.Parse(Admin.DefaultDepartment))
                .Where(x => x.IsDeleted == false)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<DepartmentQueryModel>>(listOption);
        }
    }
}
