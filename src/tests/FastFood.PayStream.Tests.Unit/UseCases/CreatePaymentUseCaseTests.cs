using Moq;
using FastFood.PayStream.Application.InputModels;
using FastFood.PayStream.Application.UseCases;
using FastFood.PayStream.Application.Ports;
using FastFood.PayStream.Application.Presenters;
using FastFood.PayStream.Domain.Entities;
using FastFood.PayStream.Domain.Common.Enums;

namespace FastFood.PayStream.Tests.Unit.UseCases;

public class CreatePaymentUseCaseTests
{
    private readonly Mock<IPaymentRepository> _paymentRepositoryMock;
    private readonly CreatePaymentPresenter _presenter;
    private readonly CreatePaymentUseCase _useCase;

    public CreatePaymentUseCaseTests()
    {
        _paymentRepositoryMock = new Mock<IPaymentRepository>();
        _presenter = new CreatePaymentPresenter();
        _useCase = new CreatePaymentUseCase(_paymentRepositoryMock.Object, _presenter);
    }

    [Fact]
    public async Task ExecuteAsync_WhenOrderIdIsEmpty_ShouldThrowArgumentException()
    {
        // Arrange
        var input = new CreatePaymentInputModel
        {
            OrderId = Guid.Empty,
            TotalAmount = 100.50m,
            OrderSnapshot = "{\"orderId\":\"123\"}"
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _useCase.ExecuteAsync(input));
        Assert.Contains("OrderId não pode ser vazio", exception.Message);
    }

    [Fact]
    public async Task ExecuteAsync_WhenTotalAmountIsZero_ShouldThrowArgumentException()
    {
        // Arrange
        var input = new CreatePaymentInputModel
        {
            OrderId = Guid.NewGuid(),
            TotalAmount = 0,
            OrderSnapshot = "{\"orderId\":\"123\"}"
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _useCase.ExecuteAsync(input));
        Assert.Contains("TotalAmount deve ser maior que zero", exception.Message);
    }

    [Fact]
    public async Task ExecuteAsync_WhenTotalAmountIsNegative_ShouldThrowArgumentException()
    {
        // Arrange
        var input = new CreatePaymentInputModel
        {
            OrderId = Guid.NewGuid(),
            TotalAmount = -10.50m,
            OrderSnapshot = "{\"orderId\":\"123\"}"
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _useCase.ExecuteAsync(input));
        Assert.Contains("TotalAmount deve ser maior que zero", exception.Message);
    }

    [Fact]
    public async Task ExecuteAsync_WhenOrderSnapshotIsNull_ShouldThrowArgumentException()
    {
        // Arrange
        var input = new CreatePaymentInputModel
        {
            OrderId = Guid.NewGuid(),
            TotalAmount = 100.50m,
            OrderSnapshot = null!
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _useCase.ExecuteAsync(input));
        Assert.Contains("OrderSnapshot não pode ser nulo ou vazio", exception.Message);
    }

    [Fact]
    public async Task ExecuteAsync_WhenOrderSnapshotIsEmpty_ShouldThrowArgumentException()
    {
        // Arrange
        var input = new CreatePaymentInputModel
        {
            OrderId = Guid.NewGuid(),
            TotalAmount = 100.50m,
            OrderSnapshot = string.Empty
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _useCase.ExecuteAsync(input));
        Assert.Contains("OrderSnapshot não pode ser nulo ou vazio", exception.Message);
    }

    [Fact]
    public async Task ExecuteAsync_WhenOrderSnapshotIsWhitespace_ShouldThrowArgumentException()
    {
        // Arrange
        var input = new CreatePaymentInputModel
        {
            OrderId = Guid.NewGuid(),
            TotalAmount = 100.50m,
            OrderSnapshot = "   "
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _useCase.ExecuteAsync(input));
        Assert.Contains("OrderSnapshot não pode ser nulo ou vazio", exception.Message);
    }

    [Fact]
    public async Task ExecuteAsync_WithValidInput_ShouldCreatePayment()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var totalAmount = 100.50m;
        var orderSnapshot = "{\"orderId\":\"123\",\"items\":[]}";
        var input = new CreatePaymentInputModel
        {
            OrderId = orderId,
            TotalAmount = totalAmount,
            OrderSnapshot = orderSnapshot
        };

        Payment? capturedPayment = null;
        _paymentRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Payment>()))
            .ReturnsAsync((Payment p) =>
            {
                capturedPayment = p;
                return p;
            });

        // Act
        var result = await _useCase.ExecuteAsync(input);

        // Assert
        Assert.NotNull(capturedPayment);
        Assert.Equal(orderId, capturedPayment.OrderId);
        Assert.Equal(totalAmount, capturedPayment.TotalAmount);
        Assert.Equal(orderSnapshot, capturedPayment.OrderSnapshot);
        Assert.Equal(EnumPaymentStatus.NotStarted, capturedPayment.Status);
        Assert.NotNull(result);
        Assert.Equal(capturedPayment.Id, result.PaymentId);
        Assert.Equal(capturedPayment.OrderId, result.OrderId);
        Assert.Equal((int)capturedPayment.Status, result.Status);
        Assert.Equal(capturedPayment.TotalAmount, result.TotalAmount);
        Assert.Equal(capturedPayment.CreatedAt, result.CreatedAt);
    }

    [Fact]
    public async Task ExecuteAsync_WithValidInput_ShouldCallRepositoryAddAsync()
    {
        // Arrange
        var input = new CreatePaymentInputModel
        {
            OrderId = Guid.NewGuid(),
            TotalAmount = 100.50m,
            OrderSnapshot = "{\"orderId\":\"123\"}"
        };

        _paymentRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Payment>()))
            .ReturnsAsync((Payment p) => p);

        // Act
        await _useCase.ExecuteAsync(input);

        // Assert
        _paymentRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Payment>()), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_WithValidInput_ShouldReturnResponseWithCorrectData()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var totalAmount = 100.50m;
        var orderSnapshot = "{\"orderId\":\"123\"}";
        var input = new CreatePaymentInputModel
        {
            OrderId = orderId,
            TotalAmount = totalAmount,
            OrderSnapshot = orderSnapshot
        };

        Payment? savedPayment = null;
        _paymentRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Payment>()))
            .ReturnsAsync((Payment p) =>
            {
                savedPayment = p;
                return p;
            });

        // Act
        var result = await _useCase.ExecuteAsync(input);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(savedPayment);
        Assert.Equal(savedPayment.Id, result.PaymentId);
        Assert.Equal(savedPayment.OrderId, result.OrderId);
        Assert.Equal((int)savedPayment.Status, result.Status);
        Assert.Equal(savedPayment.TotalAmount, result.TotalAmount);
        Assert.Equal(savedPayment.CreatedAt, result.CreatedAt);
    }

    [Fact]
    public async Task ExecuteAsync_WithValidInput_ShouldSetPaymentStatusToNotStarted()
    {
        // Arrange
        var input = new CreatePaymentInputModel
        {
            OrderId = Guid.NewGuid(),
            TotalAmount = 100.50m,
            OrderSnapshot = "{\"orderId\":\"123\"}"
        };

        Payment? capturedPayment = null;
        _paymentRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Payment>()))
            .ReturnsAsync((Payment p) =>
            {
                capturedPayment = p;
                return p;
            });

        // Act
        await _useCase.ExecuteAsync(input);

        // Assert
        Assert.NotNull(capturedPayment);
        Assert.Equal(EnumPaymentStatus.NotStarted, capturedPayment.Status);
    }
}
