
using Application.Common.Handler;
using Application.Common.Interface;
using Application.Implements.Departments.Command;
using Domain.Helper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Implements.Employee.Command.DeleteFromBin
{
    public class DeleteEmployeeFromBinCommandHandler : BaseHandler<DeleteEmployeeFromBinCommand, EmployeeCommandResult>
    {
        private readonly ISender _mediator;
        public DeleteEmployeeFromBinCommandHandler(ISender mediator, IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext) : base(writeDbcontext, readDbcontext)
        {
            _mediator = mediator;
        }

        public override async Task<EmployeeCommandResult> Handle(DeleteEmployeeFromBinCommand request, CancellationToken cancellationToken)
        {
            var chechExist = await _readDbcontext.Employees.Where(x => x.EmployeeId == request.id).FirstOrDefaultAsync();
            if (chechExist is null) return new EmployeeCommandResult(false, Message.NotExistID);
            if (chechExist.IsDeleted == false) return new EmployeeCommandResult(false, "Phòng ban này đang trong danh sách đang sử dụng");

            _writeDbcontext.Employees.Remove(chechExist);
            await _writeDbcontext.SaveChangesAsync(cancellationToken);
            return new EmployeeCommandResult(true, Message.DeletedSuccess);
        }
    }
}
