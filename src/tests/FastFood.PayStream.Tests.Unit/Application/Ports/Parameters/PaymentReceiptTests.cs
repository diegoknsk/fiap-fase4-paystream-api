using FluentAssertions;
using FastFood.PayStream.Application.Ports.Parameters;

namespace FastFood.PayStream.Tests.Unit.Application.Ports.Parameters;

public class PaymentReceiptTests
{
    [Fact]
    public void PaymentId_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var receipt = new PaymentReceipt();
        var paymentId = "payment-123";

        // Act
        receipt.PaymentId = paymentId;

        // Assert
        receipt.PaymentId.Should().Be(paymentId);
    }

    [Fact]
    public void PaymentId_WhenNotSet_ShouldReturnEmptyString()
    {
        // Arrange & Act
        var receipt = new PaymentReceipt();

        // Assert
        receipt.PaymentId.Should().BeEmpty();
    }

    [Fact]
    public void ExternalReference_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var receipt = new PaymentReceipt();
        var externalRef = "ref-456";

        // Act
        receipt.ExternalReference = externalRef;

        // Assert
        receipt.ExternalReference.Should().Be(externalRef);
    }

    [Fact]
    public void Status_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var receipt = new PaymentReceipt();
        var status = "approved";

        // Act
        receipt.Status = status;

        // Assert
        receipt.Status.Should().Be(status);
    }

    [Fact]
    public void StatusDetail_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var receipt = new PaymentReceipt();
        var statusDetail = "Payment processed successfully";

        // Act
        receipt.StatusDetail = statusDetail;

        // Assert
        receipt.StatusDetail.Should().Be(statusDetail);
    }

    [Fact]
    public void TotalPaidAmount_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var receipt = new PaymentReceipt();
        var totalPaid = 300.00m;

        // Act
        receipt.TotalPaidAmount = totalPaid;

        // Assert
        receipt.TotalPaidAmount.Should().Be(totalPaid);
    }

    [Fact]
    public void PaymentMethod_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var receipt = new PaymentReceipt();
        var paymentMethod = "debit_card";

        // Act
        receipt.PaymentMethod = paymentMethod;

        // Assert
        receipt.PaymentMethod.Should().Be(paymentMethod);
    }

    [Fact]
    public void PaymentType_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var receipt = new PaymentReceipt();
        var paymentType = "credit";

        // Act
        receipt.PaymentType = paymentType;

        // Assert
        receipt.PaymentType.Should().Be(paymentType);
    }

    [Fact]
    public void Currency_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var receipt = new PaymentReceipt();
        var currency = "EUR";

        // Act
        receipt.Currency = currency;

        // Assert
        receipt.Currency.Should().Be(currency);
    }

    [Fact]
    public void DateApproved_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var receipt = new PaymentReceipt();
        var dateApproved = DateTime.UtcNow;

        // Act
        receipt.DateApproved = dateApproved;

        // Assert
        receipt.DateApproved.Should().Be(dateApproved);
    }
}
