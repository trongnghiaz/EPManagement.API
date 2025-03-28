using Application.Common.Handler;
using Application.Common.Interface;
using Application.Common.Model;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Implements.Employee.Query.GetListByUser
{
    public class GetListByUserQueryHandler : BaseHandler<GetListByUserQuery, PagedList<EmployeeQueryModel>>
    {
        private readonly ISender _mediator;
        public GetListByUserQueryHandler(ISender mediator, IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext) : base(writeDbcontext, readDbcontext)
        {
            _mediator = mediator;
        }

        public override async Task<PagedList<EmployeeQueryModel>> Handle(GetListByUserQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Employees> employeesQuery = _readDbcontext.Employees;
            if (!string.IsNullOrWhiteSpace(request.searchTerm))
            {
                employeesQuery = employeesQuery.Where(e => e.EmployeeName.Contains(request.searchTerm));
            }
            if (!string.IsNullOrWhiteSpace(request.sortGender.ToString()))
            {
                employeesQuery = employeesQuery.Where(e => e.Gender == request.sortGender);
            }
            if (!string.IsNullOrWhiteSpace(request.sortActive.ToString()))
            {
                employeesQuery = employeesQuery.Where(e => e.IsActive == request.sortActive);
            }
            var employees =  (from employee in employeesQuery
                              join department in _readDbcontext.Department on employee.DepartmentId equals department.DepartmentId into emp
                                   from m in emp.DefaultIfEmpty()
                                   where employee.DepartmentId == request.departmentId
                                   select new EmployeeQueryModel
                                   {
                                       EmployeeId = employee.EmployeeId,
                                       EmployeeName = employee.EmployeeName,
                                       Gender = employee.Gender,
                                       DateOfBirth = employee.DateOfBirth,
                                       PhoneNumber = employee.PhoneNumber,
                                       Address = employee.Address,
                                       Email = employee.Email,
                                       Password = employee.Password,
                                       JobTitle = employee.JobTitle,
                                       IsActive = employee.IsActive,
                                       DepartmentId = employee.DepartmentId,
                                       Departments = m.DepartmentName,
                                   });
            //List<EmployeeQueryModel> list = employees.OrderBy(e => e.EmployeeName).ToList();
            var page = await PagedList<EmployeeQueryModel>.CreateAsync(employees, request.page, request.pageSize);
            return page;
        }
    }
}
