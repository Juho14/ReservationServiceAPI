namespace ConferenceRoom.Api.DTOs.Rooms
{
    public class CreateRoomRequest
    {
        public string Name { get; set; } = null!;
        public int Capacity { get; set; }
    }
}
