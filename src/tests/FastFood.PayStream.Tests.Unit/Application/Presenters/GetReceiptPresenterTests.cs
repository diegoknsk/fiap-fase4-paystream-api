using FluentAssertions;
using FastFood.PayStream.Application.OutputModels;
using FastFood.PayStream.Application.Presenters;

namespace FastFood.PayStream.Tests.Unit.Application.Presenters;

public class GetReceiptPresenterTests
{
    [Fact]
    public void Present_WhenValidOutputModel_ShouldReturnResponse()
    {
        // Arrange
        var presenter = new GetReceiptPresenter();
        var outputModel = new GetReceiptOutputModel
        {
            PaymentId = Guid.NewGuid().ToString(),
            ExternalReference = "EXT-123",
            Status = "approved",
            StatusDetail = "accredited",
            TotalPaidAmount = 100.50m,
            PaymentMethod = "pix",
            PaymentType = "bank_transfer",
            Currency = "BRL",
            DateApproved = DateTime.UtcNow
        };

        // Act
        var result = presenter.Present(outputModel);

        // Assert
        result.Should().NotBeNull();
        result.PaymentId.Should().Be(outputModel.PaymentId);
        result.ExternalReference.Should().Be(outputModel.ExternalReference);
        result.Status.Should().Be(outputModel.Status);
        result.StatusDetail.Should().Be(outputModel.StatusDetail);
        result.TotalPaidAmount.Should().Be(outputModel.TotalPaidAmount);
        result.PaymentMethod.Should().Be(outputModel.PaymentMethod);
        result.PaymentType.Should().Be(outputModel.PaymentType);
        result.Currency.Should().Be(outputModel.Currency);
        result.DateApproved.Should().Be(outputModel.DateApproved);
    }
}
