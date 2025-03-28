using Domain.ValueObject;

namespace Domain.Entities
{
    public class Roles : Enumeration<Roles>
    {
        public static readonly Roles Admin = new(1, "Admin");
        public static readonly Roles Manager = new(2, "Manager");
        public static readonly Roles UserView = new(3, "UserView");
        public static readonly Roles UserEdit = new(4, "UserEdit");
        public Roles(int id, string name)
            : base(id, name)
        {
        }

        public virtual ICollection<Permissions> Permissions { get; set; }

        public virtual ICollection<Employees> Employees { get; set; }
    }
}
