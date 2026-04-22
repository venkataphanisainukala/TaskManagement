using DAL;
using DAL.Entity;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class RoleBLL
    {
        public RoleDAL roleDAL;

        public RoleBLL(IConfiguration configuration)
        {
            roleDAL = new RoleDAL(configuration);
        }

        public int Create(Role role)
        {
            return roleDAL.Create(role);
        }
    }
}
