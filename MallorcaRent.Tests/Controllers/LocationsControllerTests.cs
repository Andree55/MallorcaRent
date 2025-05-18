using MallorcaRent.Api.Controllers;
using MallorcaRent.Domain.Entities;
using MallorcaRent.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MallorcaRent.Tests.Controllers;

public class LocationsControllerTests
{
    private AppDbContext GetTestDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new AppDbContext(options);

        context.Locations.AddRange(
            new Location { Name = "Palma Airport" },
            new Location { Name = "Alcudia" }
        );

        context.SaveChanges();
        return context;
    }

    [Fact]
    public async Task Get_ReturnsListOfLocations()
    {
        var context = GetTestDbContext();
        var controller = new LocationsController(context);

        var result = await controller.Get();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var locations = Assert.IsType<List<Location>>(okResult.Value);
        Assert.Equal(2, locations.Count);
    }
}
