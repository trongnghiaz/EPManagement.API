using Application.Common.Handler;
using Application.Common.Interface;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Implements.Departments.Query.GetDetailDepartment.GetByIdQuery
{
    public class GetByIdQueryHandler : BaseHandler<GetByIdQuery, DepartmentQueryModel>
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;
        public GetByIdQueryHandler(ISender mediator, IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext, IMapper mapper) : base(writeDbcontext, readDbcontext)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task<DepartmentQueryModel> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var search = await _writeDbcontext.Department.Where(d => d.DepartmentId == request.id).FirstOrDefaultAsync();
            if (search == null)
            {
                throw new Exception("Employee not found !");
            }
            else
            {
                return _mapper.Map<DepartmentQueryModel>(search);
            }
        }
    }
}
