namespace ConferenceRoom.Api.Models
{
    public class UserEntity : BaseEntity
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public ICollection<ReservationEntity> Reservations { get; set; } = new List<ReservationEntity>();
    }
}
