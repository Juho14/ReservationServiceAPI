using ConferenceRoom.Api.DTOs.Rooms;
using ConferenceRoom.Api.Models;

namespace ConferenceRoom.Api.Services.RoomService
{
    public interface IRoomService
    {
        Task<Result<List<RoomDTO>>> GetAllRoomsAsync(bool includeDeleted = false);
        Task<Result<RoomDTO>> GetRoomByIdAsync(int id, bool includeDeleted = false);
        Task<Result<RoomDTO>> CreateRoomAsync(CreateRoomRequest request);
        Task<Result<RoomDTO>> UpdateRoomAsync(int id, UpdateRoomRequest request);
        Task<Result<string>> DeleteRoomAsync(int id);
    }

}
