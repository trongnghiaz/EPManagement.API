
using Domain.Enum;

namespace Application.Common.Model
{
    public class ListEmployeeRequest
    {
        public string? searchTerm {  get; set; }
        public Gender? sortGender {  get; set; }
        public bool? sortActive {  get; set; }
        public string? sortDepartment {  get; set; }
        public int page { get; set; } = 1;
        public int pageSize { get; set; } = 10;
    }
}
