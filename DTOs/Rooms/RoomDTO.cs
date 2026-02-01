using ConferenceRoom.Api.DTOs.Reservations;

namespace ConferenceRoom.Api.DTOs.Rooms
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Capacity { get; set; }
        public List<ReservationDTO> Reservations { get; set; } = new();
    }
}
