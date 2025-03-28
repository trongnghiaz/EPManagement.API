using Application.Common.Interface;
using Domain.Entities;

namespace Application.Implements.Departments.Query
{
    public class DepartmentQueryModel : IMapFrom<Department>
    {
        public Guid DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public string? Address { get; set; }
        public DateTime EstablishDate { get; set; }

    }
}
