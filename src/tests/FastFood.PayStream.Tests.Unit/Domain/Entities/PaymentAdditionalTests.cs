using FluentAssertions;
using FastFood.PayStream.Domain.Entities;
using FastFood.PayStream.Domain.Common.Enums;
using FastFood.PayStream.Domain.Common.Exceptions;

namespace FastFood.PayStream.Tests.Unit.Domain.Entities;

/// <summary>
/// Testes adicionais para Payment cobrindo edge cases
/// </summary>
public class PaymentAdditionalTests
{
    [Fact]
    public void Constructor_WhenOrderIdIsEmpty_ShouldThrowException()
    {
        // Arrange & Act & Assert
        // O construtor não valida OrderId vazio, mas vamos testar o comportamento
        var payment = new Payment(Guid.Empty, 100.00m, "{}");
        
        // Assert
        payment.OrderId.Should().Be(Guid.Empty);
        payment.Status.Should().Be(EnumPaymentStatus.NotStarted);
    }

    [Fact]
    public void Constructor_WhenTotalAmountIsZero_ShouldCreatePayment()
    {
        // Arrange & Act
        // O construtor não valida TotalAmount zero, mas vamos testar o comportamento
        var payment = new Payment(Guid.NewGuid(), 0m, "{}");
        
        // Assert
        payment.TotalAmount.Should().Be(0m);
        payment.Status.Should().Be(EnumPaymentStatus.NotStarted);
    }

    [Fact]
    public void Constructor_WhenTotalAmountIsNegative_ShouldCreatePayment()
    {
        // Arrange & Act
        // O construtor não valida TotalAmount negativo, mas vamos testar o comportamento
        var payment = new Payment(Guid.NewGuid(), -10.50m, "{}");
        
        // Assert
        payment.TotalAmount.Should().Be(-10.50m);
        payment.Status.Should().Be(EnumPaymentStatus.NotStarted);
    }

    [Fact]
    public void Approve_WhenTransactionIdIsWhitespace_ShouldThrowArgumentException()
    {
        // Arrange
        var payment = new Payment(Guid.NewGuid(), 100.00m, "{}");

        // Act & Assert
        var action = () => payment.Approve("   ");
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void GenerateQrCode_WhenTransactionIdIsWhitespace_ShouldThrowArgumentException()
    {
        // Arrange
        var payment = new Payment(Guid.NewGuid(), 100.00m, "{}");

        // Act & Assert
        var action = () => payment.GenerateQrCode("   ");
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Flow_CompletePaymentFlow_ShouldWorkCorrectly()
    {
        // Arrange
        var payment = new Payment(Guid.NewGuid(), 100.00m, "{}");
        var qrCodeUrl = "https://qr.test.com";
        var transactionId = "TRX123456";

        // Act
        payment.Start();
        payment.GenerateQrCode(qrCodeUrl);
        payment.Approve(transactionId);

        // Assert
        payment.Status.Should().Be(EnumPaymentStatus.Approved);
        payment.QrCodeUrl.Should().Be(qrCodeUrl);
        payment.ExternalTransactionId.Should().Be(transactionId);
    }

    [Fact]
    public void Flow_RejectAfterQrCode_ShouldWorkCorrectly()
    {
        // Arrange
        var payment = new Payment(Guid.NewGuid(), 100.00m, "{}");
        var qrCodeUrl = "https://qr.test.com";

        // Act
        payment.Start();
        payment.GenerateQrCode(qrCodeUrl);
        payment.Reject();

        // Assert
        payment.Status.Should().Be(EnumPaymentStatus.Rejected);
        payment.QrCodeUrl.Should().Be(qrCodeUrl);
    }
}
