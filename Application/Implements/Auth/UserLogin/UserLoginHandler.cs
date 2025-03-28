using Application.Common.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Implements.Auth.UserLogin
{
    public class UserLoginHandler : IRequestHandler<UserLogin, UserModel>
    {
        private readonly IManageReadDbContext _readDbContext;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public UserLoginHandler(IManageReadDbContext readDbContext, IJwtTokenGenerator jwtTokenGenerator) 
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _readDbContext = readDbContext;
        }
        public async Task<UserModel> Handle(UserLogin request, CancellationToken cancellationToken)
        {                        
            var user = await (from employee in _readDbContext.Employees
                              join department in _readDbContext.Department on employee.DepartmentId equals department.DepartmentId
                              join er in _readDbContext.EmployeeRoles on employee.EmployeeId equals er.EmployeesEmployeeId
                              join r in _readDbContext.Roles on er.RolesId equals r.Id
                              where employee.EmployeeId == request.UserId
                              select new UserModel(
                                  employee.EmployeeId, 
                                  employee.EmployeeName,
                                  employee.AvatarUrl,
                                  employee.Gender,
                                  employee.DateOfBirth,
                                  employee.PhoneNumber,
                                  employee.Address,
                                  employee.Email,
                                  department.DepartmentName,
                                  r.Name)).FirstAsync();
            return user;
        }
    }
}
