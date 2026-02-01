using ConferenceRoom.Api.DTOs.Users;
using ConferenceRoom.Api.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoom.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool includeDeleted = false)
        {
            var result = await _service.GetAllUsersAsync(includeDeleted);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromQuery] bool includeDeleted = false)
        {
            var result = await _service.GetUserByIdAsync(id, includeDeleted);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
        {
            var result = await _service.CreateUserAsync(request);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserRequest request)
        {
            var result = await _service.UpdateUserAsync(id, request);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteUserAsync(id);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }
    }

}
