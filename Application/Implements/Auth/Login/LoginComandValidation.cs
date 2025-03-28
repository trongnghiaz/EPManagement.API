using FluentValidation;

namespace Application.Implements.Auth.Login
{
    public class LoginComandValidation : AbstractValidator<LoginCommand>
    {
        public LoginComandValidation()
        {
            RuleFor(u => u.Email).NotEmpty().WithMessage("Khong duoc de trong");

            RuleFor(p => p.Password).NotEmpty().WithMessage("Khong duoc de trong");

        }
    }
}
