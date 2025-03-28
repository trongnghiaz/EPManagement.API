using Application.Common.Handler;
using Application.Common.Interface;
using AutoMapper;
using Domain.Entities;
using Domain.Helper;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Application.Implements.Employee.Command.AddEmployee
{
    public class AddEmployeeCommandHandler : BaseHandler<AddEmployeeCommand, EmployeeCommandResult>
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        private readonly IRandomPasswordString _randomPassword;
        private readonly ISQSService _sqsservice;
        public AddEmployeeCommandHandler(ISQSService sQSService, IRandomPasswordString passwordString, ISender mediator, IMapper mapper, IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext) : base(writeDbcontext, readDbcontext)
        {
            _sqsservice = sQSService;
            _randomPassword = passwordString;
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task<EmployeeCommandResult> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            Guid departmentId = Guid.NewGuid();
            if (!await _readDbcontext.Department.Where(d => d.DepartmentId == request.departmentId).AnyAsync(cancellationToken))
            {                                
                return new EmployeeCommandResult(false, "Phòng ban này chưa tồn tại");
            }

            if (await _readDbcontext.Employees.AnyAsync(e => e.Email == request.email, cancellationToken))
            {
                return new EmployeeCommandResult(false, Message.AlreadyExistEmail);
            }
                        
            string password = _randomPassword.GenerateRandomPassword();

            var employee = new Employees()
            {
                EmployeeName = request.employeeName,                
                Gender = request.gender,
                DateOfBirth = request.dateOfBirth,
                PhoneNumber = request.phoneNumber,
                Address = request.address,
                Email = request.email,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                JobTitle = request.jobTitle,
                SalaryBase = request.salaryBase,
                IsActive = request.isActive,
                DepartmentId = request.departmentId 
            };            
            var create = await _writeDbcontext.Employees.AddAsync(employee);                                                

            var role = new EmployeeRoles { EmployeesEmployeeId = employee.EmployeeId, RolesId = 3 };
            await _writeDbcontext.EmployeeRoles.AddAsync(role);
            await _writeDbcontext.SaveChangesAsync();
            await _sqsservice.SendMessageAsync($"Name: {employee.EmployeeName}, Email: {employee.Email}, Password: {password}");

            return new EmployeeCommandResult(true, "Tạo thành công!");
        }
    }
}
