
using Application.Common.Handler;
using Application.Common.Interface;
using Application.Common.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Implements.Salaries.GetListSalaries
{
    public class ListSalariesQueryHandler : BaseHandler<ListSalariesQuery, List<ListSalariesResponse>>
    {
        private readonly ISender _mediator;
        public ListSalariesQueryHandler(ISender mediator, IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext) : base(writeDbcontext, readDbcontext)
        {
            _mediator = mediator;
        }

        public override async Task<List<ListSalariesResponse>> Handle(ListSalariesQuery request, CancellationToken cancellationToken)
        {
            
            //var month = await salariesQuery.Select(s => s.SalaryMonth).MaxAsync(cancellationToken);
            //var record = await _readDbcontext.Salaries
            //    .Where(s => s.Month.Month >= 1)
                
            //    .Select(s => new ListSalariesResponse
            //    {
            //        SalaryId = s.SalaryId,
            //        EmployeeId = s.EmployeesEmployeeId,
            //        //EmployeeName = s.Employees.EmployeeName,
            //        Month = s.Month,    
            //        SalaryBase = s.SalaryBase,
            //        WorkHours = s.WorkHours,
            //        OvertimeHours = s.OvertimeHours,
                                      
            //        TotalSalary = (float)s.TotalSalary
            //    }).ToListAsync(cancellationToken);

            var record = await _readDbcontext.Salaries
                .Where(s => s.Month.Month >= 1 && s.EmployeesEmployeeId == request.Id)
                .Include(s => s.Employees)
                .OrderBy(s => s.Month)
                .Select(s => new ListSalariesResponse
                {
                    SalaryId = s.SalaryId,
                    EmployeeId = s.EmployeesEmployeeId,
                    EmployeeName = s.Employees.EmployeeName,
                    Month = s.Month,    
                    SalaryBase = s.SalaryBase,
                    WorkHours = s.WorkHours,
                    OvertimeHours = s.OvertimeHours,                    
                    TotalSalary = (float)s.TotalSalary
                }).ToListAsync(cancellationToken);
            return record;
        }
    }
}
