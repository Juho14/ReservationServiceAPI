namespace ConferenceRoom.Api.DTOs.Rooms
{
    public class UpdateRoomRequest
    {
        public string Name { get; set; } = null!;
        public int Capacity { get; set; }
    }
}
