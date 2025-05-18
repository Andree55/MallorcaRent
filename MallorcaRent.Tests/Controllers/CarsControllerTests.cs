using MallorcaRent.Api.Controllers;
using MallorcaRent.Domain.Entities;
using MallorcaRent.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MallorcaRent.Tests.Controllers;

public class CarsControllerTests
{
    private AppDbContext GetTestDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new AppDbContext(options);

        context.Cars.AddRange(
            new Car { Model = "Model 3", PricePerDay = 90 },
            new Car { Model = "Model S", PricePerDay = 120 }
        );

        context.SaveChanges();
        return context;
    }

    [Fact]
    public async Task Get_ReturnsListOfCars()
    {
        // Arrange
        var context = GetTestDbContext();
        var controller = new CarsController(context);

        // Act
        var result = await controller.Get();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var cars = Assert.IsType<List<Car>>(okResult.Value);
        Assert.Equal(2, cars.Count);
    }
}
