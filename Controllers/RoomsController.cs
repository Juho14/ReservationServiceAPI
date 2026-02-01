using ConferenceRoom.Api.DTOs.Rooms;
using ConferenceRoom.Api.Services.RoomService;
using ConferenceRoom.Api.Utils;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoom.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController(IRoomService service) : ControllerBase
    {
        private readonly IRoomService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool includeDeleted = false)
        {
            var result = await _service.GetAllRoomsAsync(includeDeleted);
            return ResponseUtils.CreateResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromQuery] bool includeDeleted = false)
        {
            var result = await _service.GetRoomByIdAsync(id, includeDeleted);
            return ResponseUtils.CreateResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoomRequest request)
        {
            var result = await _service.CreateRoomAsync(request);
            return ResponseUtils.CreateResponse(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRoomRequest request)
        {
            var result = await _service.UpdateRoomAsync(id, request);
            return ResponseUtils.CreateResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteRoomAsync(id);
            return ResponseUtils.CreateResponse(result);
        }
    }

}
