using ConferenceRoom.Api.DTOs.Reservations;
using ConferenceRoom.Api.Models;

namespace ConferenceRoom.Api.Services.ReservationService
{
    public interface IReservationService
    {
        Task<Result<List<ReservationDTO>>> GetAllReservationsAsync(bool includeDeleted = false);
        Task<Result<ReservationDTO>> GetReservationByIdAsync(int id, bool includeDeleted = false);
        Task<Result<ReservationDTO>> CreateReservationAsync(CreateReservationRequest request);
        Task<Result<ReservationDTO>> UpdateReservationAsync(int id, UpdateReservationRequest request);
        Task<Result<bool>> DeleteReservationAsync(int id);
    }

}
