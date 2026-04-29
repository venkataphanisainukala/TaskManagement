namespace DAL.Entity
{
    public class Role : BaseEntity
    {
        //change
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class RoleList
    {
        public RoleList()
        {
            this.Roles = new List<Role>();
        }
        public List<Role> Roles { get; set; }
        public int TotalCount { get; set; }
    }
}
