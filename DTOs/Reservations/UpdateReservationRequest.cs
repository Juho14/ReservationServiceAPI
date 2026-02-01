using ConferenceRoom.Api.Enums;

namespace ConferenceRoom.Api.DTOs.Reservations
{
    public class UpdateReservationRequest
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ReservationStatus Status { get; set; } = ReservationStatus.Active;
    }
}