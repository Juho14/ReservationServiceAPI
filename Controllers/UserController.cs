using ConferenceRoom.Api.DTOs.Users;
using ConferenceRoom.Api.Services.UserService;
using ConferenceRoom.Api.Utils;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoom.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IUserService service) : ControllerBase
    {
        private readonly IUserService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool includeDeleted = false)
        {
            var result = await _service.GetAllUsersAsync(includeDeleted);
            return ResponseUtils.CreateResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromQuery] bool includeDeleted = false)
        {
            var result = await _service.GetUserByIdAsync(id, includeDeleted);
            return ResponseUtils.CreateResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
        {
            var result = await _service.CreateUserAsync(request);
            return ResponseUtils.CreateResponse(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserRequest request)
        {
            var result = await _service.UpdateUserAsync(id, request);
            return ResponseUtils.CreateResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteUserAsync(id);
            return ResponseUtils.CreateResponse(result);
        }
    }

}
