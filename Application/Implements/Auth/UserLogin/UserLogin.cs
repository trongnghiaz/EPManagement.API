using MediatR;

namespace Application.Implements.Auth.UserLogin
{
    public class UserLogin : IRequest<UserModel>
    {
        public Guid UserId { get; set; }
    }        
}
