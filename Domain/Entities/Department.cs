namespace Domain.Entities
{
    public class Department
    {        
        public Guid DepartmentId {  get; set; }
        public string DepartmentName { get; set; }
        public string Address { get; set; }
        public DateTime EstablishDate { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Employees> Employees { get; set; } = new List<Employees>();

    }
}
