
namespace Application.Implements.Departments.Query.GetListDepartmentDeleted
{
    public class DeletedDepartmentModel
    {
        public Guid DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public string? Address { get; set; }
        public DateTime EstablishDate { get; set; }
    }
}
