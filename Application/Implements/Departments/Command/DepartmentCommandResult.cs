namespace Application.Implements.Departments.Command
{
    public record DepartmentCommandResult(bool isSuccess, string message = null!);
}
