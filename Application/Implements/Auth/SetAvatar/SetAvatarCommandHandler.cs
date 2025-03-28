using Application.Common.Handler;
using Application.Common.Interface;
using Domain.Helper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Implements.Auth.SetAvatar
{
    public class SetAvatarCommandHandler : BaseHandler<SetAvatarCommand, SetAvatarResult>
    {
        private readonly ISender _mediator;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SetAvatarCommandHandler(ISender mediator,IWebHostEnvironment webHostEnvironment , IManageWriteDbContext writeDbcontext, IManageReadDbContext readDbcontext) : base(writeDbcontext, readDbcontext)
        {
            _webHostEnvironment = webHostEnvironment;
            _mediator = mediator;
        }

        public override async Task<SetAvatarResult> Handle(SetAvatarCommand request, CancellationToken cancellationToken)
        {
            if(!await _readDbcontext.Employees.AnyAsync(x => x.EmployeeId == request.EmployeeId))
            {
                return new SetAvatarResult(false, Message.UserNotFound);
            }
            
            if (request.File != null)
            {
                string uploadsFolder = Path.Combine("wwwroot","avatars" ,$"{Guid.NewGuid()}{Path.GetExtension(request.File.FileName)}");
                string filePath = Path.Combine(uploadsFolder, request.File.FileName);
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    request.File.CopyTo(fileStream);
                    await _writeDbcontext.Employees
                    .Where(x => x.EmployeeId == request.EmployeeId)
                    .ExecuteUpdateAsync(x => x.SetProperty(u => u.AvatarUrl, filePath));
                    await _writeDbcontext.SaveChangesAsync();
                }
                return new SetAvatarResult(true, Message.ActionSuccess);
            }
            return new SetAvatarResult(false, "Ảnh không được để trống");
        }
    }
}
