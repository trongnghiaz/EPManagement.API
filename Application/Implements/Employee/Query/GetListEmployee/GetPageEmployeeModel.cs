
namespace Application.Implements.Employee.Query.GetListEmployee
{
    public record GetPageEmployeeModel(int totalPage, int page, IEnumerable<EmployeeQueryModel> employees);
    
}
