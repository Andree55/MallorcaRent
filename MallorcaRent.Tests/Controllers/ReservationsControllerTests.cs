using MallorcaRent.Api.Controllers;
using MallorcaRent.Application.Dtos;
using MallorcaRent.Application.Interfaces;
using MallorcaRent.Domain.Entities;
using MallorcaRent.Infrastructure.Data;
using MallorcaRent.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MallorcaRent.Tests.Controllers;

public class ReservationsControllerTests
{
    private AppDbContext GetTestDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new AppDbContext(options);

        var car = new Car { Model = "Model Y", PricePerDay = 100 };
        var loc1 = new Location { Name = "Palma Airport" };
        var loc2 = new Location { Name = "Manacor" };

        context.Cars.Add(car);
        context.Locations.AddRange(loc1, loc2);
        context.SaveChanges();

        return context;
    }

    [Fact]
    public async Task Post_CreatesReservationAndReturnsCost()
    {
        var context = GetTestDbContext();
        IReservationService service = new ReservationService(context);
        var controller = new ReservationsController(service);

        var dto = new ReservationRequestDto
        {
            CarId = context.Cars.First().Id,
            PickupLocationId = context.Locations.First().Id,
            ReturnLocationId = context.Locations.Skip(1).First().Id,
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddDays(3)
        };

        var result = await controller.Post(dto);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<Reservation>(ok.Value);
        Assert.Equal(300, response.TotalCost);
    }

    [Fact]
    public async Task Get_ReturnsAllReservations()
    {
        var context = GetTestDbContext();
        var service = new ReservationService(context);
        var controller = new ReservationsController(service);

        var dto = new ReservationRequestDto
        {
            CarId = context.Cars.First().Id,
            PickupLocationId = context.Locations.First().Id,
            ReturnLocationId = context.Locations.Skip(1).First().Id,
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddDays(2)
        };

        await controller.Post(dto);
        await controller.Post(dto);

        var result = await controller.Get();

        var ok = Assert.IsType<OkObjectResult>(result);
        var list = Assert.IsAssignableFrom<IEnumerable<Reservation>>(ok.Value);
        Assert.Equal(2, list.Count());
    }

    [Fact]
    public async Task Post_InvalidEndDate_ReturnsBadRequest()
    {
        var context = GetTestDbContext();
        var service = new ReservationService(context);
        var controller = new ReservationsController(service);

        var dto = new ReservationRequestDto
        {
            CarId = context.Cars.First().Id,
            PickupLocationId = context.Locations.First().Id,
            ReturnLocationId = context.Locations.Skip(1).First().Id,
            StartDate = DateTime.Today,
            EndDate = default
        };

        var result = await controller.Post(dto);

        Assert.IsType<BadRequestObjectResult>(result);
    }
    [Fact]
    public async Task DeleteAll_RemovesAllReservations()
    {
        var context = GetTestDbContext();
        var service = new ReservationService(context);
        var controller = new ReservationsController(service);

        var dto = new ReservationRequestDto
        {
            CarId = context.Cars.First().Id,
            PickupLocationId = context.Locations.First().Id,
            ReturnLocationId = context.Locations.Skip(1).First().Id,
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddDays(3)
        };

        await controller.Post(dto);
        await controller.Post(dto);

        var getBefore = await controller.Get();
        var okBefore = Assert.IsType<OkObjectResult>(getBefore);
        var listBefore = Assert.IsAssignableFrom<IEnumerable<Reservation>>(okBefore.Value);
        Assert.Equal(2, listBefore.Count());

        var delete = await controller.DeleteAll();
        Assert.IsType<NoContentResult>(delete);

        var getAfter = await controller.Get();
        var okAfter = Assert.IsType<OkObjectResult>(getAfter);
        var listAfter = Assert.IsAssignableFrom<IEnumerable<Reservation>>(okAfter.Value);
        Assert.Empty(listAfter);
    }


}
