using System.ComponentModel.DataAnnotations;

namespace DAL.Entity
{
    public class User : BaseEntity
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }

    }
}
