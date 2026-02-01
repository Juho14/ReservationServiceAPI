namespace ConferenceRoom.Api.Models.Extensions
{
    public static class BaseEntityExtensions
    {
        public static void DeleteEntity(this BaseEntity entity)
        {
            entity.Deleted = true;
            entity.DeletedAt = DateTime.UtcNow;
        }
    }
}
