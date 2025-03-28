using Application.Common.Handler;
using Application.Common.Interface;
using Application.Common.Model;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Implements.Role.RoleQuery.GetEmployeeRoles
{
    public class EmployeeRolesQueryHandler : BaseHandler<EmployeeRolesQuery, PagedList<EmployeeRolesModel>>
    {
        private readonly ISender _mediator;
        public EmployeeRolesQueryHandler(IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext, ISender mediator) : base(writeDbcontext, readDbcontext)
        {
            _mediator = mediator;
        }
        public override async Task<PagedList<EmployeeRolesModel>> Handle(EmployeeRolesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Roles> roleQuery = _readDbcontext.Roles;
            IQueryable<Employees> employeeQuery = _readDbcontext.Employees;

            if(!string.IsNullOrWhiteSpace(request.searchEmail))
            {
                employeeQuery = employeeQuery.Where(e => e.Email.Contains(request.searchEmail));
            }

            if(!string.IsNullOrWhiteSpace(request.roleId))
            {
                roleQuery = roleQuery.Where(r => r.Id == int.Parse(request.roleId));
            }

            var employeeRoles =  (from employee in employeeQuery
                                       join empRole in _readDbcontext.EmployeeRoles on employee.EmployeeId equals empRole.EmployeesEmployeeId
                                       join roles in roleQuery on empRole.RolesId equals roles.Id
                                       select new EmployeeRolesModel(employee.EmployeeId, employee.Email, roles.Id, roles.Name));
            var result = await PagedList<EmployeeRolesModel>.CreateAsync(employeeRoles, request.page, request.pageSize);
            return result;
        }
    }
}
