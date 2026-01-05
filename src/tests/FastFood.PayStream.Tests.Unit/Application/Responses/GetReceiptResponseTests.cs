using FluentAssertions;
using FastFood.PayStream.Application.Responses;

namespace FastFood.PayStream.Tests.Unit.Application.Responses;

public class GetReceiptResponseTests
{
    [Fact]
    public void PaymentId_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var response = new GetReceiptResponse();
        var paymentId = "payment-789";

        // Act
        response.PaymentId = paymentId;

        // Assert
        response.PaymentId.Should().Be(paymentId);
    }

    [Fact]
    public void PaymentId_WhenNotSet_ShouldReturnEmptyString()
    {
        // Arrange & Act
        var response = new GetReceiptResponse();

        // Assert
        response.PaymentId.Should().BeEmpty();
    }

    [Fact]
    public void ExternalReference_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var response = new GetReceiptResponse();
        var externalRef = "ext-ref-123";

        // Act
        response.ExternalReference = externalRef;

        // Assert
        response.ExternalReference.Should().Be(externalRef);
    }

    [Fact]
    public void Status_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var response = new GetReceiptResponse();
        var status = "pending";

        // Act
        response.Status = status;

        // Assert
        response.Status.Should().Be(status);
    }

    [Fact]
    public void StatusDetail_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var response = new GetReceiptResponse();
        var statusDetail = "Waiting for payment";

        // Act
        response.StatusDetail = statusDetail;

        // Assert
        response.StatusDetail.Should().Be(statusDetail);
    }

    [Fact]
    public void TotalPaidAmount_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var response = new GetReceiptResponse();
        var totalPaid = 250.00m;

        // Act
        response.TotalPaidAmount = totalPaid;

        // Assert
        response.TotalPaidAmount.Should().Be(totalPaid);
    }

    [Fact]
    public void PaymentMethod_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var response = new GetReceiptResponse();
        var paymentMethod = "pix";

        // Act
        response.PaymentMethod = paymentMethod;

        // Assert
        response.PaymentMethod.Should().Be(paymentMethod);
    }

    [Fact]
    public void PaymentType_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var response = new GetReceiptResponse();
        var paymentType = "instant";

        // Act
        response.PaymentType = paymentType;

        // Assert
        response.PaymentType.Should().Be(paymentType);
    }

    [Fact]
    public void Currency_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var response = new GetReceiptResponse();
        var currency = "USD";

        // Act
        response.Currency = currency;

        // Assert
        response.Currency.Should().Be(currency);
    }

    [Fact]
    public void DateApproved_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var response = new GetReceiptResponse();
        var dateApproved = DateTime.UtcNow;

        // Act
        response.DateApproved = dateApproved;

        // Assert
        response.DateApproved.Should().Be(dateApproved);
    }
}
