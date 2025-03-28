
using FluentValidation;

namespace Application.Implements.Departments.Command.UpdateDepartment
{
    public class UpdateDepartmentValidation : AbstractValidator<UpdateDepartmentCommand>
    {
        public UpdateDepartmentValidation()
        {
            RuleFor(d => d.departmentName).NotEmpty().WithMessage("Không được để trống");
            RuleFor(d => d.address).NotEmpty().WithMessage("Không được để trống");
        }
    }
}
