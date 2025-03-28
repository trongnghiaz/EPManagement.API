using Application.Common.Handler;
using Application.Common.Interface;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Implements.Employee.Query.GetDetailEmployee.GetById
{
    public class GetByIdHandler : BaseHandler<GetById, EmployeeQueryModel>
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public GetByIdHandler(IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext, ISender mediator, IMapper mapper) : base(writeDbcontext, readDbcontext)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        public override async Task<EmployeeQueryModel> Handle(GetById request, CancellationToken cancellationToken)
        {
            var employees = await (from employee in _readDbcontext.Employees
                                   where employee.EmployeeId == request.id
                                   join department in _readDbcontext.Department on employee.DepartmentId equals department.DepartmentId into emp
                                   from m in emp.DefaultIfEmpty()
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
                                       SalaryBase = employee.SalaryBase,
                                       IsActive = employee.IsActive,
                                       DepartmentId = employee.DepartmentId,
                                       Departments = m.DepartmentName,
                                   }).FirstOrDefaultAsync();

            if (employees == null)
            {
                throw new Exception("not found");
            }

            return employees;
        }
    }
}
