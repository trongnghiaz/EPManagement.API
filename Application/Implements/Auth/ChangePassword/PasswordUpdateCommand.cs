using MediatR;

namespace Application.Implements.Auth.ChangePassword
{
    public record PasswordUpdateCommand(string password, string newPassword, string confirmPassword) : IRequest<PasswordUpdateResult>
    {
        public void SetId(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; private set; }
    }
}
