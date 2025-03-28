using Application.Common.Handler;
using Application.Common.Interface;
using Domain.Helper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Implements.Departments.Command.DeleteDepartment
{
    public class DeleteDepartmentCommandHandler : BaseHandler<DeleteDepartmentCommand, DepartmentCommandResult>
    {
        private readonly ISender _mediator;
        public DeleteDepartmentCommandHandler(ISender mediator, IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext) : base(writeDbcontext, readDbcontext)
        {
            _mediator = mediator;
        }
        public override async Task<DepartmentCommandResult> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            if (request.id == Guid.Parse(Admin.DefaultDepartment))
            {
                return new DepartmentCommandResult(false, "Phòng ban này không thể xóa");
            }
            //check exist:
            var exist = await _readDbcontext.Department.Where(d => d.DepartmentId == request.id).FirstOrDefaultAsync();
                                   
            if (exist is null)
            {
                return new DepartmentCommandResult(false, Message.NotExistID);
            }
            else
            {
                if (await _readDbcontext.Employees.AnyAsync(x => x.DepartmentId == request.id)) 
                    return new DepartmentCommandResult(false, "Phòng ban còn nhân sự không thể xóa");                
                if (exist.IsDeleted == true) 
                    return new DepartmentCommandResult(false, "Phòng ban này đã trong danh sách đã xóa");
                
                await _writeDbcontext.Department
                    .Where(d => d.DepartmentId == request.id)
                    .ExecuteUpdateAsync(u => u.SetProperty(u => u.IsDeleted, true));
                
                return new DepartmentCommandResult(true, Message.DeletedSuccess);
            }
        }
    }
}
