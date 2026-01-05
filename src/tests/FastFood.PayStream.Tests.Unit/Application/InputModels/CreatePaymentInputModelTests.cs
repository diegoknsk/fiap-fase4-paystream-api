using FluentAssertions;
using FastFood.PayStream.Application.InputModels;
using System.ComponentModel.DataAnnotations;

namespace FastFood.PayStream.Tests.Unit.Application.InputModels;

public class CreatePaymentInputModelTests
{
    [Fact]
    public void OrderId_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var model = new CreatePaymentInputModel();
        var orderId = Guid.NewGuid();

        // Act
        model.OrderId = orderId;

        // Assert
        model.OrderId.Should().Be(orderId);
    }

    [Fact]
    public void TotalAmount_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var model = new CreatePaymentInputModel();
        var totalAmount = 100.50m;

        // Act
        model.TotalAmount = totalAmount;

        // Assert
        model.TotalAmount.Should().Be(totalAmount);
    }

    [Fact]
    public void OrderSnapshot_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var model = new CreatePaymentInputModel();
        var snapshot = "{\"order\":\"data\"}";

        // Act
        model.OrderSnapshot = snapshot;

        // Assert
        model.OrderSnapshot.Should().Be(snapshot);
    }

    [Fact]
    public void OrderSnapshot_WhenNotSet_ShouldReturnEmptyString()
    {
        // Arrange & Act
        var model = new CreatePaymentInputModel();

        // Assert
        model.OrderSnapshot.Should().BeEmpty();
    }

    [Fact]
    public void OrderId_ShouldHaveRequiredAttribute()
    {
        // Arrange
        var property = typeof(CreatePaymentInputModel).GetProperty(nameof(CreatePaymentInputModel.OrderId));

        // Act & Assert
        property.Should().NotBeNull();
        property!.GetCustomAttributes(typeof(RequiredAttribute), false)
            .Should().NotBeEmpty();
    }

    [Fact]
    public void TotalAmount_ShouldHaveRequiredAttribute()
    {
        // Arrange
        var property = typeof(CreatePaymentInputModel).GetProperty(nameof(CreatePaymentInputModel.TotalAmount));

        // Act & Assert
        property.Should().NotBeNull();
        property!.GetCustomAttributes(typeof(RequiredAttribute), false)
            .Should().NotBeEmpty();
    }

    [Fact]
    public void TotalAmount_ShouldHaveRangeAttribute()
    {
        // Arrange
        var property = typeof(CreatePaymentInputModel).GetProperty(nameof(CreatePaymentInputModel.TotalAmount));

        // Act & Assert
        property.Should().NotBeNull();
        property!.GetCustomAttributes(typeof(RangeAttribute), false)
            .Should().NotBeEmpty();
    }

    [Fact]
    public void OrderSnapshot_ShouldHaveRequiredAttribute()
    {
        // Arrange
        var property = typeof(CreatePaymentInputModel).GetProperty(nameof(CreatePaymentInputModel.OrderSnapshot));

        // Act & Assert
        property.Should().NotBeNull();
        property!.GetCustomAttributes(typeof(RequiredAttribute), false)
            .Should().NotBeEmpty();
    }
}
