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
using FastFood.PayStream.Domain.Common.Enums;

namespace FastFood.PayStream.Tests.Unit.InterfacesExternas.Controllers;

public class PaymentControllerTests
{
    private readonly Mock<IPaymentRepository> _paymentRepositoryMock;
    private readonly Mock<IPaymentGateway> _realGatewayMock;
    private readonly Mock<IPaymentGateway> _fakeGatewayMock;
    private readonly CreatePaymentUseCase _createPaymentUseCase;
    private readonly GenerateQrCodeUseCase _generateQrCodeUseCase;
    private readonly GetReceiptUseCase _getReceiptUseCase;
    private readonly PaymentController _controller;

    public PaymentControllerTests()
    {
        _paymentRepositoryMock = new Mock<IPaymentRepository>();
        _realGatewayMock = new Mock<IPaymentGateway>();
        _fakeGatewayMock = new Mock<IPaymentGateway>();
        
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
            new GetReceiptPresenter());
        
        _controller = new PaymentController(
            _createPaymentUseCase,
            _generateQrCodeUseCase,
            _getReceiptUseCase);
    }

    [Fact]
    public async Task CreatePayment_WhenValidRequest_ShouldReturn201Created()
    {
        // Arrange
        var input = new CreatePaymentInputModel
        {
            OrderId = Guid.NewGuid(),
            TotalAmount = 100.50m,
            OrderSnapshot = "{}"
        };

        _paymentRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Payment>()))
            .ReturnsAsync((Payment p) => p);

        // Act
        var result = await _controller.Create(input);

        // Assert
        result.Should().NotBeNull();
        var objectResult = result as ObjectResult;
        objectResult.Should().NotBeNull();
        objectResult!.StatusCode.Should().Be(201);
        objectResult.Value.Should().BeOfType<ApiResponse<CreatePaymentResponse>>();
        
        var apiResponse = objectResult.Value as ApiResponse<CreatePaymentResponse>;
        apiResponse.Should().NotBeNull();
        apiResponse!.Success.Should().BeTrue();
        apiResponse.Content.Should().NotBeNull();
    }

    [Fact]
    public async Task CreatePayment_WhenInvalidRequest_ShouldReturn400BadRequest()
    {
        // Arrange
        var input = new CreatePaymentInputModel
        {
            OrderId = Guid.Empty,
            TotalAmount = 100.50m,
            OrderSnapshot = "{}"
        };

        // Act
        var result = await _controller.Create(input);

        // Assert
        result.Should().NotBeNull();
        var badRequestResult = result as BadRequestObjectResult;
        badRequestResult.Should().NotBeNull();
        badRequestResult!.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task GenerateQrCode_WhenValidRequest_ShouldReturn200Ok()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var payment = new Payment(orderId, 100.00m, "{\"code\":\"ORD-123\",\"orderedProducts\":[]}");
        var qrCodeUrl = "https://qr.mercadopago.com/test";

        _paymentRepositoryMock
            .Setup(r => r.GetByOrderIdAsync(orderId))
            .ReturnsAsync(payment);

        _realGatewayMock
            .Setup(g => g.GenerateQrCodeAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<QrCodeItemModel>>()))
            .ReturnsAsync(qrCodeUrl);

        _paymentRepositoryMock
            .Setup(r => r.UpdateAsync(It.IsAny<Payment>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.GenerateQrCode(orderId, false);

        // Assert
        result.Should().NotBeNull();
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult!.StatusCode.Should().Be(200);
        okResult.Value.Should().BeOfType<ApiResponse<GenerateQrCodeResponse>>();
    }

    [Fact]
    public async Task GenerateQrCode_WhenPaymentNotFound_ShouldReturn404NotFound()
    {
        // Arrange
        var orderId = Guid.NewGuid();

        _paymentRepositoryMock
            .Setup(r => r.GetByOrderIdAsync(orderId))
            .ReturnsAsync((Payment?)null);

        // Act
        var result = await _controller.GenerateQrCode(orderId, false);

        // Assert
        result.Should().NotBeNull();
        var notFoundResult = result as NotFoundObjectResult;
        notFoundResult.Should().NotBeNull();
        notFoundResult!.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task GetReceipt_WhenValidRequest_ShouldReturn200Ok()
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

        _realGatewayMock
            .Setup(g => g.GetReceiptFromGatewayAsync("TRX123456"))
            .ReturnsAsync(receipt);

        // Act
        var result = await _controller.GetReceiptFromGateway(orderId, false);

        // Assert
        result.Should().NotBeNull();
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult!.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetReceipt_WhenPaymentNotFound_ShouldReturn404NotFound()
    {
        // Arrange
        var orderId = Guid.NewGuid();

        _paymentRepositoryMock
            .Setup(r => r.GetByOrderIdAsync(orderId))
            .ReturnsAsync((Payment?)null);

        // Act
        var result = await _controller.GetReceiptFromGateway(orderId, false);

        // Assert
        result.Should().NotBeNull();
        var notFoundResult = result as NotFoundObjectResult;
        notFoundResult.Should().NotBeNull();
        notFoundResult!.StatusCode.Should().Be(404);
    }
}
