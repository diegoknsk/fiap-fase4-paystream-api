using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using FastFood.PayStream.Api.Controllers;
using FastFood.PayStream.Application.UseCases;
using FastFood.PayStream.Application.InputModels;
using FastFood.PayStream.Application.Responses;
using FastFood.PayStream.Application.Models.Common;
using FastFood.PayStream.Application.Ports;
using FastFood.PayStream.Application.Ports.Parameters;
using FastFood.PayStream.Application.Presenters;
using FastFood.PayStream.Domain.Entities;

namespace FastFood.PayStream.Tests.Unit.InterfacesExternas.Controllers;

/// <summary>
/// Testes adicionais para PaymentController cobrindo edge cases e cenários de erro
/// </summary>
public class PaymentControllerAdditionalTests
{
    private readonly Mock<IPaymentRepository> _paymentRepositoryMock;
    private readonly Mock<IPaymentGateway> _realGatewayMock;
    private readonly Mock<IPaymentGateway> _fakeGatewayMock;
    private readonly Mock<IKitchenService> _kitchenServiceMock;
    private readonly CreatePaymentUseCase _createPaymentUseCase;
    private readonly GenerateQrCodeUseCase _generateQrCodeUseCase;
    private readonly GetReceiptUseCase _getReceiptUseCase;
    private readonly PaymentController _controller;

    public PaymentControllerAdditionalTests()
    {
        _paymentRepositoryMock = new Mock<IPaymentRepository>();
        _realGatewayMock = new Mock<IPaymentGateway>();
        _fakeGatewayMock = new Mock<IPaymentGateway>();
        _kitchenServiceMock = new Mock<IKitchenService>();
        _kitchenServiceMock
            .Setup(k => k.SendToPreparationAsync(It.IsAny<Guid>(), It.IsAny<string>()))
            .Returns(Task.CompletedTask);
        
        _createPaymentUseCase = new CreatePaymentUseCase(
            _paymentRepositoryMock.Object,
            new CreatePaymentPresenter());
        _generateQrCodeUseCase = new GenerateQrCodeUseCase(
            _paymentRepositoryMock.Object,
            _realGatewayMock.Object,
            _fakeGatewayMock.Object,
            new GenerateQrCodePresenter());
        _getReceiptUseCase = new GetReceiptUseCase(
            _paymentRepositoryMock.Object,
            _realGatewayMock.Object,
            _fakeGatewayMock.Object,
            _kitchenServiceMock.Object,
            new GetReceiptPresenter());
        
        _controller = new PaymentController(
            _createPaymentUseCase,
            _generateQrCodeUseCase,
            _getReceiptUseCase);
    }

    [Fact]
    public async Task GenerateQrCode_WhenOrderSnapshotIsInvalid_ShouldReturn404NotFound()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var payment = new Payment(orderId, 100.00m, "invalid json");

        _paymentRepositoryMock
            .Setup(r => r.GetByOrderIdAsync(orderId))
            .ReturnsAsync(payment);

        // Act
        var result = await _controller.GenerateQrCode(orderId, false);

        // Assert
        result.Should().NotBeNull();
        // O controller retorna NotFound para ApplicationException
        var notFoundResult = result as NotFoundObjectResult;
        notFoundResult.Should().NotBeNull();
        notFoundResult!.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task GenerateQrCode_WhenOrderSnapshotIsNull_ShouldReturn404NotFound()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var payment = new Payment(orderId, 100.00m, null!);

        _paymentRepositoryMock
            .Setup(r => r.GetByOrderIdAsync(orderId))
            .ReturnsAsync(payment);

        // Act
        var result = await _controller.GenerateQrCode(orderId, false);

        // Assert
        result.Should().NotBeNull();
        // O controller retorna NotFound para ApplicationException
        var notFoundResult = result as NotFoundObjectResult;
        notFoundResult.Should().NotBeNull();
        notFoundResult!.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task GetReceipt_WhenExternalTransactionIdIsEmpty_ShouldReturn400BadRequest()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var payment = new Payment(orderId, 100.00m, "{}");
        // Payment não tem ExternalTransactionId

        _paymentRepositoryMock
            .Setup(r => r.GetByOrderIdAsync(orderId))
            .ReturnsAsync(payment);

        // Act
        var result = await _controller.GetReceiptFromGateway(orderId, false);

        // Assert
        result.Should().NotBeNull();
        // ApplicationException sem "não encontrado" retorna BadRequest
        var badRequestResult = result as BadRequestObjectResult;
        badRequestResult.Should().NotBeNull();
        badRequestResult!.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task GetReceipt_WhenApplicationExceptionDoesNotContainNotFound_ShouldReturn400BadRequest()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var payment = new Payment(orderId, 100.00m, "{}");
        // Payment não tem ExternalTransactionId, então vai lançar ApplicationException
        // que não contém "não encontrado"

        _paymentRepositoryMock
            .Setup(r => r.GetByOrderIdAsync(orderId))
            .ReturnsAsync(payment);

        // Act
        var result = await _controller.GetReceiptFromGateway(orderId, false);

        // Assert
        result.Should().NotBeNull();
        var badRequestResult = result as BadRequestObjectResult;
        badRequestResult.Should().NotBeNull();
        badRequestResult!.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task GenerateQrCode_WhenFakeCheckoutIsTrue_ShouldUseFakeGateway()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var payment = new Payment(orderId, 100.00m, "{\"code\":\"ORD-123\",\"orderedProducts\":[]}");
        var qrCodeUrl = "https://fake.qr.com/test";

        _paymentRepositoryMock
            .Setup(r => r.GetByOrderIdAsync(orderId))
            .ReturnsAsync(payment);

        _fakeGatewayMock
            .Setup(g => g.GenerateQrCodeAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<QrCodeItemModel>>()))
            .ReturnsAsync(qrCodeUrl);

        _paymentRepositoryMock
            .Setup(r => r.UpdateAsync(It.IsAny<Payment>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.GenerateQrCode(orderId, true);

        // Assert
        result.Should().NotBeNull();
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        _fakeGatewayMock.Verify(g => g.GenerateQrCodeAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<QrCodeItemModel>>()), Times.Once);
        _realGatewayMock.Verify(g => g.GenerateQrCodeAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<QrCodeItemModel>>()), Times.Never);
    }

    [Fact]
    public async Task GetReceipt_WhenFakeCheckoutIsTrue_ShouldUseFakeGateway()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var payment = new Payment(orderId, 100.00m, "{}");
        payment.Approve("TRX123456");
        
        var receipt = new PaymentReceipt
        {
            PaymentId = payment.Id.ToString(),
            ExternalReference = "EXT-123",
            Status = "approved",
            StatusDetail = "accredited",
            TotalPaidAmount = 100.00m,
            PaymentMethod = "pix",
            PaymentType = "bank_transfer",
            Currency = "BRL",
            DateApproved = DateTime.UtcNow
        };

        _paymentRepositoryMock
            .Setup(r => r.GetByOrderIdAsync(orderId))
            .ReturnsAsync(payment);

        _fakeGatewayMock
            .Setup(g => g.GetReceiptFromGatewayAsync("TRX123456"))
            .ReturnsAsync(receipt);

        // Act
        var result = await _controller.GetReceiptFromGateway(orderId, true);

        // Assert
        result.Should().NotBeNull();
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        _fakeGatewayMock.Verify(g => g.GetReceiptFromGatewayAsync("TRX123456"), Times.Once);
        _realGatewayMock.Verify(g => g.GetReceiptFromGatewayAsync(It.IsAny<string>()), Times.Never);
    }
}
