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

public class PaymentNotificationUseCaseTests
{
    private readonly Mock<IPaymentRepository> _paymentRepositoryMock;
    private readonly Mock<IPaymentGateway> _realPaymentGatewayMock;
    private readonly Mock<IPaymentGateway> _fakePaymentGatewayMock;
    private readonly PaymentNotificationPresenter _presenter;
    private readonly PaymentNotificationUseCase _useCase;

    public PaymentNotificationUseCaseTests()
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
    public async Task ExecuteAsync_WhenOrderIdIsEmpty_ShouldThrowArgumentException()
    {
        // Arrange
        var input = new PaymentNotificationInputModel
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
        var input = new PaymentNotificationInputModel
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
    public async Task ExecuteAsync_WhenPaymentIsApproved_ShouldUpdatePaymentStatus()
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
        updatedPayment!.Status.Should().Be(EnumPaymentStatus.Approved);
        updatedPayment.ExternalTransactionId.Should().Be(transactionId);
        result.Status.Should().Be((int)EnumPaymentStatus.Approved);
        _paymentRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Payment>()), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_WhenPaymentIsRejected_ShouldUpdatePaymentStatus()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var payment = new Payment(orderId, 100.00m, "{}");
        
        var statusResult = new PaymentStatusResult
        {
            IsApproved = false,
            IsRejected = true,
            IsCanceled = false,
            IsPending = false,
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
        updatedPayment!.Status.Should().Be(EnumPaymentStatus.Rejected);
        result.Status.Should().Be((int)EnumPaymentStatus.Rejected);
    }

    [Fact]
    public async Task ExecuteAsync_WhenPaymentIsCanceled_ShouldUpdatePaymentStatus()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var payment = new Payment(orderId, 100.00m, "{}");
        
        var statusResult = new PaymentStatusResult
        {
            IsApproved = false,
            IsRejected = false,
            IsCanceled = true,
            IsPending = false,
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
        updatedPayment!.Status.Should().Be(EnumPaymentStatus.Canceled);
        result.Status.Should().Be((int)EnumPaymentStatus.Canceled);
    }

    [Fact]
    public async Task ExecuteAsync_WhenFakeCheckout_ShouldUseFakeGateway()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var payment = new Payment(orderId, 100.00m, "{}");
        
        var statusResult = new PaymentStatusResult
        {
            IsApproved = true,
            IsRejected = false,
            IsCanceled = false,
            IsPending = false,
            TransactionId = "TRX123456"
        };

        var input = new PaymentNotificationInputModel
        {
            OrderId = orderId,
            FakeCheckout = true
        };

        _paymentRepositoryMock
            .Setup(r => r.GetByOrderIdAsync(orderId))
            .ReturnsAsync(payment);

        _fakePaymentGatewayMock
            .Setup(g => g.CheckPaymentStatusAsync(payment.Id.ToString()))
            .ReturnsAsync(statusResult);

        _paymentRepositoryMock
            .Setup(r => r.UpdateAsync(It.IsAny<Payment>()))
            .Returns(Task.CompletedTask);

        // Act
        await _useCase.ExecuteAsync(input);

        // Assert
        _fakePaymentGatewayMock.Verify(g => g.CheckPaymentStatusAsync(payment.Id.ToString()), Times.Once);
        _realPaymentGatewayMock.Verify(g => g.CheckPaymentStatusAsync(It.IsAny<string>()), Times.Never);
    }
}
