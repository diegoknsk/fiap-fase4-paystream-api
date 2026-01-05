using FluentAssertions;
using FastFood.PayStream.Application.InputModels;
using System.ComponentModel.DataAnnotations;

namespace FastFood.PayStream.Tests.Unit.Application.InputModels;

public class PaymentNotificationInputModelTests
{
    [Fact]
    public void OrderId_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var model = new PaymentNotificationInputModel();
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
        var model = new PaymentNotificationInputModel();

        // Act
        model.FakeCheckout = true;

        // Assert
        model.FakeCheckout.Should().BeTrue();
    }

    [Fact]
    public void FakeCheckout_WhenNotSet_ShouldReturnFalse()
    {
        // Arrange & Act
        var model = new PaymentNotificationInputModel();

        // Assert
        model.FakeCheckout.Should().BeFalse();
    }

    [Fact]
    public void OrderId_ShouldHaveRequiredAttribute()
    {
        // Arrange
        var property = typeof(PaymentNotificationInputModel).GetProperty(nameof(PaymentNotificationInputModel.OrderId));

        // Act & Assert
        property.Should().NotBeNull();
        property!.GetCustomAttributes(typeof(RequiredAttribute), false)
            .Should().NotBeEmpty();
    }
}
