using FluentAssertions;
using FastFood.PayStream.Application.OutputModels;

namespace FastFood.PayStream.Tests.Unit.Application.OutputModels;

public class PaymentNotificationOutputModelTests
{
    [Fact]
    public void PaymentId_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var model = new PaymentNotificationOutputModel();
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
        var model = new PaymentNotificationOutputModel();
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
        var model = new PaymentNotificationOutputModel();
        var status = 3;

        // Act
        model.Status = status;

        // Assert
        model.Status.Should().Be(status);
    }

    [Fact]
    public void ExternalTransactionId_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var model = new PaymentNotificationOutputModel();
        var transactionId = "trans-123";

        // Act
        model.ExternalTransactionId = transactionId;

        // Assert
        model.ExternalTransactionId.Should().Be(transactionId);
    }

    [Fact]
    public void ExternalTransactionId_WhenNotSet_ShouldReturnNull()
    {
        // Arrange & Act
        var model = new PaymentNotificationOutputModel();

        // Assert
        model.ExternalTransactionId.Should().BeNull();
    }

    [Fact]
    public void StatusMessage_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var model = new PaymentNotificationOutputModel();
        var statusMessage = "Payment approved";

        // Act
        model.StatusMessage = statusMessage;

        // Assert
        model.StatusMessage.Should().Be(statusMessage);
    }

    [Fact]
    public void StatusMessage_WhenNotSet_ShouldReturnEmptyString()
    {
        // Arrange & Act
        var model = new PaymentNotificationOutputModel();

        // Assert
        model.StatusMessage.Should().BeEmpty();
    }
}
