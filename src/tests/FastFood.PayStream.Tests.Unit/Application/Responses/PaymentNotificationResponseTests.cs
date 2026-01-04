using FluentAssertions;
using FastFood.PayStream.Application.Responses;

namespace FastFood.PayStream.Tests.Unit.Application.Responses;

public class PaymentNotificationResponseTests
{
    [Fact]
    public void PaymentId_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var response = new PaymentNotificationResponse();
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
        var response = new PaymentNotificationResponse();
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
        var response = new PaymentNotificationResponse();
        var status = 4;

        // Act
        response.Status = status;

        // Assert
        response.Status.Should().Be(status);
    }

    [Fact]
    public void ExternalTransactionId_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var response = new PaymentNotificationResponse();
        var transactionId = "trans-456";

        // Act
        response.ExternalTransactionId = transactionId;

        // Assert
        response.ExternalTransactionId.Should().Be(transactionId);
    }

    [Fact]
    public void ExternalTransactionId_WhenNotSet_ShouldReturnNull()
    {
        // Arrange & Act
        var response = new PaymentNotificationResponse();

        // Assert
        response.ExternalTransactionId.Should().BeNull();
    }

    [Fact]
    public void StatusMessage_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var response = new PaymentNotificationResponse();
        var statusMessage = "Payment rejected";

        // Act
        response.StatusMessage = statusMessage;

        // Assert
        response.StatusMessage.Should().Be(statusMessage);
    }

    [Fact]
    public void StatusMessage_WhenNotSet_ShouldReturnEmptyString()
    {
        // Arrange & Act
        var response = new PaymentNotificationResponse();

        // Assert
        response.StatusMessage.Should().BeEmpty();
    }
}
