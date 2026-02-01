using ConferenceRoom.Api.DTOs.Users;
using ConferenceRoom.Api.Models;

namespace ConferenceRoom.Api.Services.UserService
{
    public interface IUserService
    {
        Task<Result<List<UserDto>>> GetAllUsersAsync(bool includeDeleted = false);
        Task<Result<UserDto>> GetUserByIdAsync(int id, bool includeDeleted = false);
        Task<Result<UserDto>> CreateUserAsync(CreateUserRequest request);
        Task<Result<UserDto>> UpdateUserAsync(int id, UpdateUserRequest request);
        Task<Result<bool>> DeleteUserAsync(int id); // soft delete
    }

}
