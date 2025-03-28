using Application.Common.Handler;
using Application.Common.Interface;
using Domain.Helper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Implements.Departments.Command.RecycleDepartment
{
    public class RecycleDepartmentCommanHandler : BaseHandler<RecycleDepartmentCommand, DepartmentCommandResult>
    {
        private readonly ISender _mediator;
        public RecycleDepartmentCommanHandler(ISender mediator, IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext) : base(writeDbcontext, readDbcontext)
        {
            _mediator = mediator;
        }
        public override async Task<DepartmentCommandResult> Handle(RecycleDepartmentCommand request, CancellationToken cancellationToken)
        {
            var checkExist = await _readDbcontext.Department.Where(d => d.DepartmentId == request.departmentId).FirstOrDefaultAsync();
            if (checkExist == null) return new DepartmentCommandResult(false, Message.NotExistID);
            if (checkExist.IsDeleted != true) return new DepartmentCommandResult(false, "Phòng ban này chưa bị xóa");

            await _writeDbcontext.Department
                .Where(d => d.DepartmentId == request.departmentId)
                .ExecuteUpdateAsync(u => u.SetProperty(u => u.IsDeleted, false));
            return new DepartmentCommandResult(true, Message.ActionSuccess);
        }
    }
}
