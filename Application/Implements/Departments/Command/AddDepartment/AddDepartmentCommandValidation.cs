using FluentValidation;

namespace Application.Implements.Departments.Command.AddDepartment
{
    public class AddDepartmentCommandValidation : AbstractValidator<AddDepartmentCommand>
    {
        public AddDepartmentCommandValidation()
        {
            RuleFor(d => d.departmentName).NotEmpty().WithMessage("Không được để trống");
            RuleFor(d => d.address).NotEmpty().WithMessage("Không được để trống");
        }
    }
}
