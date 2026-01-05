using FluentAssertions;
using FastFood.PayStream.Application.Responses;

namespace FastFood.PayStream.Tests.Unit.Application.Responses;

public class GenerateQrCodeResponseTests
{
    [Fact]
    public void QrCodeUrl_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var response = new GenerateQrCodeResponse();
        var qrCodeUrl = "https://mercadopago.com/qr/123";

        // Act
        response.QrCodeUrl = qrCodeUrl;

        // Assert
        response.QrCodeUrl.Should().Be(qrCodeUrl);
    }

    [Fact]
    public void QrCodeUrl_WhenNotSet_ShouldReturnEmptyString()
    {
        // Arrange & Act
        var response = new GenerateQrCodeResponse();

        // Assert
        response.QrCodeUrl.Should().BeEmpty();
    }

    [Fact]
    public void PaymentId_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var response = new GenerateQrCodeResponse();
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
        var response = new GenerateQrCodeResponse();
        var orderId = Guid.NewGuid();

        // Act
        response.OrderId = orderId;

        // Assert
        response.OrderId.Should().Be(orderId);
    }
}
