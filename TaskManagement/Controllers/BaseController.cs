using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaskManagement.Response;

namespace TaskManagement.Controllers
{
    public class BaseController : ControllerBase
    {
        [NonAction]
        protected ApiResponse CreateSuccessResponse(object response,HttpStatusCode httpStatusCode=HttpStatusCode.OK, string message = "Success")
        {
            return new ApiResponse()
            {
                Response = response,
                Message = message,
                Status = true,
                StatusCode = (int)httpStatusCode
            };
        }

        [NonAction]
        protected ApiResponse CreateFailedResponse(object response, HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError, string errorMessage = "Failed")
        {
            return new ApiResponse()
            {
                Response = response,
                ErrorMessage = errorMessage,
                Status = false,
                StatusCode = (int)httpStatusCode
            };
        }
    }
}
