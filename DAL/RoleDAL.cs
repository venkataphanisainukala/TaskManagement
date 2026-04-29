using DAL.Entity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;

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

        #region GetById
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public Role GetById(int id)
        {
            Role role = null;
            var idParameter = new SqlParameter()
            {
                ParameterName = "@Id",
                SqlDbType = SqlDbType.Int,
                IsNullable = false,
                Value = id,
                Direction = ParameterDirection.Input,
            };

            using (var connection = CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {

                    connection.Open();

                    command.CommandText = "Sp_Role_GetById";

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(idParameter);
                    command.Connection = connection;

                    var datareader = command.ExecuteReader();
                    while (datareader.Read())
                    {
                        role = new Role()
                        {
                            Id = datareader["Id"] != DBNull.Value ? Convert.ToInt32(datareader["Id"]) : 0,
                            Name = datareader["Name"] != DBNull.Value ? Convert.ToString(datareader["Name"]) : string.Empty,
                        };
                    }

                }
                return role;

            }
        }

        #endregion

        #region GetById
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public RoleList GetList(SortWithPageParameters sortWithPageParameters)
        {
            RoleList roleList = new RoleList();
            var parms = new SqlParameter[]

                    {

                new SqlParameter(){

                    ParameterName ="@SortParameter",

                    SqlDbType = SqlDbType.NVarChar,

                    IsNullable=true,

                    Value =!string.IsNullOrEmpty(sortWithPageParameters.SortParameter)?sortWithPageParameters.SortParameter:null,

                    Direction = ParameterDirection.Input,

                },

                new SqlParameter(){

                    ParameterName ="@SortDirection",

                    SqlDbType = SqlDbType.NVarChar,

                    IsNullable=true,

                    Value=!string.IsNullOrEmpty(sortWithPageParameters.SortDirection)?sortWithPageParameters.SortDirection:null,

                    Direction = ParameterDirection.Input,

                },


                new SqlParameter(){

                    ParameterName ="@Search",

                    SqlDbType = SqlDbType.NVarChar,

                    IsNullable=true,

                    Value = sortWithPageParameters.SearchString,

                    Direction = ParameterDirection.Input,

                },

                 new SqlParameter(){

                    ParameterName ="@PageNum",

                    SqlDbType = SqlDbType.Int,

                    IsNullable=true,

                    Value = sortWithPageParameters.PageNumber.HasValue?sortWithPageParameters.PageNumber:1,

                    Direction = ParameterDirection.Input,

                },

                  new SqlParameter(){

                    ParameterName ="@PageSize",

                    SqlDbType = SqlDbType.Int,

                    IsNullable=true,

                    Value = sortWithPageParameters.PageSize.HasValue?sortWithPageParameters.PageSize:10,

                    Direction = ParameterDirection.Input,

                },



                    };

            using (var connection = CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {

                    connection.Open();

                    command.CommandText = "sp_Role_GetList";

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parms);
                    command.Connection = connection;

                    var datareader = command.ExecuteReader();
                    while (datareader.Read())
                    {
                      Role  role = new Role()
                        {
                            Id = datareader["Id"] != DBNull.Value ? Convert.ToInt32(datareader["Id"]) : 0,
                            Name = datareader["Name"] != DBNull.Value ? Convert.ToString(datareader["Name"]) : string.Empty,
                        };
                        roleList.Roles.Add(role);   
                    }

                    if (datareader.NextResult())
                    {
                        while (datareader.Read())
                        {
                            roleList.TotalCount = Convert.ToInt32(datareader["TotalCount"]);
                        }
                    }

                }
                return roleList;

            }
        }

        #endregion

    }
}
