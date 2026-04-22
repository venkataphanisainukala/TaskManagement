using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BaseDAL
    {
        private  readonly string connectionstring;
        public BaseDAL(IConfiguration configuration)
        {
            this.connectionstring = configuration.GetConnectionString("TaskManagement");
        }
        //PassConnection string and it will create Connection to the Database
        public SqlConnection CreateConnection()
        {
            return new SqlConnection(connectionstring);
        }
    }
}
