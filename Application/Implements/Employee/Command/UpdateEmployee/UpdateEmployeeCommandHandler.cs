using Application.Common.Handler;
using Application.Common.Interface;
using Domain.Helper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Implements.Employee.Command.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler : BaseHandler<UpdateEmployeeCommand, EmployeeCommandResult>
    {
        private readonly ISender _mediator;
        public UpdateEmployeeCommandHandler(ISender mediator, IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext) : base(writeDbcontext, readDbcontext)
        {
            _mediator = mediator;
        }
        public override async Task<EmployeeCommandResult> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            if (!await _readDbcontext.Employees.Where(e => e.EmployeeId == request.Id).AnyAsync(cancellationToken))
            {
                return new EmployeeCommandResult(false, Message.NotExistID);
            }
            if (!await _readDbcontext.Department.Where(d => d.DepartmentId == request.departmentId).AnyAsync(cancellationToken))
            {
                return new EmployeeCommandResult(false, "Không có phòng ban này");
            }
            var update = await _writeDbcontext.Employees.Where(e => e.EmployeeId == request.Id)
                .ExecuteUpdateAsync(u => u
                .SetProperty(name => name.EmployeeName, request.employeeName)
                .SetProperty(g => g.Gender, request.gender)
                .SetProperty(birth => birth.DateOfBirth, request.dateOfBirth)
                .SetProperty(p => p.PhoneNumber, request.phoneNumber)
                .SetProperty(a => a.Address, request.address)
                .SetProperty(e => e.Email, request.email)
                .SetProperty(j => j.JobTitle, request.jobTitle)
                .SetProperty(s => s.SalaryBase, request.salaryBase)
                .SetProperty(act => act.IsActive, request.isActive)
                .SetProperty(act => act.DepartmentId, request.departmentId));
            return new EmployeeCommandResult(true, Message.UpdatedSuccess);
        }
    }
}
