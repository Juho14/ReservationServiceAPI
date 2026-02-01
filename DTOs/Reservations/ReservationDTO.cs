namespace ConferenceRoom.Api.DTOs.Reservations
{
    public class ReservationDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public int RoomId { get; set; }
        public string RoomName { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; } = null!;
    }
}
