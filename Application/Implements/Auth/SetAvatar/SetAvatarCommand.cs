
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Implements.Auth.SetAvatar
{
    public record SetAvatarCommand : IRequest<SetAvatarResult>
    {
    //    public void SetId(Guid id)
    //    {
    //        Id = id;
    //    }
    //    public Guid Id { get; private set; }
        public Guid EmployeeId { get; set; }
        public IFormFile File { get; set; }
    }
}
