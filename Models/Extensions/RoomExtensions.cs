using ConferenceRoom.Api.DTOs.Reservations;
using ConferenceRoom.Api.DTOs.Rooms;

namespace ConferenceRoom.Api.Models.Extensions
{
    public static class RoomExtensions
    {
        public static RoomDTO MapToDto(this RoomEntity room)
        {
            if (room == null) return null!;

            return new RoomDTO
            {
                Id = room.Id,
                Name = room.Name,
                Capacity = room.Capacity,
                Reservations = room.Reservations?.MapToDto() ?? new List<ReservationDTO>()
            };
        }

        public static RoomEntity MapToEntity(this CreateRoomRequest request)
        {
            return new RoomEntity
            {
                Name = request.Name,
                Capacity = request.Capacity
            };
        }

        public static void UpdateFromRequest(this RoomEntity room, UpdateRoomRequest request)
        {
            room.Name = request.Name;
            room.Capacity = request.Capacity;
        }


        public static List<RoomDTO> MapToDto(this IEnumerable<RoomEntity> rooms)
        {
            return rooms.Select(r => r.MapToDto()).ToList();
        }
    }
}
