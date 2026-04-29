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

    // This controller class is a class that should have Controller in the suffix of class name and should inherit from ControllerBase class. 
    //if we say this is a API controller then we should have ApiController attribute on top of the class and also APICOntroller attribute is used to make sure that the controller is used for API and it also provides some default behavior for API controllers such as automatic model validation and automatic response formatting.
    public class RoleController : BaseController
    {
        public RoleBLL roleBLL;

        public RoleController(IConfiguration configuration)
        {
            this.roleBLL = new RoleBLL(configuration);
        }

        #region Save

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleSaveRequest"></param>
        /// <returns></returns>
        [Route("Save")]//Route attribute is used to specify the route for the action method. In this case, it is used to specify that the action method should be invoked for requests that match the route "api/Role/Save".Attribute Routing
        [HttpPost]//HttpVerb attribute is used to specify the HTTP verb for the action method. In this case, it is used to specify that the action method should be invoked for HTTP POST requests.

        public ApiResponse Save(RoleSaveRequest roleSaveRequest)
        {
            ApiResponse apiResponse = null;
            //id and name expected RoleSave Requqest
            //role created ,createdDate,createdBy,updatedDate,updatedBy but we dont this
            Role role = new Role()
            {
                Id = roleSaveRequest.Id,
                Name = roleSaveRequest.Name,
                CreatedBy = 1
            };


            int result = roleBLL.Create(role);
            if (result == 0)
            {
                apiResponse = CreateSuccessResponse(result, HttpStatusCode.Created, "RoleCreated Successfully");
            }
            else if (result == 1)
            {
                apiResponse = CreateFailedResponse(result, HttpStatusCode.Ambiguous, "Role Already Exists");
            }
            else
            {
                apiResponse = CreateFailedResponse(result);
            }
            return apiResponse;
        }

        #endregion

        #region GetById

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetById")]//Route attribute is used to specify the route for the action method. In this case, it is used to specify that the action method should be invoked for requests that match the route "api/Role/Save".Attribute Routing
        [HttpGet]//HttpVerb attribute is used to specify the HTTP verb for the action method. In this case, it is used to specify that the action method should be invoked for HTTP POST requests.

        public ApiResponse GetById(int id)
        {
            ApiResponse apiResponse = null;
            //id and name expected RoleSave Requqest
            //role created ,createdDate,createdBy,updatedDate,updatedBy but we dont this


            var result = roleBLL.GetById(id);

            if (result != null)
            {
                RoleResponse roleResponse = new RoleResponse()
                {
                    Id = result.Id,
                    Name = result.Name
                };
                apiResponse = CreateSuccessResponse(roleResponse);
            }
            else
            {
                apiResponse = CreateFailedResponse(result, HttpStatusCode.NotFound);
            }
            return apiResponse;
        }

        #endregion


        #region GetList

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetList")]//Route attribute is used to specify the route for the action method. In this case, it is used to specify that the action method should be invoked for requests that match the route "api/Role/Save".Attribute Routing
        [HttpGet]//HttpVerb attribute is used to specify the HTTP verb for the action method. In this case, it is used to specify that the action method should be invoked for HTTP POST requests.

        public ApiResponse GetList([FromQuery]SortWithPageParameters sortWithPageParameters)
        {
            ApiResponse apiResponse = null;

            var result = roleBLL.GetList(sortWithPageParameters);

            if (result != null && result.Roles.Count>0)
            {
                apiResponse = CreateSuccessResponse(result);
            }
            else
            {
                apiResponse = CreateFailedResponse(result, HttpStatusCode.NotFound);
            }
            return apiResponse;
        }

        #endregion
    }
}
