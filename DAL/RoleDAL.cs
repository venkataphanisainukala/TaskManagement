using DAL.Entity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DAL
{
    public class RoleDAL:BaseDAL
    {
        public RoleDAL(IConfiguration configuration) : base(configuration)
        {
        }

        #region Create

        /// <summary>
        /// Add Role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>

        public int Create(Role role)
        {
            int retVal = 0;
            int newId = 0;
            var parms = new SqlParameter[]

                   {

                new SqlParameter(){

                    ParameterName ="@LoggedInUserId",

                    SqlDbType = SqlDbType.Int,

                    IsNullable=true,

                    Value =1,

                    Direction = ParameterDirection.Input,

                },

                new SqlParameter(){

                    ParameterName ="@Name",

                    SqlDbType = SqlDbType.NVarChar,

                    IsNullable=true,

                    Value = role.Name,

                    Direction = ParameterDirection.Input,

                },



                   };
            var idParameter = new SqlParameter()
            {
                ParameterName = "@Id",
                SqlDbType = SqlDbType.Int,
                IsNullable = true,
                Value = role.Id,
                Direction = ParameterDirection.Input,
            };
            var returnParameter = new SqlParameter()

            {

                ParameterName = "@ReturnVal",

                SqlDbType = SqlDbType.Int,

                Direction = ParameterDirection.ReturnValue,

            };

            var outPutParameter = new SqlParameter()
            {
                ParameterName = "@NewId",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output,
            };

            using (var connection = CreateConnection())
            {
                using (var command = connection.CreateCommand())

                {

                    connection.Open();

                    command.CommandText = "Sp_Role_Create";

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(parms);

                    if (role.Id > 0)
                    {

                        command.Parameters.Add(idParameter);

                    }

                    command.Parameters.Add(returnParameter);
                    command.Parameters.Add(outPutParameter);
                    command.Connection = connection;

                    command.ExecuteNonQuery();

                    retVal = (int)returnParameter.Value;
                    if (retVal == 0 && role.Id == 0)
                    {
                        newId = (int)outPutParameter.Value;
                    }

                }
                return retVal;

            }
        }

        #endregion

    }
}
