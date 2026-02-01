using ConferenceRoom.Api.Data;
using ConferenceRoom.Api.DTOs.Reservations;
using ConferenceRoom.Api.DTOs.Rooms;
using ConferenceRoom.Api.Models;
using ConferenceRoom.Api.Models.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoom.Api.Services.RoomService
{
    public class RoomService : IRoomService
    {
        private readonly AppDbContext _context;

        public RoomService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<RoomDTO>>> GetAllRoomsAsync(bool includeDeleted = false)
        {
            var query = includeDeleted ? _context.Rooms.IncludeDeleted() : _context.Rooms;

            var rooms = await query
                .Include(r => r.Reservations)
                .ThenInclude(res => res.User)
                .ToListAsync();

            var dtos = rooms.Select(r => r.MapToDto()).ToList();

            return Result<List<RoomDTO>>.Success(dtos);
        }

        public async Task<Result<RoomDTO>> GetRoomByIdAsync(int id, bool includeDeleted = false)
        {
            var query = includeDeleted ? _context.Rooms.IncludeDeleted() : _context.Rooms;

            var room = await query
                .Include(r => r.Reservations)
                .ThenInclude(res => res.User)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (room == null)
                return Result<RoomDTO>.Failure($"Room with id {id} not found.");

            return Result<RoomDTO>.Success(room.MapToDto());
        }


        public async Task<Result<RoomDTO>> CreateRoomAsync(CreateRoomRequest request)
        {
            var entity = new RoomEntity
            {
                Name = request.Name,
                Capacity = request.Capacity,
                CreatedAt = DateTime.UtcNow
            };

            _context.Rooms.Add(entity);
            await _context.SaveChangesAsync();

            return Result<RoomDTO>.Success(new RoomDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Capacity = entity.Capacity
            });
        }

        public async Task<Result<RoomDTO>> UpdateRoomAsync(int id, UpdateRoomRequest request)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
                return Result<RoomDTO>.Failure($"Room with id {id} not found.");

            room.Name = request.Name;
            room.Capacity = request.Capacity;

            await _context.SaveChangesAsync();

            return Result<RoomDTO>.Success(new RoomDTO
            {
                Id = room.Id,
                Name = room.Name,
                Capacity = room.Capacity
            });
        }

        public async Task<Result<bool>> DeleteRoomAsync(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
                return Result<bool>.Failure($"Room with id {id} not found.");

            room.Deleted = true;
            room.DeletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Result<bool>.Success(true);
        }
    }
}
