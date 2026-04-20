using DAL;
using DAL.Entity;

namespace BLL
{
    public class RoleBLL
    {
        public RoleDAL roleDAL;

        public RoleBLL()
        {
            roleDAL = new RoleDAL();
        }

        public int Create(Role role)
        {
            return roleDAL.Create(role);
        }
    }
}
