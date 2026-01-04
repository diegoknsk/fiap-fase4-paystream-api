using FluentAssertions;
using FastFood.PayStream.Application.OutputModels;

namespace FastFood.PayStream.Tests.Unit.Application.OutputModels;

public class GetReceiptOutputModelTests
{
    [Fact]
    public void PaymentId_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var model = new GetReceiptOutputModel();
        var paymentId = "payment-123";

        // Act
        model.PaymentId = paymentId;

        // Assert
        model.PaymentId.Should().Be(paymentId);
    }

    [Fact]
    public void PaymentId_WhenNotSet_ShouldReturnEmptyString()
    {
        // Arrange & Act
        var model = new GetReceiptOutputModel();

        // Assert
        model.PaymentId.Should().BeEmpty();
    }

    [Fact]
    public void ExternalReference_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var model = new GetReceiptOutputModel();
        var externalRef = "ref-456";

        // Act
        model.ExternalReference = externalRef;

        // Assert
        model.ExternalReference.Should().Be(externalRef);
    }

    [Fact]
    public void Status_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var model = new GetReceiptOutputModel();
        var status = "approved";

        // Act
        model.Status = status;

        // Assert
        model.Status.Should().Be(status);
    }

    [Fact]
    public void StatusDetail_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var model = new GetReceiptOutputModel();
        var statusDetail = "Payment approved successfully";

        // Act
        model.StatusDetail = statusDetail;

        // Assert
        model.StatusDetail.Should().Be(statusDetail);
    }

    [Fact]
    public void TotalPaidAmount_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var model = new GetReceiptOutputModel();
        var totalPaid = 200.50m;

        // Act
        model.TotalPaidAmount = totalPaid;

        // Assert
        model.TotalPaidAmount.Should().Be(totalPaid);
    }

    [Fact]
    public void PaymentMethod_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var model = new GetReceiptOutputModel();
        var paymentMethod = "credit_card";

        // Act
        model.PaymentMethod = paymentMethod;

        // Assert
        model.PaymentMethod.Should().Be(paymentMethod);
    }

    [Fact]
    public void PaymentType_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var model = new GetReceiptOutputModel();
        var paymentType = "debit";

        // Act
        model.PaymentType = paymentType;

        // Assert
        model.PaymentType.Should().Be(paymentType);
    }

    [Fact]
    public void Currency_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var model = new GetReceiptOutputModel();
        var currency = "BRL";

        // Act
        model.Currency = currency;

        // Assert
        model.Currency.Should().Be(currency);
    }

    [Fact]
    public void DateApproved_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var model = new GetReceiptOutputModel();
        var dateApproved = DateTime.UtcNow;

        // Act
        model.DateApproved = dateApproved;

        // Assert
        model.DateApproved.Should().Be(dateApproved);
    }
}
