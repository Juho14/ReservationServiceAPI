using ConferenceRoom.Api.DTOs.Reservations;
using ConferenceRoom.Api.Services.ReservationService;
using ConferenceRoom.Api.Utils;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoom.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController(IReservationService service) : ControllerBase
    {
        private readonly IReservationService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool includeDeleted = false)
        {
            var result = await _service.GetAllReservationsAsync(includeDeleted);
            return ResponseUtils.CreateResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromQuery] bool includeDeleted = false)
        {
            var result = await _service.GetReservationByIdAsync(id, includeDeleted);
            return ResponseUtils.CreateResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReservationRequest request)
        {
            var result = await _service.CreateReservationAsync(request);
            return ResponseUtils.CreateResponse(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateReservationRequest request)
        {
            var result = await _service.UpdateReservationAsync(id, request);
            return ResponseUtils.CreateResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteReservationAsync(id);
            return ResponseUtils.CreateResponse(result);
        }
    }
}
