using MallorcaRent.Domain.Entities;

namespace MallorcaRent.Infrastructure.Data;

public static class SeedData
{
    public static void Initialize(AppDbContext context)
    {
        if (!context.Cars.Any())
        {
            context.Cars.AddRange(
                new Car { Model = "Model 3", PricePerDay = 90 },
                new Car { Model = "Model Y", PricePerDay = 100 },
                new Car { Model = "Model S", PricePerDay = 120 },
                new Car { Model = "Model X", PricePerDay = 130 }
            );
        }

        if (!context.Locations.Any())
        {
            context.Locations.AddRange(
                new Location { Name = "Palma Airport" },
                new Location { Name = "Palma City Center" },
                new Location { Name = "Alcudia" },
                new Location { Name = "Manacor" }
            );
        }

        context.SaveChanges();

        if (!context.Reservations.Any())
        {
            var car = context.Cars.First();
            var loc1 = context.Locations.First();
            var loc2 = context.Locations.Skip(1).First();

            context.Reservations.AddRange(
                new Reservation
                {
                    CarId = car.Id,
                    PickupLocationId = loc1.Id,
                    ReturnLocationId = loc2.Id,
                    StartDate = DateTime.Today.AddDays(1),
                    EndDate = DateTime.Today.AddDays(5),
                    TotalCost = 4 * car.PricePerDay
                },
                new Reservation
                {
                    CarId = car.Id,
                    PickupLocationId = loc2.Id,
                    ReturnLocationId = loc1.Id,
                    StartDate = DateTime.Today.AddDays(10),
                    EndDate = DateTime.Today.AddDays(13),
                    TotalCost = 3 * car.PricePerDay
                }
            );

            context.SaveChanges();
        }
    }
}
