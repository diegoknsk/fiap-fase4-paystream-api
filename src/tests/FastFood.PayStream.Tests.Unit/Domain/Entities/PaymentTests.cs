using FastFood.PayStream.Domain.Common.Enums;
using FastFood.PayStream.Domain.Entities;

namespace FastFood.PayStream.Tests.Unit.Domain.Entities;

public class PaymentTests
{
    private readonly Guid _orderId = Guid.NewGuid();
    private readonly decimal _totalAmount = 100.50m;
    private readonly string _orderSnapshot = "{\"orderId\":\"123\",\"items\":[]}";

    [Fact]
    public void Constructor_ShouldInitializePaymentWithStatusNotStarted()
    {
        // Arrange & Act
        var payment = new Payment(_orderId, _totalAmount, _orderSnapshot);

        // Assert
        Assert.Equal(EnumPaymentStatus.NotStarted, payment.Status);
    }

    [Fact]
    public void Constructor_ShouldInitializeCreatedAtWithDateTimeUtcNow()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var payment = new Payment(_orderId, _totalAmount, _orderSnapshot);
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.True(payment.CreatedAt >= beforeCreation);
        Assert.True(payment.CreatedAt <= afterCreation);
    }

    [Fact]
    public void Constructor_ShouldInitializeIdAsNewGuid()
    {
        // Arrange & Act
        var payment = new Payment(_orderId, _totalAmount, _orderSnapshot);

        // Assert
        Assert.NotEqual(Guid.Empty, payment.Id);
    }

    [Fact]
    public void Constructor_ShouldInitializeAllPropertiesCorrectly()
    {
        // Arrange & Act
        var payment = new Payment(_orderId, _totalAmount, _orderSnapshot);

        // Assert
        Assert.Equal(_orderId, payment.OrderId);
        Assert.Equal(_totalAmount, payment.TotalAmount);
        Assert.Equal(_orderSnapshot, payment.OrderSnapshot);
        Assert.Null(payment.ExternalTransactionId);
        Assert.Null(payment.QrCodeUrl);
    }

    [Fact]
    public void Start_ShouldChangeStatusToStarted()
    {
        // Arrange
        var payment = new Payment(_orderId, _totalAmount, _orderSnapshot);

        // Act
        payment.Start();

        // Assert
        Assert.Equal(EnumPaymentStatus.Started, payment.Status);
    }

    [Fact]
    public void GenerateQrCode_ShouldThrowArgumentExceptionWhenQrCodeUrlIsNull()
    {
        // Arrange
        var payment = new Payment(_orderId, _totalAmount, _orderSnapshot);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => payment.GenerateQrCode(null!));
    }

    [Fact]
    public void GenerateQrCode_ShouldThrowArgumentExceptionWhenQrCodeUrlIsEmpty()
    {
        // Arrange
        var payment = new Payment(_orderId, _totalAmount, _orderSnapshot);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => payment.GenerateQrCode(string.Empty));
    }

    [Fact]
    public void GenerateQrCode_ShouldThrowArgumentExceptionWhenQrCodeUrlIsWhitespace()
    {
        // Arrange
        var payment = new Payment(_orderId, _totalAmount, _orderSnapshot);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => payment.GenerateQrCode("   "));
    }

    [Fact]
    public void GenerateQrCode_ShouldAssignQrCodeUrlWhenValid()
    {
        // Arrange
        var payment = new Payment(_orderId, _totalAmount, _orderSnapshot);
        var qrCodeUrl = "https://example.com/qrcode";

        // Act
        payment.GenerateQrCode(qrCodeUrl);

        // Assert
        Assert.Equal(qrCodeUrl, payment.QrCodeUrl);
    }

    [Fact]
    public void GenerateQrCode_ShouldChangeStatusToQrCodeGeneratedWhenValid()
    {
        // Arrange
        var payment = new Payment(_orderId, _totalAmount, _orderSnapshot);
        var qrCodeUrl = "https://example.com/qrcode";

        // Act
        payment.GenerateQrCode(qrCodeUrl);

        // Assert
        Assert.Equal(EnumPaymentStatus.QrCodeGenerated, payment.Status);
    }

    [Fact]
    public void Approve_ShouldThrowArgumentExceptionWhenTransactionIdIsNull()
    {
        // Arrange
        var payment = new Payment(_orderId, _totalAmount, _orderSnapshot);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => payment.Approve(null!));
    }

    [Fact]
    public void Approve_ShouldThrowArgumentExceptionWhenTransactionIdIsEmpty()
    {
        // Arrange
        var payment = new Payment(_orderId, _totalAmount, _orderSnapshot);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => payment.Approve(string.Empty));
    }

    [Fact]
    public void Approve_ShouldAssignExternalTransactionIdWhenValid()
    {
        // Arrange
        var payment = new Payment(_orderId, _totalAmount, _orderSnapshot);
        var transactionId = "TRX123456";

        // Act
        payment.Approve(transactionId);

        // Assert
        Assert.Equal(transactionId, payment.ExternalTransactionId);
    }

    [Fact]
    public void Approve_ShouldChangeStatusToApprovedWhenValid()
    {
        // Arrange
        var payment = new Payment(_orderId, _totalAmount, _orderSnapshot);
        var transactionId = "TRX123456";

        // Act
        payment.Approve(transactionId);

        // Assert
        Assert.Equal(EnumPaymentStatus.Approved, payment.Status);
    }

    [Fact]
    public void Reject_ShouldChangeStatusToRejected()
    {
        // Arrange
        var payment = new Payment(_orderId, _totalAmount, _orderSnapshot);

        // Act
        payment.Reject();

        // Assert
        Assert.Equal(EnumPaymentStatus.Rejected, payment.Status);
    }

    [Fact]
    public void Cancel_ShouldChangeStatusToCanceled()
    {
        // Arrange
        var payment = new Payment(_orderId, _totalAmount, _orderSnapshot);

        // Act
        payment.Cancel();

        // Assert
        Assert.Equal(EnumPaymentStatus.Canceled, payment.Status);
    }

    [Fact]
    public void Flow_StartGenerateQrCodeApprove_ShouldWorkCorrectly()
    {
        // Arrange
        var payment = new Payment(_orderId, _totalAmount, _orderSnapshot);
        var qrCodeUrl = "https://example.com/qrcode";
        var transactionId = "TRX123456";

        // Act
        payment.Start();
        payment.GenerateQrCode(qrCodeUrl);
        payment.Approve(transactionId);

        // Assert
        Assert.Equal(EnumPaymentStatus.Approved, payment.Status);
        Assert.Equal(qrCodeUrl, payment.QrCodeUrl);
        Assert.Equal(transactionId, payment.ExternalTransactionId);
    }

    [Fact]
    public void Flow_StartGenerateQrCodeReject_ShouldWorkCorrectly()
    {
        // Arrange
        var payment = new Payment(_orderId, _totalAmount, _orderSnapshot);
        var qrCodeUrl = "https://example.com/qrcode";

        // Act
        payment.Start();
        payment.GenerateQrCode(qrCodeUrl);
        payment.Reject();

        // Assert
        Assert.Equal(EnumPaymentStatus.Rejected, payment.Status);
        Assert.Equal(qrCodeUrl, payment.QrCodeUrl);
    }

    [Fact]
    public void Flow_StartCancel_ShouldWorkCorrectly()
    {
        // Arrange
        var payment = new Payment(_orderId, _totalAmount, _orderSnapshot);

        // Act
        payment.Start();
        payment.Cancel();

        // Assert
        Assert.Equal(EnumPaymentStatus.Canceled, payment.Status);
    }
}
