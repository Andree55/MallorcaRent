using MallorcaRent.Application.Dtos;
using MallorcaRent.Application.Interfaces;
using MallorcaRent.Domain.Entities;
using MallorcaRent.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MallorcaRent.Infrastructure.Repositories
{
    public class ReservationService : IReservationService
    {
        private readonly AppDbContext _context;
        public ReservationService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Reservation> CreateReservationAsync(ReservationRequestDto dto)
        {
            var car = await _context.Cars.FindAsync(dto.CarId);
            if (car == null)
                throw new Exception("Invalid Car ID");

            var totalDays = (dto.EndDate - dto.StartDate).Days;
            if (totalDays <= 0)
                throw new Exception("Invalid date range");

            var reservation = new Reservation
            {
                CarId = dto.CarId,
                PickupLocationId = dto.PickupLocationId,
                ReturnLocationId = dto.ReturnLocationId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                TotalCost = totalDays * car.PricePerDay
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return reservation;

        }

        public async Task<List<Reservation>> GetAllReservationsAsync()
        {
            return await _context.Reservations
                .Include(r => r.Car)
                .Include(r => r.PickupLocation)
                .Include(r => r.ReturnLocation)
                .ToListAsync();
        }
    }
}
