


namespace Application.Implements.Salaries.SalaryCounting
{
    public record SalaryCountedResponse(
        Guid id, 
        Guid employeeId, 
        DateTime month, 
        float workHours,
        float salaryBase,
        float overtimeSalary, 
        float allowance,
        float deduction,
        float totalSalary
        );
    
}
