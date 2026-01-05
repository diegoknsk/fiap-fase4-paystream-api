using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using FastFood.PayStream.Api.Controllers;

namespace FastFood.PayStream.Tests.Unit.InterfacesExternas.Controllers;

public class HealthControllerTests
{
    [Fact]
    public void Get_ShouldReturn200Ok()
    {
        // Arrange
        var controller = new HealthController();

        // Act
        var result = controller.Get();

        // Assert
        result.Should().NotBeNull();
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult!.StatusCode.Should().Be(200);
    }

    [Fact]
    public void Get_ShouldReturnHealthStatus()
    {
        // Arrange
        var controller = new HealthController();

        // Act
        var result = controller.Get();

        // Assert
        result.Should().NotBeNull();
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult!.Value.Should().NotBeNull();
        
        var value = okResult.Value;
        value.Should().NotBeNull();
    }
}
