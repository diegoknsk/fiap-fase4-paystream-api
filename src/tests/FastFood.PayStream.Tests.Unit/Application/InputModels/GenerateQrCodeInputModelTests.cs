using FluentAssertions;
using FastFood.PayStream.Application.InputModels;

namespace FastFood.PayStream.Tests.Unit.Application.InputModels;

public class GenerateQrCodeInputModelTests
{
    [Fact]
    public void OrderId_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var model = new GenerateQrCodeInputModel();
        var orderId = Guid.NewGuid();

        // Act
        model.OrderId = orderId;

        // Assert
        model.OrderId.Should().Be(orderId);
    }

    [Fact]
    public void FakeCheckout_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var model = new GenerateQrCodeInputModel();

        // Act
        model.FakeCheckout = true;

        // Assert
        model.FakeCheckout.Should().BeTrue();
    }

    [Fact]
    public void FakeCheckout_WhenNotSet_ShouldReturnFalse()
    {
        // Arrange & Act
        var model = new GenerateQrCodeInputModel();

        // Assert
        model.FakeCheckout.Should().BeFalse();
    }
}
