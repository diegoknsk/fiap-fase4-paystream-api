using FluentAssertions;
using FastFood.PayStream.Application.OutputModels;

namespace FastFood.PayStream.Tests.Unit.Application.OutputModels;

public class CreatePaymentOutputModelTests
{
    [Fact]
    public void PaymentId_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var model = new CreatePaymentOutputModel();
        var paymentId = Guid.NewGuid();

        // Act
        model.PaymentId = paymentId;

        // Assert
        model.PaymentId.Should().Be(paymentId);
    }

    [Fact]
    public void OrderId_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var model = new CreatePaymentOutputModel();
        var orderId = Guid.NewGuid();

        // Act
        model.OrderId = orderId;

        // Assert
        model.OrderId.Should().Be(orderId);
    }

    [Fact]
    public void Status_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var model = new CreatePaymentOutputModel();
        var status = 3;

        // Act
        model.Status = status;

        // Assert
        model.Status.Should().Be(status);
    }

    [Fact]
    public void TotalAmount_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var model = new CreatePaymentOutputModel();
        var totalAmount = 150.75m;

        // Act
        model.TotalAmount = totalAmount;

        // Assert
        model.TotalAmount.Should().Be(totalAmount);
    }

    [Fact]
    public void CreatedAt_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var model = new CreatePaymentOutputModel();
        var createdAt = DateTime.UtcNow;

        // Act
        model.CreatedAt = createdAt;

        // Assert
        model.CreatedAt.Should().Be(createdAt);
    }
}
