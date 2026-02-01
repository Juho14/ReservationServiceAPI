using ConferenceRoom.Api.Data;
using ConferenceRoom.Api.DTOs.Reservations;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoom.Api.Models.Extensions
{
    public static class ReservationExtensions
    {
        public static ReservationDTO MapToDto(this ReservationEntity reservation)
        {
            if (reservation == null) return null!;

            return new ReservationDTO
            {
                Id = reservation.Id,
                UserId = reservation.UserId,
                UserName = reservation.User != null ? $"{reservation.User.FirstName} {reservation.User.LastName}" : string.Empty,
                RoomId = reservation.RoomId,
                RoomName = reservation.Room?.Name ?? string.Empty,
                StartTime = reservation.StartTime,
                EndTime = reservation.EndTime,
                Status = reservation.Status
            };
        }

        public static List<ReservationDTO> MapToDto(this IEnumerable<ReservationEntity> reservations)
        {
            return reservations.Select(r => r.MapToDto()).ToList();
        }

        public static bool HasValidTimeRange(this ReservationEntity reservation, out string? error)
        {
            if (reservation.EndTime <= reservation.StartTime)
            {
                error = "EndTime must be after StartTime.";
                return false;
            }

            error = null;
            return true;
        }

        public static bool HasFutureStartTime(this ReservationEntity reservation, out string? error)
        {
            if (reservation.StartTime < DateTime.UtcNow)
            {
                error = "StartTime cannot be in the past.";
                return false;
            }

            error = null;
            return true;
        }

        public static bool IsValid(this ReservationEntity reservation, out string? error)
        {
            if (!reservation.HasValidTimeRange(out error))
                return false;

            if (!reservation.HasFutureStartTime(out error))
                return false;

            error = null;
            return true;
        }


        public static async Task<bool> HasOverlappingReservationAsync(
            this ReservationEntity reservation,
            AppDbContext context)
        {
            return await context.Reservations
                .Where(r => r.UserId == reservation.UserId && r.Id != reservation.Id && !r.Deleted)
                .Where(r => r.StartTime < reservation.EndTime && r.EndTime > reservation.StartTime)
                .AnyAsync();
        }
    }
}
