using ConferenceRoom.Api.DTOs.Reservations;

namespace ConferenceRoom.Api.DTOs.Users
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public List<ReservationDTO> Reservations { get; set; } = new();
    }
}
