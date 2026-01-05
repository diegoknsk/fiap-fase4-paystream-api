using FluentAssertions;
using FastFood.PayStream.Application.OutputModels;

namespace FastFood.PayStream.Tests.Unit.Application.OutputModels;

public class GenerateQrCodeOutputModelTests
{
    [Fact]
    public void QrCodeUrl_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var model = new GenerateQrCodeOutputModel();
        var qrCodeUrl = "https://example.com/qrcode";

        // Act
        model.QrCodeUrl = qrCodeUrl;

        // Assert
        model.QrCodeUrl.Should().Be(qrCodeUrl);
    }

    [Fact]
    public void QrCodeUrl_WhenNotSet_ShouldReturnEmptyString()
    {
        // Arrange & Act
        var model = new GenerateQrCodeOutputModel();

        // Assert
        model.QrCodeUrl.Should().BeEmpty();
    }

    [Fact]
    public void PaymentId_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var model = new GenerateQrCodeOutputModel();
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
        var model = new GenerateQrCodeOutputModel();
        var orderId = Guid.NewGuid();

        // Act
        model.OrderId = orderId;

        // Assert
        model.OrderId.Should().Be(orderId);
    }
}
