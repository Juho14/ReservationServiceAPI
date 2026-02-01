namespace ConferenceRoom.Api.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool Deleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
    }
}
