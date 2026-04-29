using BLL;
using DAL.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaskManagement.Request;
using TaskManagement.Response;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
            public UserBLL userBLL;

            public UserController(IConfiguration configuration)
            {
                this.userBLL = new UserBLL(configuration);
            }

        #region Save

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userSaveRequest"></param>
        /// <returns></returns>
        [Route("Save")]//Route attribute is used to specify the route for the action method. In this case, it is used to specify that the action method should be invoked for requests that match the route "api/Role/Save".Attribute Routing
            [HttpPost]//HttpVerb attribute is used to specify the HTTP verb for the action method. In this case, it is used to specify that the action method should be invoked for HTTP POST requests.

            public ApiResponse Save(UserSaveRequest userSaveRequest)
            {
                ApiResponse apiResponse = null;
                //id and name expected RoleSave Requqest
                //role created ,createdDate,createdBy,updatedDate,updatedBy but we dont this
                User user = new User()
                {
                    Id = userSaveRequest.Id,
                    Name = userSaveRequest.Name,
                    RoleId = userSaveRequest.RoleId,
                    Email   = userSaveRequest.Email,
                    Phone   = userSaveRequest.Phone,
                    CreatedBy = 1
                };


                int result = userBLL.AddUser(user);
                if (result == 0)
                {
                    apiResponse = CreateSuccessResponse(result, HttpStatusCode.Created, "User Created Successfully");
                }
                else if (result == 1)
                {
                    apiResponse = CreateFailedResponse(result, HttpStatusCode.Ambiguous, "User Already Exists");
                }
                else
                {
                    apiResponse = CreateFailedResponse(result);
                }
                return apiResponse;
            }

            #endregion
        }
    }
