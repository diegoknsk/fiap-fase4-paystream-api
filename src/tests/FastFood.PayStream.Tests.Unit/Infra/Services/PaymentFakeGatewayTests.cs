using FluentAssertions;
using FastFood.PayStream.Infra.Services;
using FastFood.PayStream.Application.Ports.Parameters;

namespace FastFood.PayStream.Tests.Unit.Infra.Services;

public class PaymentFakeGatewayTests
{
    private readonly PaymentFakeGateway _gateway;

    public PaymentFakeGatewayTests()
    {
        _gateway = new PaymentFakeGateway();
    }

    [Fact]
    public async Task GenerateQrCodeAsync_ShouldReturnFakeUrl()
    {
        // Arrange
        var externalReference = Guid.NewGuid().ToString();
        var orderCode = "ORDER-123";
        var items = new List<QrCodeItemModel>
        {
            new QrCodeItemModel
            {
                Title = "Item 1",
                Description = "Description 1",
                UnitPrice = 10.50m,
                Quantity = 2
            }
        };

        // Act
        var result = await _gateway.GenerateQrCodeAsync(externalReference, orderCode, items);

        // Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().Contain("fake-qrcode.example.com");
        result.Should().Contain(externalReference);
        result.Should().Contain(orderCode);
    }

    [Fact]
    public async Task GenerateQrCodeAsync_WithDifferentParameters_ShouldReturnCorrectUrl()
    {
        // Arrange
        var externalReference = "REF-456";
        var orderCode = "ORDER-789";
        var items = new List<QrCodeItemModel>();

        // Act
        var result = await _gateway.GenerateQrCodeAsync(externalReference, orderCode, items);

        // Assert
        result.Should().Be($"https://fake-qrcode.example.com/payment/{externalReference}?order={orderCode}");
    }

    [Fact]
    public async Task GetReceiptFromGatewayAsync_ShouldReturnFakeReceipt()
    {
        // Arrange
        var paymentId = Guid.NewGuid().ToString();

        // Act
        var result = await _gateway.GetReceiptFromGatewayAsync(paymentId);

        // Assert
        result.Should().NotBeNull();
        result.PaymentId.Should().Be(paymentId);
        result.ExternalReference.Should().Be(paymentId);
        result.Status.Should().Be("approved");
        result.StatusDetail.Should().Be("Pagamento aprovado (fake)");
        result.TotalPaidAmount.Should().Be(100.00m);
        result.PaymentMethod.Should().Be("pix");
        result.PaymentType.Should().Be("pix");
        result.Currency.Should().Be("BRL");
        result.DateApproved.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public async Task CheckPaymentStatusAsync_ShouldReturnApprovedStatus()
    {
        // Arrange
        var externalReference = Guid.NewGuid().ToString();

        // Act
        var result = await _gateway.CheckPaymentStatusAsync(externalReference);

        // Assert
        result.Should().NotBeNull();
        result.IsApproved.Should().BeTrue();
        result.IsPending.Should().BeFalse();
        result.IsRejected.Should().BeFalse();
        result.IsCanceled.Should().BeFalse();
        result.TransactionId.Should().Be(externalReference);
    }

    [Fact]
    public async Task CheckPaymentStatusAsync_WithDifferentReference_ShouldReturnCorrectTransactionId()
    {
        // Arrange
        var externalReference = "REF-12345";

        // Act
        var result = await _gateway.CheckPaymentStatusAsync(externalReference);

        // Assert
        result.TransactionId.Should().Be(externalReference);
    }
}
