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

/// <summary>
/// Testes adicionais para GenerateQrCodeUseCase cobrindo edge cases
/// </summary>
public class GenerateQrCodeUseCaseAdditionalTests
{
    private readonly Mock<IPaymentRepository> _paymentRepositoryMock;
    private readonly Mock<IPaymentGateway> _realPaymentGatewayMock;
    private readonly Mock<IPaymentGateway> _fakePaymentGatewayMock;
    private readonly GenerateQrCodePresenter _presenter;
    private readonly GenerateQrCodeUseCase _useCase;

    public GenerateQrCodeUseCaseAdditionalTests()
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
    public async Task ExecuteAsync_WhenOrderSnapshotHasNullProducts_ShouldHandleGracefully()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var orderSnapshot = "{\"code\":\"ORD-123\",\"orderedProducts\":null}";
        var payment = new Payment(orderId, 100.00m, orderSnapshot);
        var qrCodeUrl = "https://qr.mercadopago.com/test";
        
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
        _realPaymentGatewayMock.Verify(g => g.GenerateQrCodeAsync(
            It.IsAny<string>(), 
            It.IsAny<string>(), 
            It.Is<List<QrCodeItemModel>>(items => items != null && items.Count == 0)), 
            Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_WhenOrderSnapshotHasEmptyProducts_ShouldHandleGracefully()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var orderSnapshot = "{\"code\":\"ORD-123\",\"orderedProducts\":[]}";
        var payment = new Payment(orderId, 100.00m, orderSnapshot);
        var qrCodeUrl = "https://qr.mercadopago.com/test";
        
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
    }

    [Fact]
    public async Task ExecuteAsync_WhenOrderSnapshotHasProductsWithNullFields_ShouldUseDefaults()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var orderSnapshot = "{\"code\":\"ORD-123\",\"orderedProducts\":[{\"name\":null,\"description\":null,\"unitPrice\":10.50,\"quantity\":2,\"unitMeasure\":null}]}";
        var payment = new Payment(orderId, 100.00m, orderSnapshot);
        var qrCodeUrl = "https://qr.mercadopago.com/test";
        
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
        _realPaymentGatewayMock.Verify(g => g.GenerateQrCodeAsync(
            It.IsAny<string>(), 
            It.IsAny<string>(), 
            It.Is<List<QrCodeItemModel>>(items => 
                items != null && 
                items.Count == 1 && 
                items[0].Title == string.Empty &&
                items[0].Description == string.Empty &&
                items[0].UnitMeasure == "unit")), 
            Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_WhenOrderSnapshotHasNoCode_ShouldUseEmptyString()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var orderSnapshot = "{\"orderedProducts\":[]}";
        var payment = new Payment(orderId, 100.00m, orderSnapshot);
        var qrCodeUrl = "https://qr.mercadopago.com/test";
        
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
        _realPaymentGatewayMock.Verify(g => g.GenerateQrCodeAsync(
            It.IsAny<string>(), 
            string.Empty, 
            It.IsAny<List<QrCodeItemModel>>()), 
            Times.Once);
    }
}
