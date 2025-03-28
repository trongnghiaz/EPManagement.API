using FluentValidation;

namespace Application.Implements.Employee.Command.UpdateEmployee
{
    public class UpdateEmployeeCommandValidation : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidation()
        {

            RuleFor(e => e.employeeName).NotEmpty().WithMessage("Tên không được để trống");

            RuleFor(e => e.phoneNumber).NotEmpty().WithMessage("SDT không được để trống");

            RuleFor(e => e.email).EmailAddress().WithMessage("Email không hợp lệ")
                .Must(ContainAtAndDot).WithMessage("Email phải thuộc kiểu: example@xx.x");

            RuleFor(e => e.address).NotEmpty().WithMessage("Địa chỉ không được để trống");

            RuleFor(e => e.dateOfBirth).NotEmpty().WithMessage("Ngày sinh không được trống");

            RuleFor(e => e.departmentId).NotEmpty().WithMessage("Phải chọn một phòng ban cho nhân sự này");
        }
        private bool ContainAtAndDot(string email)
        {
            return email.Contains("@") && email.Contains(".");
        }
    }
}
