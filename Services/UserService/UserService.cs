using ConferenceRoom.Api.Data;
using ConferenceRoom.Api.DTOs.Users;
using ConferenceRoom.Api.Models;
using ConferenceRoom.Api.Models.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoom.Api.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<UserDto>>> GetAllUsersAsync(bool includeDeleted = false)
        {
            var query = includeDeleted ? _context.Users.IncludeDeleted() : _context.Users;

            var users = await query
                .Include(u => u.Reservations)
                .ThenInclude(r => r.Room)
                .ToListAsync();

            return Result<List<UserDto>>.Success(users.MapToDto());
        }

        public async Task<Result<UserDto>> GetUserByIdAsync(int id, bool includeDeleted = false)
        {
            var query = includeDeleted ? _context.Users.IncludeDeleted() : _context.Users;

            var user = await query
                .Include(u => u.Reservations)
                .ThenInclude(r => r.Room)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return Result<UserDto>.Failure($"User with id {id} not found.");

            return Result<UserDto>.Success(user.MapToDto());
        }

        public async Task<Result<UserDto>> CreateUserAsync(CreateUserRequest request)
        {
            var entity = request.MapToEntity();

            _context.Users.Add(entity);
            await _context.SaveChangesAsync();

            return Result<UserDto>.Success(entity.MapToDto());
        }

        public async Task<Result<UserDto>> UpdateUserAsync(int id, UpdateUserRequest request)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return Result<UserDto>.Failure($"User with id {id} not found.");

            user.UpdateFromRequest(request);
            await _context.SaveChangesAsync();

            return Result<UserDto>.Success(user.MapToDto());
        }

        public async Task<Result<bool>> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return Result<bool>.Failure($"User with id {id} not found.");

            user.DeleteEntity();
            await _context.SaveChangesAsync();
            return Result<bool>.Success(true);
        }
    }
}
