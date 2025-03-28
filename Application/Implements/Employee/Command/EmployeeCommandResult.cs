namespace Application.Implements.Employee.Command
{
    public record EmployeeCommandResult(bool isSuccess, string message = null!);
}
