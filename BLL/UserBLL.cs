using DAL;
using DAL.Entity;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class UserBLL
    {
        public UserDAL userDAL;

        public UserBLL(IConfiguration configuration)
        {
            userDAL = new UserDAL(configuration);
        }

        public int AddUser(User user)
        {
            return userDAL.AddUser(user);
        }

        //public Role GetById(int id)
        //{
        //    return roleDAL.GetById(id);
        //}
    }
}

