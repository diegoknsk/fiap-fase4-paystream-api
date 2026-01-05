using FluentAssertions;
using FastFood.PayStream.Application.OutputModels;
using FastFood.PayStream.Application.Presenters;
using FastFood.PayStream.Domain.Common.Enums;

namespace FastFood.PayStream.Tests.Unit.Application.Presenters;

public class PaymentNotificationPresenterTests
{
    [Fact]
    public void Present_WhenValidOutputModel_ShouldReturnResponse()
    {
        // Arrange
        var presenter = new PaymentNotificationPresenter();
        var outputModel = new PaymentNotificationOutputModel
        {
            PaymentId = Guid.NewGuid(),
            OrderId = Guid.NewGuid(),
            Status = (int)EnumPaymentStatus.Approved,
            ExternalTransactionId = "TRX123456",
            StatusMessage = "Pagamento aprovado."
        };

        // Act
        var result = presenter.Present(outputModel);

        // Assert
        result.Should().NotBeNull();
        result.PaymentId.Should().Be(outputModel.PaymentId);
        result.OrderId.Should().Be(outputModel.OrderId);
        result.Status.Should().Be(outputModel.Status);
        result.ExternalTransactionId.Should().Be(outputModel.ExternalTransactionId);
        result.StatusMessage.Should().Be(outputModel.StatusMessage);
    }
}
