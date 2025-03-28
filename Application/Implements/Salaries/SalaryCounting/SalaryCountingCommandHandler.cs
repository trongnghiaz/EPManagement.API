
using Application.Common.Handler;
using Application.Common.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Implements.Salaries.SalaryCounting
{
    public class SalaryCountingCommandHandler : BaseHandler<SalaryCountingCommand, SalaryCountedResponse>
    {
        private readonly ISender _mediator;
        public SalaryCountingCommandHandler(ISender mediator, IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext) : base(writeDbcontext, readDbcontext)
        {
            _mediator = mediator;
        }

        public override async Task<SalaryCountedResponse> Handle(SalaryCountingCommand request, CancellationToken cancellationToken)
        {
            var employee = await _readDbcontext.Employees.Where(x => x.EmployeeId == request.id).FirstOrDefaultAsync();
            var attendance = await _readDbcontext.Attendance.Where(x => x.EmployeesEmployeeId == request.id).ToListAsync();
            if (employee == null || attendance == null)
            {
                return new SalaryCountedResponse(Guid.Empty, Guid.Empty, DateTime.Now, 0, 0, 0, 0, 0, 0);
            }
            
            var workHours = attendance.Sum(x => x.WorkHours);
            var salaryBase = (float)employee.SalaryBase;
            var overtimeHours = attendance.Sum(x => x.OvertimeHours);
            var allowance = 0;
            var deduction = 0;
            var totalSalary = salaryBase * workHours + overtimeHours * 1.5 * salaryBase + allowance - deduction;
            var salary = new Domain.Entities.Salaries
            {
                SalaryId = Guid.NewGuid(),
                EmployeesEmployeeId = request.id,
                Month = DateTime.Now,
                WorkHours = (int)workHours,
                SalaryBase = salaryBase,
                OvertimeHours = overtimeHours,
                Allowance = allowance,
                Deduction = deduction,
                TotalSalary = (float)totalSalary
            };
            await _writeDbcontext.Salaries.AddAsync(salary);
            await _writeDbcontext.SaveChangesAsync(cancellationToken);
            return new SalaryCountedResponse(salary.SalaryId, salary.EmployeesEmployeeId, salary.Month, salary.WorkHours, salary.SalaryBase, (float)salary.OvertimeHours, (float)salary.Allowance, (float)salary.Deduction, (int)totalSalary);
            
        }
    }
}
