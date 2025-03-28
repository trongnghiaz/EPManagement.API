using Application.Common.Interface;
using Domain.Entities;
using Domain.Enum;

namespace Application.Implements.Employee.Query.ListDeleted
{
    public class ListEmployeeDeletedModel : IMapFrom<Employees>
    {
        public Guid EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }        

    }
}
