namespace ConferenceRoom.Api.DTOs.Reservations
{
    public class CreateReservationRequest
    {
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
