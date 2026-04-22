using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaskManagement.Response;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
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
    }
}
