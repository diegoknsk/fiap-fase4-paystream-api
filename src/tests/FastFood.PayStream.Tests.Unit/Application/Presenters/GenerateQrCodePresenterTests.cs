using FluentAssertions;
using FastFood.PayStream.Application.OutputModels;
using FastFood.PayStream.Application.Presenters;

namespace FastFood.PayStream.Tests.Unit.Application.Presenters;

public class GenerateQrCodePresenterTests
{
    [Fact]
    public void Present_WhenValidOutputModel_ShouldReturnResponse()
    {
        // Arrange
        var presenter = new GenerateQrCodePresenter();
        var outputModel = new GenerateQrCodeOutputModel
        {
            QrCodeUrl = "https://qr.mercadopago.com/test",
            PaymentId = Guid.NewGuid(),
            OrderId = Guid.NewGuid()
        };

        // Act
        var result = presenter.Present(outputModel);

        // Assert
        result.Should().NotBeNull();
        result.QrCodeUrl.Should().Be(outputModel.QrCodeUrl);
        result.PaymentId.Should().Be(outputModel.PaymentId);
        result.OrderId.Should().Be(outputModel.OrderId);
    }
}
