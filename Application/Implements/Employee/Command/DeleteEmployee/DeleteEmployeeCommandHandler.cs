using Application.Common.Handler;
using Application.Common.Interface;
using Domain.Helper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Implements.Employee.Command.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler : BaseHandler<DeleteEmployeeCommand, EmployeeCommandResult>
    {
        private readonly ISender _mediator;
        public DeleteEmployeeCommandHandler(ISender mediator, IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext) : base(writeDbcontext, readDbcontext)
        {
            _mediator = mediator;
        }
        public override async Task<EmployeeCommandResult> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var checkExist = await _readDbcontext.Employees.Where(e => e.EmployeeId == request.employeeId).FirstOrDefaultAsync();
            if (checkExist == null)
            {
                return new EmployeeCommandResult(false, Message.NotExistID);
            }
            if (request.employeeId == Guid.Parse(Admin.AdminId))
            {
                return new EmployeeCommandResult(false, Message.DefaultRecord);
            }
            if (checkExist.IsDeleted == true) return new EmployeeCommandResult(false, "Nhân sự này đã trong danh sách đã xóa");

            await _writeDbcontext.Employees
                .Where(d => d.EmployeeId == request.employeeId)
                .ExecuteUpdateAsync(e => e
                .SetProperty(x => x.IsDeleted, true)
                .SetProperty(x => x.DepartmentId, Guid.Parse(Admin.DefaultDepartment)));
            return new EmployeeCommandResult(true, Message.DeletedSuccess);
        }
    }
}
