using ConferenceRoom.Api.Enums;

namespace ConferenceRoom.Api.Models
{
    public class ReservationEntity : BaseEntity
    {
        public int UserId { get; set; }
        public UserEntity User { get; set; } = null!;
        public int RoomId { get; set; }
        public RoomEntity Room { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ReservationStatus Status { get; set; } = ReservationStatus.Active;
    }
}
