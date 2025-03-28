
using Application.Common.Handler;
using Application.Common.Interface;
using Domain.Helper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Implements.Departments.Command.DeleteFromBin
{
    public class DeleteDepertmentFromBinCommandHandler : BaseHandler<DeleteDepertmentFromBinCommand, DepartmentCommandResult>
    {
        private readonly ISender _mediator;
        public DeleteDepertmentFromBinCommandHandler(ISender mediator, IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext) : base(writeDbcontext, readDbcontext)
        {
            _mediator = mediator;
        }

        public override async Task<DepartmentCommandResult> Handle(DeleteDepertmentFromBinCommand request, CancellationToken cancellationToken)
        {
            var chechExist = await _readDbcontext.Department.Where(x => x.DepartmentId == request.id).FirstOrDefaultAsync();
            if (chechExist is null) return new DepartmentCommandResult(false, Message.NotExistID);
            if (chechExist.IsDeleted == false) return new DepartmentCommandResult(false, "Phòng ban này đang trong danh sách đang sử dụng");

             _writeDbcontext.Department.Remove(chechExist);
            await _writeDbcontext.SaveChangesAsync(cancellationToken);
            return new DepartmentCommandResult(true, Message.DeletedSuccess);
            
        }
    }
}
