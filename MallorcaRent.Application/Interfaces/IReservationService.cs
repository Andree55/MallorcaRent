using MallorcaRent.Application.Dtos;
using MallorcaRent.Domain.Entities;

namespace MallorcaRent.Application.Interfaces
{
    public interface IReservationService
    {
        Task<Reservation> CreateReservationAsync(ReservationRequestDto dto);
        Task<List<Reservation>> GetAllReservationsAsync();
    }
}
