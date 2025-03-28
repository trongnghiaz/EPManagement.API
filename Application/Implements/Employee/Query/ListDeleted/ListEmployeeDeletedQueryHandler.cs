
using Application.Common.Handler;
using Application.Common.Interface;
using Application.Common.Model;
using Domain.Entities;
using MediatR;

namespace Application.Implements.Employee.Query.ListDeleted
{
    public class ListEmployeeDeletedQueryHandler : BaseHandler<ListEmployeeDeletedQuery, PagedList<ListEmployeeDeletedModel>>
    {
        private readonly ISender _mediator;
        public ListEmployeeDeletedQueryHandler(ISender mediator, IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext) : base(writeDbcontext, readDbcontext)
        {
            _mediator = mediator;
        }

        public override async Task<PagedList<ListEmployeeDeletedModel>> Handle(ListEmployeeDeletedQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Employees> employeesQuery = _readDbcontext.Employees;
            if (!string.IsNullOrWhiteSpace(request.searchTerm))
            {
                employeesQuery = employeesQuery.Where(e => e.EmployeeName.Contains(request.searchTerm));
            }            
                        
            var employees = (from employee in employeesQuery                                                                                      
                             where employee.IsDeleted == true
                             select new ListEmployeeDeletedModel
                             {
                                 EmployeeId = employee.EmployeeId,
                                 EmployeeName = employee.EmployeeName,
                                 Gender = employee.Gender,
                                 DateOfBirth = employee.DateOfBirth,
                                 PhoneNumber = employee.PhoneNumber,
                                 Address = employee.Address,
                                 Email = employee.Email,                                                                                                   
                             });

            var page = await PagedList<ListEmployeeDeletedModel>.CreateAsync(employees, request.page, request.pageSize);
            return page;
        }
    }
}
