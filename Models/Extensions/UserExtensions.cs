using ConferenceRoom.Api.DTOs.Reservations;
using ConferenceRoom.Api.DTOs.Users;

namespace ConferenceRoom.Api.Models.Extensions
{
    public static class UserExtensions
    {
        public static UserDto MapToDto(this UserEntity user)
        {
            if (user == null) return null!;

            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Reservations = user.Reservations?.MapToDto() ?? new List<ReservationDTO>()
            };
        }

        public static List<UserDto> MapToDto(this IEnumerable<UserEntity> users)
        {
            return users.Select(u => u.MapToDto()).ToList();
        }
    }
}
