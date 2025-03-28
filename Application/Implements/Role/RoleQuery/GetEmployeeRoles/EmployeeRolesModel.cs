namespace Application.Implements.Role.RoleQuery.GetEmployeeRoles
{
    public record EmployeeRolesModel(Guid employeeId, string email, int roleId, string role);
}
