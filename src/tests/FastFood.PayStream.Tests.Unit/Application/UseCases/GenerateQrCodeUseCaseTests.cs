using Moq;
using FluentAssertions;
using FastFood.PayStream.Application.InputModels;
using FastFood.PayStream.Application.UseCases;
using FastFood.PayStream.Application.Ports;
using FastFood.PayStream.Application.Ports.Parameters;
using FastFood.PayStream.Application.Presenters;
using FastFood.PayStream.Domain.Entities;
using FastFood.PayStream.Domain.Common.Enums;

namespace FastFood.PayStream.Tests.Unit.Application.UseCases;

public class GenerateQrCodeUseCaseTests
{
    private readonly Mock<IPaymentRepository> _paymentRepositoryMock;
    private readonly Mock<IPaymentGateway> _realPaymentGatewayMock;
    private readonly Mock<IPaymentGateway> _fakePaymentGatewayMock;
    private readonly GenerateQrCodePresenter _presenter;
    private readonly GenerateQrCodeUseCase _useCase;

    public GenerateQrCodeUseCaseTests()
    {
        _paymentRepositoryMock = new Mock<IPaymentRepository>();
        _realPaymentGatewayMock = new Mock<IPaymentGateway>();
        _fakePaymentGatewayMock = new Mock<IPaymentGateway>();
        _presenter = new GenerateQrCodePresenter();
        _useCase = new GenerateQrCodeUseCase(
            _paymentRepositoryMock.Object,
            _realPaymentGatewayMock.Object,
            _fakePaymentGatewayMock.Object,
            _presenter);
    }

    [Fact]
    public async Task ExecuteAsync_WhenOrderIdIsEmpty_ShouldThrowArgumentException()
    {
        // Arrange
        var input = new GenerateQrCodeInputModel
        {
            OrderId = Guid.Empty,
            FakeCheckout = false
        };

        // Act & Assert
        var action = async () => await _useCase.ExecuteAsync(input);
        await action.Should().ThrowAsync<ArgumentException>()
            .WithMessage("OrderId não pode ser vazio.*");
    }

    [Fact]
    public async Task ExecuteAsync_WhenPaymentNotFound_ShouldThrowApplicationException()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var input = new GenerateQrCodeInputModel
        {
            OrderId = orderId,
            FakeCheckout = false
        };

        _paymentRepositoryMock
            .Setup(r => r.GetByOrderIdAsync(orderId))
            .ReturnsAsync((Payment?)null);

        // Act & Assert
        var action = async () => await _useCase.ExecuteAsync(input);
        await action.Should().ThrowAsync<ApplicationException>()
            .WithMessage($"Pagamento não encontrado para o OrderId: {orderId}");
    }

    [Fact]
    public async Task ExecuteAsync_WhenOrderSnapshotIsNull_ShouldThrowApplicationException()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var payment = new Payment(orderId, 100.00m, null!);
        var input = new GenerateQrCodeInputModel
        {
            OrderId = orderId,
            FakeCheckout = false
        };

        _paymentRepositoryMock
            .Setup(r => r.GetByOrderIdAsync(orderId))
            .ReturnsAsync(payment);

        // Act & Assert
        var action = async () => await _useCase.ExecuteAsync(input);
        await action.Should().ThrowAsync<ApplicationException>()
            .WithMessage($"Pagamento {payment.Id} não possui OrderSnapshot válido.*");
    }

    [Fact]
    public async Task ExecuteAsync_WhenOrderSnapshotIsInvalidJson_ShouldThrowApplicationException()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var payment = new Payment(orderId, 100.00m, "invalid json");
        var input = new GenerateQrCodeInputModel
        {
            OrderId = orderId,
            FakeCheckout = false
        };

        _paymentRepositoryMock
            .Setup(r => r.GetByOrderIdAsync(orderId))
            .ReturnsAsync(payment);

        // Act & Assert
        var action = async () => await _useCase.ExecuteAsync(input);
        await action.Should().ThrowAsync<ApplicationException>()
            .WithMessage("Erro ao deserializar OrderSnapshot:*");
    }

    [Fact]
    public async Task ExecuteAsync_WhenValidInput_ShouldReturnQrCodeUrl()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var paymentId = Guid.NewGuid();
        var qrCodeUrl = "https://qr.mercadopago.com/test";
        var orderSnapshot = "{\"code\":\"ORD-123\",\"orderedProducts\":[{\"name\":\"Product\",\"description\":\"Desc\",\"unitPrice\":10.50,\"quantity\":2,\"unitMeasure\":\"unit\"}]}";
        
        var payment = new Payment(orderId, 100.00m, orderSnapshot);
        var input = new GenerateQrCodeInputModel
        {
            OrderId = orderId,
            FakeCheckout = false
        };

        _paymentRepositoryMock
            .Setup(r => r.GetByOrderIdAsync(orderId))
            .ReturnsAsync(payment);

        _realPaymentGatewayMock
            .Setup(g => g.GenerateQrCodeAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<QrCodeItemModel>>()))
            .ReturnsAsync(qrCodeUrl);

        _paymentRepositoryMock
            .Setup(r => r.UpdateAsync(It.IsAny<Payment>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _useCase.ExecuteAsync(input);

        // Assert
        result.Should().NotBeNull();
        result.QrCodeUrl.Should().Be(qrCodeUrl);
        result.PaymentId.Should().Be(payment.Id);
        result.OrderId.Should().Be(orderId);
        _paymentRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Payment>()), Times.Once);
        _realPaymentGatewayMock.Verify(g => g.GenerateQrCodeAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<QrCodeItemModel>>()), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_WhenFakeCheckout_ShouldUseFakeGateway()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var qrCodeUrl = "https://fake.qr.com/test";
        var orderSnapshot = "{\"code\":\"ORD-123\",\"orderedProducts\":[]}";
        
        var payment = new Payment(orderId, 100.00m, orderSnapshot);
        var input = new GenerateQrCodeInputModel
        {
            OrderId = orderId,
            FakeCheckout = true
        };

        _paymentRepositoryMock
            .Setup(r => r.GetByOrderIdAsync(orderId))
            .ReturnsAsync(payment);

        _fakePaymentGatewayMock
            .Setup(g => g.GenerateQrCodeAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<QrCodeItemModel>>()))
            .ReturnsAsync(qrCodeUrl);

        _paymentRepositoryMock
            .Setup(r => r.UpdateAsync(It.IsAny<Payment>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _useCase.ExecuteAsync(input);

        // Assert
        result.Should().NotBeNull();
        result.QrCodeUrl.Should().Be(qrCodeUrl);
        _fakePaymentGatewayMock.Verify(g => g.GenerateQrCodeAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<QrCodeItemModel>>()), Times.Once);
        _realPaymentGatewayMock.Verify(g => g.GenerateQrCodeAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<QrCodeItemModel>>()), Times.Never);
    }

    [Fact]
    public async Task ExecuteAsync_WhenSuccess_ShouldUpdatePaymentStatus()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var qrCodeUrl = "https://qr.mercadopago.com/test";
        var orderSnapshot = "{\"code\":\"ORD-123\",\"orderedProducts\":[]}";
        
        var payment = new Payment(orderId, 100.00m, orderSnapshot);
        var input = new GenerateQrCodeInputModel
        {
            OrderId = orderId,
            FakeCheckout = false
        };

        _paymentRepositoryMock
            .Setup(r => r.GetByOrderIdAsync(orderId))
            .ReturnsAsync(payment);

        _realPaymentGatewayMock
            .Setup(g => g.GenerateQrCodeAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<QrCodeItemModel>>()))
            .ReturnsAsync(qrCodeUrl);

        Payment? updatedPayment = null;
        _paymentRepositoryMock
            .Setup(r => r.UpdateAsync(It.IsAny<Payment>()))
            .Callback<Payment>(p => updatedPayment = p)
            .Returns(Task.CompletedTask);

        // Act
        await _useCase.ExecuteAsync(input);

        // Assert
        updatedPayment.Should().NotBeNull();
        updatedPayment!.Status.Should().Be(EnumPaymentStatus.QrCodeGenerated);
        updatedPayment.QrCodeUrl.Should().Be(qrCodeUrl);
    }
}
