using FluentAssertions;
using FastFood.PayStream.Application.OutputModels;
using FastFood.PayStream.Application.Presenters;
using FastFood.PayStream.Domain.Common.Enums;

namespace FastFood.PayStream.Tests.Unit.Application.Presenters;

public class CreatePaymentPresenterTests
{
    [Fact]
    public void Present_WhenValidOutputModel_ShouldReturnResponse()
    {
        // Arrange
        var presenter = new CreatePaymentPresenter();
        var outputModel = new CreatePaymentOutputModel
        {
            PaymentId = Guid.NewGuid(),
            OrderId = Guid.NewGuid(),
            Status = (int)EnumPaymentStatus.NotStarted,
            TotalAmount = 100.50m,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var result = presenter.Present(outputModel);

        // Assert
        result.Should().NotBeNull();
        result.PaymentId.Should().Be(outputModel.PaymentId);
        result.OrderId.Should().Be(outputModel.OrderId);
        result.Status.Should().Be(outputModel.Status);
        result.TotalAmount.Should().Be(outputModel.TotalAmount);
        result.CreatedAt.Should().Be(outputModel.CreatedAt);
    }
}
