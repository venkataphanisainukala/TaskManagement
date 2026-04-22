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
    public class RoleController : ControllerBase
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
             ApiResponse apiResponse=null;
            //id and name expected RoleSave Requqest
            //role created ,createdDate,createdBy,updatedDate,updatedBy but we dont this
            Role role = new Role()
            {
                Id= roleSaveRequest.Id,
                Name= roleSaveRequest.Name,
                CreatedBy=1
            };
         

            int result= roleBLL.Create(role);
            if (result == 0) 
            {
                apiResponse = new ApiResponse()
                {
                    Message = role.Id==0?"Role Created Successfully": "Role Updated Successfully",
                    Status = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Response= result
                };  
            }
            else if(result == 1)
            {
                apiResponse = new ApiResponse()
                {
                    ErrorMessage = "Already Exist",
                    Status = false,
                    StatusCode = (int)HttpStatusCode.Ambiguous,
                    Response = result
                };
            }
            else
            {
                apiResponse = new ApiResponse()
                {
                    ErrorMessage = "Internal server error occurred",
                    Status = false,
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Response = result
                };
            }
            return apiResponse;
        }

        #endregion
    }
}
