using ConferenceRoom.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoom.Api.Utils
{
    public static class ResponseUtils
    {
        public static IActionResult CreateResponse<T>(Result<T> result)
        {
            return result.IsSuccess ? new OkObjectResult(result.Data) : new BadRequestObjectResult(result.ErrorMessage);
        }
    }
}
