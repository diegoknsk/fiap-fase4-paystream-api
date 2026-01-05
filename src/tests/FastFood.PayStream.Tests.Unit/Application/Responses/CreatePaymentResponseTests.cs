using FluentAssertions;
using FastFood.PayStream.Application.Responses;

namespace FastFood.PayStream.Tests.Unit.Application.Responses;

public class CreatePaymentResponseTests
{
    [Fact]
    public void PaymentId_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var response = new CreatePaymentResponse();
        var paymentId = Guid.NewGuid();

        // Act
        response.PaymentId = paymentId;

        // Assert
        response.PaymentId.Should().Be(paymentId);
    }

    [Fact]
    public void OrderId_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var response = new CreatePaymentResponse();
        var orderId = Guid.NewGuid();

        // Act
        response.OrderId = orderId;

        // Assert
        response.OrderId.Should().Be(orderId);
    }

    [Fact]
    public void Status_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var response = new CreatePaymentResponse();
        var status = 2;

        // Act
        response.Status = status;

        // Assert
        response.Status.Should().Be(status);
    }

    [Fact]
    public void TotalAmount_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var response = new CreatePaymentResponse();
        var totalAmount = 99.99m;

        // Act
        response.TotalAmount = totalAmount;

        // Assert
        response.TotalAmount.Should().Be(totalAmount);
    }

    [Fact]
    public void CreatedAt_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var response = new CreatePaymentResponse();
        var createdAt = DateTime.UtcNow;

        // Act
        response.CreatedAt = createdAt;

        // Assert
        response.CreatedAt.Should().Be(createdAt);
    }
}
