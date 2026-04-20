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
    public class RoleController : ControllerBase
    {
        public RoleBLL roleBLL;
        public RoleController()
        {
            this.roleBLL = new RoleBLL();
        }

        #region Save

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleSaveRequest"></param>
        /// <returns></returns>
        [Route("Save")]
        [HttpPost]
        public ApiResponse Save(RoleSaveRequest roleSaveRequest)
        {
            ApiResponse apiResponse=null;

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
