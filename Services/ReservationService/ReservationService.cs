using ConferenceRoom.Api.Data;
using ConferenceRoom.Api.DTOs.Reservations;
using ConferenceRoom.Api.Models;
using ConferenceRoom.Api.Models.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoom.Api.Services.ReservationService
{
    public class ReservationService : IReservationService
    {
        private readonly AppDbContext _context;

        public ReservationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<ReservationDTO>>> GetAllReservationsAsync(bool includeDeleted = false)
        {
            var query = includeDeleted ? _context.Reservations.IncludeDeleted() : _context.Reservations;

            var reservations = await query
                .Include(r => r.User)
                .Include(r => r.Room)
                .ToListAsync();

            return Result<List<ReservationDTO>>.Success(reservations.MapToDto());
        }

        public async Task<Result<ReservationDTO>> GetReservationByIdAsync(int id, bool includeDeleted = false)
        {
            var query = includeDeleted ? _context.Reservations.IncludeDeleted() : _context.Reservations;

            var reservation = await query
                .Include(r => r.User)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
                return Result<ReservationDTO>.Failure($"Reservation with id {id} not found.");

            return Result<ReservationDTO>.Success(reservation.MapToDto());
        }

        public async Task<Result<ReservationDTO>> CreateReservationAsync(CreateReservationRequest request)
        {
            var overlapping = await _context.Reservations
                .Where(r => r.RoomId == request.RoomId &&
                            r.EndTime > request.StartTime &&
                            r.StartTime < request.EndTime &&
                            !r.Deleted)
                .AnyAsync();

            if (overlapping)
                return Result<ReservationDTO>.Failure("This room is already booked during the selected time.");

            var entity = request.MapToEntity();

            if (!entity.IsValid(out var error))
                return Result<ReservationDTO>.Failure(error);

            if (await entity.HasOverlappingReservationAsync(_context))
                return Result<ReservationDTO>.Failure("User already has a reservation during this time.");

            _context.Reservations.Add(entity);
            await _context.SaveChangesAsync();

            var dto = await GetReservationByIdAsync(entity.Id, includeDeleted: true);
            return dto;
        }

        public async Task<Result<ReservationDTO>> UpdateReservationAsync(int id, UpdateReservationRequest request)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
                return Result<ReservationDTO>.Failure($"Reservation with id {id} not found.");

            if (reservation.StartTime.Date <= DateTime.UtcNow.Date)
                return Result<ReservationDTO>.Failure("Cannot update a reservation on the same day.");

            var overlapping = await _context.Reservations
                .Where(r => r.Id != id &&
                            r.RoomId == reservation.RoomId &&
                            r.EndTime > request.StartTime &&
                            r.StartTime < request.EndTime &&
                            !r.Deleted)
                .AnyAsync();

            if (overlapping)
                return Result<ReservationDTO>.Failure("This room is already booked during the selected time.");

            if (!reservation.IsValid(out var error))
                return Result<ReservationDTO>.Failure(error);

            if (await reservation.HasOverlappingReservationAsync(_context))
                return Result<ReservationDTO>.Failure("User already has a reservation during this time.");

            reservation.UpdateFromRequest(request);

            await _context.SaveChangesAsync();

            return await GetReservationByIdAsync(id, includeDeleted: true);
        }

        public async Task<Result<bool>> DeleteReservationAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
                return Result<bool>.Failure($"Reservation with id {id} not found.");

            reservation.DeleteEntity();
            await _context.SaveChangesAsync();
            return Result<bool>.Success(true);
        }
    }
}
