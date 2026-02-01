using ConferenceRoom.Api.DTOs.Rooms;
using ConferenceRoom.Api.Services.RoomService;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoom.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _service;

        public RoomsController(IRoomService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool includeDeleted = false)
        {
            var result = await _service.GetAllRoomsAsync(includeDeleted);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromQuery] bool includeDeleted = false)
        {
            var result = await _service.GetRoomByIdAsync(id, includeDeleted);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoomRequest request)
        {
            var result = await _service.CreateRoomAsync(request);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRoomRequest request)
        {
            var result = await _service.UpdateRoomAsync(id, request);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteRoomAsync(id);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }
    }

}
