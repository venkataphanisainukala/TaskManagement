using DAL.Entity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DAL
{
    public class UserDAL : BaseDAL
    {
        public UserDAL(IConfiguration configuration) : base(configuration)
        {
        }

        #region AddUser

        /// <summary>
        /// Add User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>

        public int AddUser(User user)
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

                    Value = user.Name,

                    Direction = ParameterDirection.Input,

                },

                 new SqlParameter(){

                    ParameterName ="@RoleId",

                    SqlDbType = SqlDbType.Int,

                    IsNullable=true,

                    Value = user.RoleId,

                    Direction = ParameterDirection.Input,

                },

                  new SqlParameter(){

                    ParameterName ="@Email",

                    SqlDbType = SqlDbType.NVarChar,

                    IsNullable=true,

                    Value = user.Email,

                    Direction = ParameterDirection.Input,

                },

                   new SqlParameter(){

                    ParameterName ="@Phone",

                    SqlDbType = SqlDbType.NVarChar,

                    IsNullable=true,

                    Value = user.Phone,

                    Direction = ParameterDirection.Input,

                },
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

            using (var connection  = CreateConnection())
            {
                using (var command = connection.CreateCommand())

                {

                    connection.Open();

                    command.CommandText = "Sp_User_Create";

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(parms);

                    if (user.Id > 0)
                    {

                        command.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = "@Id",
                            SqlDbType = SqlDbType.Int,
                            IsNullable = true,
                            Value = user.Id,
                            Direction = ParameterDirection.Input,
                        });

                    }

                    command.Parameters.Add(returnParameter);
                    command.Parameters.Add(outPutParameter);
                    command.Connection = connection;

                    command.ExecuteNonQuery();

                    retVal = (int)returnParameter.Value;
                    if (retVal == 0 && user.Id == 0)
                    {
                        newId = (int)outPutParameter.Value;
                    }

                }
                return retVal;

            }
        }

        #endregion

        //#region GetById
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>

        //public User GetById(int id)
        //{
        //    User user = null;
        //    var idParameter = new SqlParameter()
        //    {
        //        ParameterName = "@Id",
        //        SqlDbType = SqlDbType.Int,
        //        IsNullable = false,
        //        Value = id,
        //        Direction = ParameterDirection.Input,
        //    };

        //    using (SqlConnection connection = new SqlConnection(ConString))
        //    {
        //        using (var command = connection.CreateCommand())
        //        {

        //            connection.Open();

        //            command.CommandText = "Sp_User_GetById";

        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.Add(idParameter);
        //            command.Connection = connection;

        //            var datareader = command.ExecuteReader();

        //            user = new User()
        //            {
        //                Id = datareader["Id"] != DBNull.Value ? Convert.ToInt32(datareader["Id"]) : 0,
        //                Name = datareader["Name"] != DBNull.Value ? Convert.ToString(datareader["Name"]) : string.Empty,
        //                Email = datareader["Email"] != DBNull.Value ? Convert.ToString(datareader["Email"]) : string.Empty,
        //                Phone = datareader["Phone"] != DBNull.Value ? Convert.ToString(datareader["Phone"]) : string.Empty,
        //                RoleId = datareader["RoleId"] != DBNull.Value ? Convert.ToInt32(datareader["RoleId"]) : 0,
        //                RoleName = datareader["RoleName"] != DBNull.Value ? Convert.ToString(datareader["RoleName"]) : string.Empty,
        //            };
        //        }
        //        return user;

        //    }
        //}

        //#endregion


        //#region GetAllUsers
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>

        //public List<User> GetAllUsers()
        //{
        //    List<User> users = new List<User>();

        //    using (SqlConnection connection = new SqlConnection(ConString))
        //    {
        //        using (var command = connection.CreateCommand())
        //        {
        //            command.Parameters.AddWithValue("@Id", 1);

        //            connection.Open();

        //            command.CommandText = "Select U.Id,U.Name,U.RoleId,U.Email,U.Phone,R.Name " +
        //                "from tbl_User U  " +
        //                "INNER JOIN tbl_Role R ON R.Id=U.RoleId  " +
        //                "Where U.Id=@Id";



        //            command.CommandType = CommandType.Text;
        //            command.Connection = connection;

        //            var datareader = command.ExecuteReader();
        //            while (datareader.Read())
        //            {
        //                User user = new User()
        //                {
        //                    Id = datareader["Id"] != DBNull.Value ? Convert.ToInt32(datareader["Id"]) : 0,
        //                    Name = datareader["Name"] != DBNull.Value ? Convert.ToString(datareader["Name"]) : string.Empty,
        //                    RoleId = datareader["RoleId"] != DBNull.Value ? Convert.ToInt32(datareader["RoleId"]) : 0,
        //                    Email = datareader["Email"] != DBNull.Value ? Convert.ToString(datareader["Email"]) : string.Empty,
        //                    Phone = datareader["Phone"] != DBNull.Value ? Convert.ToString(datareader["Phone"]) : string.Empty,
        //                    //RoleName = datareader["RoleName"] != DBNull.Value ? Convert.ToString(datareader["Name"]) : string.Empty,
        //                };

        //                users.Add(user);
        //            }

        //        }
        //        return users;

        //    }
        //}

        //#endregion
    }
}