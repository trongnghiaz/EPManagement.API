using Application.Common.Handler;
using Application.Common.Interface;
using Application.Common.Model;
using AutoMapper;
using Domain.Entities;
using Domain.Helper;
using MediatR;

namespace Application.Implements.Employee.Query.GetListEmployee
{
    public class GetListEmployeeQueryHandler : BaseHandler<GetListEmployeeQuery, PagedList<EmployeeQueryModel>>
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public GetListEmployeeQueryHandler(ISender mediator, IMapper mapper, IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext) : base(writeDbcontext, readDbcontext)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task<PagedList<EmployeeQueryModel>> Handle(GetListEmployeeQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Employees> employeesQuery = _readDbcontext.Employees;
            if (!string.IsNullOrWhiteSpace(request.searchTerm))
            {
                employeesQuery = employeesQuery.Where(e => e.EmployeeName.Contains(request.searchTerm));
            }
            if(!string.IsNullOrWhiteSpace(request.sortGender.ToString()))
            {
                employeesQuery = employeesQuery.Where(e => e.Gender == request.sortGender);
            }
            if (!string.IsNullOrWhiteSpace(request.sortActive.ToString()))
            {
                employeesQuery = employeesQuery.Where(e => e.IsActive == request.sortActive);
            }
            if (!string.IsNullOrWhiteSpace(request.sortDepartment))
            {
                employeesQuery = employeesQuery.Where(e => e.DepartmentId == Guid.Parse(request.sortDepartment));
            }
            var employees = (from employee in employeesQuery
                            join department in _readDbcontext.Department on employee.DepartmentId equals department.DepartmentId into emp
                            from m in emp.DefaultIfEmpty()  
                            where employee.EmployeeId != Guid.Parse(Admin.AdminId)
                            where employee.IsDeleted == false
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
            
            var page = await PagedList<EmployeeQueryModel>.CreateAsync(employees, request.page, request.pageSize);
            return page;
        }
    }
}
