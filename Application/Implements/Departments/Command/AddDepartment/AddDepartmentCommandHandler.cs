using Application.Common.Handler;
using Application.Common.Interface;
using AutoMapper;
using Domain.Entities;
using Domain.Helper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Implements.Departments.Command.AddDepartment
{
    public class AddDepartmentCommandHandler : BaseHandler<AddDepartmentCommand, DepartmentCommandResult>
    {

        private readonly ISender _mediator;
        private readonly IRandomPasswordString _randomString;
        public AddDepartmentCommandHandler(IRandomPasswordString randomString, ISender mediator, IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext) : base(writeDbcontext, readDbcontext)
        {
            _randomString = randomString;
            _mediator = mediator;
        }
        
        public override async Task<DepartmentCommandResult> Handle(AddDepartmentCommand request, CancellationToken cancellationToken)
        {
            Guid DepartmentId = Guid.NewGuid();
            var add = new Department()
            {
                DepartmentId = DepartmentId,
                DepartmentName = request.departmentName,
                Address = request.address,
                EstablishDate = DateTime.UtcNow,
            };            
            await _writeDbcontext.Department.AddAsync(add);                                
            await _writeDbcontext.SaveChangesAsync();
            return new DepartmentCommandResult(true, Message.CreatedSuccess);                        
        }
    }
}
