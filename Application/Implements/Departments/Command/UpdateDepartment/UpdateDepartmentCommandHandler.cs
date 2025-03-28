using Application.Common.Handler;
using Application.Common.Interface;
using AutoMapper;
using Domain.Helper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Implements.Departments.Command.UpdateDepartment
{
    public class UpdateDepartmentCommandHandler : BaseHandler<UpdateDepartmentCommand, DepartmentCommandResult>
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public UpdateDepartmentCommandHandler(ISender mediator, IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext, IMapper mapper) : base(writeDbcontext, readDbcontext)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        public override async Task<DepartmentCommandResult> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            // check exist:
            var exist = await _readDbcontext.Department.AnyAsync(d => d.DepartmentId == request.Id);
            if (!exist)
            {
                return new DepartmentCommandResult(false, Message.NotExistID);
            }
            else
            {
                await _writeDbcontext.Department.Where(d => d.DepartmentId == request.Id)
                     .ExecuteUpdateAsync(u => u
                     .SetProperty(n => n.DepartmentName, request.departmentName)
                     .SetProperty(a => a.Address, request.address));

                return new DepartmentCommandResult(true, Message.UpdatedSuccess);
            }
        }
    }
}
