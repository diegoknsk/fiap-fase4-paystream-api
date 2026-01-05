using Moq;
using FluentAssertions;
using FastFood.PayStream.Application.InputModels;
using FastFood.PayStream.Application.UseCases;
using FastFood.PayStream.Application.Ports;
using FastFood.PayStream.Application.Ports.Parameters;
using FastFood.PayStream.Application.Presenters;
using FastFood.PayStream.Domain.Entities;

namespace FastFood.PayStream.Tests.Unit.Application.UseCases;

public class GetReceiptUseCaseTests
{
    private readonly Mock<IPaymentRepository> _paymentRepositoryMock;
    private readonly Mock<IPaymentGateway> _realPaymentGatewayMock;
    private readonly Mock<IPaymentGateway> _fakePaymentGatewayMock;
    private readonly Mock<IKitchenService> _kitchenServiceMock;
    private readonly GetReceiptPresenter _presenter;
    private readonly GetReceiptUseCase _useCase;

    public GetReceiptUseCaseTests()
    {
        _paymentRepositoryMock = new Mock<IPaymentRepository>();
        _realPaymentGatewayMock = new Mock<IPaymentGateway>();
        _fakePaymentGatewayMock = new Mock<IPaymentGateway>();
        _kitchenServiceMock = new Mock<IKitchenService>();
        _kitchenServiceMock
            .Setup(k => k.SendToPreparationAsync(It.IsAny<Guid>(), It.IsAny<string>()))
            .Returns(Task.CompletedTask);
        _presenter = new GetReceiptPresenter();
        _useCase = new GetReceiptUseCase(
            _paymentRepositoryMock.Object,
            _realPaymentGatewayMock.Object,
            _fakePaymentGatewayMock.Object,
            _kitchenServiceMock.Object,
            _presenter);
    }

    [Fact]
    public async Task ExecuteAsync_WhenOrderIdIsEmpty_ShouldThrowArgumentException()
    {
        // Arrange
        var input = new GetReceiptInputModel
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
        var input = new GetReceiptInputModel
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
    public async Task ExecuteAsync_WhenNoExternalTransactionId_ShouldThrowApplicationException()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var payment = new Payment(orderId, 100.00m, "{}");
        var input = new GetReceiptInputModel
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
            .WithMessage($"Pagamento {payment.Id} não possui ExternalTransactionId.*");
    }

    [Fact]
    public async Task ExecuteAsync_WhenValidInput_ShouldReturnReceipt()
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

        var input = new GetReceiptInputModel
        {
            OrderId = orderId,
            FakeCheckout = false
        };

        _paymentRepositoryMock
            .Setup(r => r.GetByOrderIdAsync(orderId))
            .ReturnsAsync(payment);

        _realPaymentGatewayMock
            .Setup(g => g.GetReceiptFromGatewayAsync("TRX123456"))
            .ReturnsAsync(receipt);

        // Act
        var result = await _useCase.ExecuteAsync(input);

        // Assert
        result.Should().NotBeNull();
        result.PaymentId.Should().Be(payment.Id.ToString());
        result.ExternalReference.Should().Be(receipt.ExternalReference);
        result.Status.Should().Be(receipt.Status);
        result.TotalPaidAmount.Should().Be(receipt.TotalPaidAmount);
        _realPaymentGatewayMock.Verify(g => g.GetReceiptFromGatewayAsync("TRX123456"), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_WhenFakeCheckout_ShouldUseFakeGateway()
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

        var input = new GetReceiptInputModel
        {
            OrderId = orderId,
            FakeCheckout = true
        };

        _paymentRepositoryMock
            .Setup(r => r.GetByOrderIdAsync(orderId))
            .ReturnsAsync(payment);

        _fakePaymentGatewayMock
            .Setup(g => g.GetReceiptFromGatewayAsync("TRX123456"))
            .ReturnsAsync(receipt);

        // Act
        var result = await _useCase.ExecuteAsync(input);

        // Assert
        result.Should().NotBeNull();
        _fakePaymentGatewayMock.Verify(g => g.GetReceiptFromGatewayAsync("TRX123456"), Times.Once);
        _realPaymentGatewayMock.Verify(g => g.GetReceiptFromGatewayAsync(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task ExecuteAsync_WhenFakeCheckout_ShouldUsePaymentTotalAmountInsteadOfFakeReceiptAmount()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var paymentTotalAmount = 250.75m; // Valor real do pedido
        var fakeReceiptAmount = 100.00m; // Valor fake retornado pelo gateway
        var payment = new Payment(orderId, paymentTotalAmount, "{}");
        payment.Approve("TRX123456");
        
        var receipt = new PaymentReceipt
        {
            PaymentId = payment.Id.ToString(),
            ExternalReference = "EXT-123",
            Status = "approved",
            StatusDetail = "Pagamento aprovado (fake)",
            TotalPaidAmount = fakeReceiptAmount, // Gateway fake retorna valor fixo
            PaymentMethod = "pix",
            PaymentType = "pix",
            Currency = "BRL",
            DateApproved = DateTime.UtcNow
        };

        var input = new GetReceiptInputModel
        {
            OrderId = orderId,
            FakeCheckout = true
        };

        _paymentRepositoryMock
            .Setup(r => r.GetByOrderIdAsync(orderId))
            .ReturnsAsync(payment);

        _fakePaymentGatewayMock
            .Setup(g => g.GetReceiptFromGatewayAsync("TRX123456"))
            .ReturnsAsync(receipt);

        // Act
        var result = await _useCase.ExecuteAsync(input);

        // Assert
        result.Should().NotBeNull();
        // O valor deve ser o do pedido, não o do recibo fake
        result.TotalPaidAmount.Should().Be(paymentTotalAmount);
        result.TotalPaidAmount.Should().NotBe(fakeReceiptAmount);
        _fakePaymentGatewayMock.Verify(g => g.GetReceiptFromGatewayAsync("TRX123456"), Times.Once);
    }
}
