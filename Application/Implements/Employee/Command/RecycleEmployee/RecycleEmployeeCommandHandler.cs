using Application.Common.Handler;
using Application.Common.Interface;
using Domain.Helper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Implements.Employee.Command.RecycleEmployee
{
    public class RecycleEmployeeCommandHandler : BaseHandler<RecycleEmployeeCommand, EmployeeCommandResult>
    {
        private readonly ISender _mediator;
        private readonly IRandomPasswordString _randomPassword;
        private readonly ISQSService _sqsservice;
        public RecycleEmployeeCommandHandler(ISQSService sqsservice, ISender mediator, IRandomPasswordString randomPassword, IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext) : base(writeDbcontext, readDbcontext)
        {
            _sqsservice = sqsservice;
            _randomPassword = randomPassword;
            _mediator = mediator;
        }
        public override async Task<EmployeeCommandResult> Handle(RecycleEmployeeCommand request, CancellationToken cancellationToken)
        {
            var checkRecycle = await _readDbcontext.Employees.Where(c => c.EmployeeId == request.Id).FirstOrDefaultAsync();            
            if (checkRecycle == null) return new EmployeeCommandResult(false, Message.UserNotFound);
            var checkDepartment = await _readDbcontext.Department.AnyAsync(d => d.DepartmentId == request.departmentId);
            if (!checkDepartment) return new EmployeeCommandResult(false, Message.NotExistID);
            var newPassword = _randomPassword.GenerateRandomPassword();
            var update = await _writeDbcontext.Employees.Where(e => e.EmployeeId == request.Id)
                .ExecuteUpdateAsync(u => u               
                .SetProperty(ud => ud.Password, BCrypt.Net.BCrypt.HashPassword(newPassword))
                .SetProperty(act => act.IsActive, false)
                .SetProperty(act => act.IsDeleted, false)
                .SetProperty(act => act.DepartmentId, request.departmentId));

            await _sqsservice.SendMessageAsync($"Name: {checkRecycle.EmployeeName}, Email: {checkRecycle.Email}, Password: {newPassword}");
            return new EmployeeCommandResult(true, Message.UpdatedSuccess);
        }
    }
}
