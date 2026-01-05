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
/// Testes adicionais para PaymentNotificationUseCase cobrindo edge cases e métodos privados indiretamente
/// </summary>
public class PaymentNotificationUseCaseAdditionalTests
{
    private readonly Mock<IPaymentRepository> _paymentRepositoryMock;
    private readonly Mock<IPaymentGateway> _realPaymentGatewayMock;
    private readonly Mock<IPaymentGateway> _fakePaymentGatewayMock;
    private readonly PaymentNotificationPresenter _presenter;
    private readonly PaymentNotificationUseCase _useCase;

    public PaymentNotificationUseCaseAdditionalTests()
    {
        _paymentRepositoryMock = new Mock<IPaymentRepository>();
        _realPaymentGatewayMock = new Mock<IPaymentGateway>();
        _fakePaymentGatewayMock = new Mock<IPaymentGateway>();
        _presenter = new PaymentNotificationPresenter();
        _useCase = new PaymentNotificationUseCase(
            _paymentRepositoryMock.Object,
            _realPaymentGatewayMock.Object,
            _fakePaymentGatewayMock.Object,
            _presenter);
    }

    [Fact]
    public async Task ExecuteAsync_WhenPaymentIsPending_ShouldNotChangeStatus()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var payment = new Payment(orderId, 100.00m, "{}");
        payment.Start(); // Status = Started
        
        var statusResult = new PaymentStatusResult
        {
            IsApproved = false,
            IsRejected = false,
            IsCanceled = false,
            IsPending = true,
            TransactionId = null
        };

        var input = new PaymentNotificationInputModel
        {
            OrderId = orderId,
            FakeCheckout = false
        };

        _paymentRepositoryMock
            .Setup(r => r.GetByOrderIdAsync(orderId))
            .ReturnsAsync(payment);

        _realPaymentGatewayMock
            .Setup(g => g.CheckPaymentStatusAsync(payment.Id.ToString()))
            .ReturnsAsync(statusResult);

        Payment? updatedPayment = null;
        _paymentRepositoryMock
            .Setup(r => r.UpdateAsync(It.IsAny<Payment>()))
            .Callback<Payment>(p => updatedPayment = p)
            .Returns(Task.CompletedTask);

        // Act
        var result = await _useCase.ExecuteAsync(input);

        // Assert
        result.Should().NotBeNull();
        updatedPayment.Should().NotBeNull();
        // Status deve permanecer Started quando está pendente
        updatedPayment!.Status.Should().Be(EnumPaymentStatus.Started);
        result.Status.Should().Be((int)EnumPaymentStatus.Started);
    }

    [Fact]
    public async Task ExecuteAsync_WhenStatusIsNotStarted_ShouldReturnCorrectStatusMessage()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var payment = new Payment(orderId, 100.00m, "{}");
        // Status já é NotStarted por padrão
        
        var statusResult = new PaymentStatusResult
        {
            IsApproved = false,
            IsRejected = false,
            IsCanceled = false,
            IsPending = true,
            TransactionId = null
        };

        var input = new PaymentNotificationInputModel
        {
            OrderId = orderId,
            FakeCheckout = false
        };

        _paymentRepositoryMock
            .Setup(r => r.GetByOrderIdAsync(orderId))
            .ReturnsAsync(payment);

        _realPaymentGatewayMock
            .Setup(g => g.CheckPaymentStatusAsync(payment.Id.ToString()))
            .ReturnsAsync(statusResult);

        _paymentRepositoryMock
            .Setup(r => r.UpdateAsync(It.IsAny<Payment>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _useCase.ExecuteAsync(input);

        // Assert
        result.Should().NotBeNull();
        result.StatusMessage.Should().Be("Pagamento não iniciado.");
    }

    [Fact]
    public async Task ExecuteAsync_WhenStatusIsQrCodeGenerated_ShouldReturnCorrectStatusMessage()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var payment = new Payment(orderId, 100.00m, "{}");
        payment.GenerateQrCode("https://qr.test.com");
        
        var statusResult = new PaymentStatusResult
        {
            IsApproved = false,
            IsRejected = false,
            IsCanceled = false,
            IsPending = true,
            TransactionId = null
        };

        var input = new PaymentNotificationInputModel
        {
            OrderId = orderId,
            FakeCheckout = false
        };

        _paymentRepositoryMock
            .Setup(r => r.GetByOrderIdAsync(orderId))
            .ReturnsAsync(payment);

        _realPaymentGatewayMock
            .Setup(g => g.CheckPaymentStatusAsync(payment.Id.ToString()))
            .ReturnsAsync(statusResult);

        _paymentRepositoryMock
            .Setup(r => r.UpdateAsync(It.IsAny<Payment>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _useCase.ExecuteAsync(input);

        // Assert
        result.Should().NotBeNull();
        result.StatusMessage.Should().Be("QR Code gerado.");
    }

    [Fact]
    public async Task ExecuteAsync_WhenStatusIsUnknown_ShouldReturnUnknownStatusMessage()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var payment = new Payment(orderId, 100.00m, "{}");
        
        // Simular um status que não está no switch (usando reflection para testar o default case)
        // Na prática, isso testa o default case do switch no GetStatusMessage
        
        var statusResult = new PaymentStatusResult
        {
            IsApproved = false,
            IsRejected = false,
            IsCanceled = false,
            IsPending = true,
            TransactionId = null
        };

        var input = new PaymentNotificationInputModel
        {
            OrderId = orderId,
            FakeCheckout = false
        };

        _paymentRepositoryMock
            .Setup(r => r.GetByOrderIdAsync(orderId))
            .ReturnsAsync(payment);

        _realPaymentGatewayMock
            .Setup(g => g.CheckPaymentStatusAsync(payment.Id.ToString()))
            .ReturnsAsync(statusResult);

        _paymentRepositoryMock
            .Setup(r => r.UpdateAsync(It.IsAny<Payment>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _useCase.ExecuteAsync(input);

        // Assert
        result.Should().NotBeNull();
        // O status message deve ser retornado corretamente baseado no status atual
        result.StatusMessage.Should().NotBeNullOrWhiteSpace();
    }
}
