using FluentValidation;

namespace Application.Implements.Auth.ChangePassword
{
    public class ChangePasswordValidate : AbstractValidator<PasswordUpdateCommand>
    {
        public ChangePasswordValidate()
        {
            RuleFor(p => p.password).NotEmpty().WithMessage("Không được để trống");
            RuleFor(p => p.newPassword).NotEmpty().WithMessage("Không được để trống")
                .NotEqual(p => p.password).WithMessage("Mật khẩu mới phải khác mật khẩu cũ");
            RuleFor(p => p.confirmPassword).NotEmpty().WithMessage("Không được để trống")
                .Equal(p => p.newPassword).WithMessage("Phải trùng với mật khẩu mới");
        }
    }
}
