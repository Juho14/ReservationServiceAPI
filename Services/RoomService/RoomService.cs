using ConferenceRoom.Api.Data;
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
            var rooms = await _context.Rooms
                .IncludeDeleted(includeDeleted)
                .Include(r => r.Reservations)
                .ThenInclude(res => res.User)
                .ToListAsync();

            var dtos = rooms.Select(r => r.MapToDto()).ToList();

            return Result<List<RoomDTO>>.Success(dtos);
        }

        public async Task<Result<RoomDTO>> GetRoomByIdAsync(int id, bool includeDeleted = false)
        {
            var room = await _context.Rooms
                .IncludeDeleted(includeDeleted)
                .Include(r => r.Reservations)
                .ThenInclude(res => res.User)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (room == null)
                return Result<RoomDTO>.Failure($"Room with id {id} not found.");

            return Result<RoomDTO>.Success(room.MapToDto());
        }


        public async Task<Result<RoomDTO>> CreateRoomAsync(CreateRoomRequest request)
        {
            var entity = request.MapToEntity();
            _context.Rooms.Add(entity);
            await _context.SaveChangesAsync();

            return Result<RoomDTO>.Success(entity.MapToDto());
        }

        public async Task<Result<RoomDTO>> UpdateRoomAsync(int id, UpdateRoomRequest request)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
                return Result<RoomDTO>.Failure($"Room with id {id} not found.");

            room.UpdateFromRequest(request);
            await _context.SaveChangesAsync();
            return Result<RoomDTO>.Success(room.MapToDto());
        }

        public async Task<Result<string>> DeleteRoomAsync(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
                return Result<string>.Failure($"Room with id {id} not found.");

            room.DeleteEntity();

            await _context.SaveChangesAsync();
            return Result<string>.Success("Deletion successful!");
        }
    }
}
