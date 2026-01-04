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

public class WebhookPaymentControllerTests
{
    private readonly Mock<IPaymentRepository> _paymentRepositoryMock;
    private readonly Mock<IPaymentGateway> _realGatewayMock;
    private readonly Mock<IPaymentGateway> _fakeGatewayMock;
    private readonly PaymentNotificationUseCase _paymentNotificationUseCase;
    private readonly WebhookPaymentController _controller;

    public WebhookPaymentControllerTests()
    {
        _paymentRepositoryMock = new Mock<IPaymentRepository>();
        _realGatewayMock = new Mock<IPaymentGateway>();
        _fakeGatewayMock = new Mock<IPaymentGateway>();
        
        _paymentNotificationUseCase = new PaymentNotificationUseCase(
            _paymentRepositoryMock.Object,
            _realGatewayMock.Object,
            _fakeGatewayMock.Object,
            new PaymentNotificationPresenter());
        
        _controller = new WebhookPaymentController(_paymentNotificationUseCase);
    }

    [Fact]
    public async Task ProcessWebhook_WhenValidNotification_ShouldReturn200Ok()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var payment = new Payment(orderId, 100.00m, "{}");
        var transactionId = "TRX123456";
        
        var statusResult = new PaymentStatusResult
        {
            IsApproved = true,
            IsRejected = false,
            IsCanceled = false,
            IsPending = false,
            TransactionId = transactionId
        };

        _paymentRepositoryMock
            .Setup(r => r.GetByOrderIdAsync(orderId))
            .ReturnsAsync(payment);

        _realGatewayMock
            .Setup(g => g.CheckPaymentStatusAsync(payment.Id.ToString()))
            .ReturnsAsync(statusResult);

        _paymentRepositoryMock
            .Setup(r => r.UpdateAsync(It.IsAny<Payment>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.PaymentNotification(orderId, false);

        // Assert
        result.Should().NotBeNull();
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult!.StatusCode.Should().Be(200);
        okResult.Value.Should().BeOfType<ApiResponse<PaymentNotificationResponse>>();
        
        var apiResponse = okResult.Value as ApiResponse<PaymentNotificationResponse>;
        apiResponse.Should().NotBeNull();
        apiResponse!.Success.Should().BeTrue();
        apiResponse.Content.Should().NotBeNull();
    }

    [Fact]
    public async Task ProcessWebhook_WhenInvalidPayload_ShouldReturn400BadRequest()
    {
        // Arrange
        var orderId = Guid.Empty;

        // Act
        var result = await _controller.PaymentNotification(orderId, false);

        // Assert
        result.Should().NotBeNull();
        var badRequestResult = result as BadRequestObjectResult;
        badRequestResult.Should().NotBeNull();
        badRequestResult!.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task ProcessWebhook_WhenPaymentNotFound_ShouldReturn404NotFound()
    {
        // Arrange
        var orderId = Guid.NewGuid();

        _paymentRepositoryMock
            .Setup(r => r.GetByOrderIdAsync(orderId))
            .ReturnsAsync((Payment?)null);

        // Act
        var result = await _controller.PaymentNotification(orderId, false);

        // Assert
        result.Should().NotBeNull();
        var notFoundResult = result as NotFoundObjectResult;
        notFoundResult.Should().NotBeNull();
        notFoundResult!.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task ProcessWebhook_WhenFakeCheckoutIsTrue_ShouldUseFakeGateway()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var payment = new Payment(orderId, 100.00m, "{}");
        var transactionId = "TRX123456";
        
        var statusResult = new PaymentStatusResult
        {
            IsApproved = true,
            IsRejected = false,
            IsCanceled = false,
            IsPending = false,
            TransactionId = transactionId
        };

        _paymentRepositoryMock
            .Setup(r => r.GetByOrderIdAsync(orderId))
            .ReturnsAsync(payment);

        _fakeGatewayMock
            .Setup(g => g.CheckPaymentStatusAsync(payment.Id.ToString()))
            .ReturnsAsync(statusResult);

        _paymentRepositoryMock
            .Setup(r => r.UpdateAsync(It.IsAny<Payment>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.PaymentNotification(orderId, true);

        // Assert
        result.Should().NotBeNull();
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult!.StatusCode.Should().Be(200);
        _fakeGatewayMock.Verify(g => g.CheckPaymentStatusAsync(payment.Id.ToString()), Times.Once);
        _realGatewayMock.Verify(g => g.CheckPaymentStatusAsync(It.IsAny<string>()), Times.Never);
    }
}
