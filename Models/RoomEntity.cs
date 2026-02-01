namespace ConferenceRoom.Api.Models
{
    public class RoomEntity : BaseEntity
    {
        public string Name { get; set; } = null!;
        public int Capacity { get; set; }
        public ICollection<ReservationEntity> Reservations { get; set; } = [];
    }
}
