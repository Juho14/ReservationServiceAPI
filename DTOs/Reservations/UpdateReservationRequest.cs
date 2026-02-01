namespace ConferenceRoom.Api.DTOs.Reservations
{
    public class UpdateReservationRequest
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; } = "Active";
    }
}