using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using FastFood.PayStream.Infra.Persistence;
using FastFood.PayStream.Infra.Persistence.Repositories;
using FastFood.PayStream.Infra.Persistence.Entities;
using FastFood.PayStream.Domain.Entities;
using FastFood.PayStream.Domain.Common.Enums;

namespace FastFood.PayStream.Tests.Unit.Infra.Persistence.Repositories;

public class PaymentRepositoryTests
{
    private PayStreamDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<PayStreamDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new PayStreamDbContext(options);
    }

    [Fact]
    public async Task GetByIdAsync_WhenPaymentExists_ShouldReturnPayment()
    {
        // Arrange
        using var context = CreateContext();
        var repository = new PaymentRepository(context);
        
        var paymentId = Guid.NewGuid();
        var orderId = Guid.NewGuid();
        var entity = new PaymentEntity
        {
            Id = paymentId,
            OrderId = orderId,
            Status = (int)EnumPaymentStatus.NotStarted,
            TotalAmount = 100.50m,
            CreatedAt = DateTime.UtcNow,
            OrderSnapshot = "{\"orderId\":\"123\"}"
        };
        
        context.Payments.Add(entity);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetByIdAsync(paymentId);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(paymentId);
        result.OrderId.Should().Be(orderId);
        result.Status.Should().Be(EnumPaymentStatus.NotStarted);
        result.TotalAmount.Should().Be(100.50m);
    }

    [Fact]
    public async Task GetByIdAsync_WhenPaymentDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        using var context = CreateContext();
        var repository = new PaymentRepository(context);

        // Act
        var result = await repository.GetByIdAsync(Guid.NewGuid());

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetByOrderIdAsync_WhenPaymentExists_ShouldReturnPayment()
    {
        // Arrange
        using var context = CreateContext();
        var repository = new PaymentRepository(context);
        
        var paymentId = Guid.NewGuid();
        var orderId = Guid.NewGuid();
        var entity = new PaymentEntity
        {
            Id = paymentId,
            OrderId = orderId,
            Status = (int)EnumPaymentStatus.NotStarted,
            TotalAmount = 100.50m,
            CreatedAt = DateTime.UtcNow,
            OrderSnapshot = "{\"orderId\":\"123\"}"
        };
        
        context.Payments.Add(entity);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetByOrderIdAsync(orderId);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(paymentId);
        result.OrderId.Should().Be(orderId);
    }

    [Fact]
    public async Task GetByOrderIdAsync_WhenPaymentDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        using var context = CreateContext();
        var repository = new PaymentRepository(context);

        // Act
        var result = await repository.GetByOrderIdAsync(Guid.NewGuid());

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task AddAsync_ShouldSavePaymentToDatabase()
    {
        // Arrange
        using var context = CreateContext();
        var repository = new PaymentRepository(context);
        
        var orderId = Guid.NewGuid();
        var payment = new Payment(orderId, 100.50m, "{\"orderId\":\"123\"}");

        // Act
        var result = await repository.AddAsync(payment);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();
        result.OrderId.Should().Be(orderId);
        
        var savedEntity = await context.Payments.FindAsync(result.Id);
        savedEntity.Should().NotBeNull();
        savedEntity!.OrderId.Should().Be(orderId);
    }

    [Fact]
    public async Task UpdateAsync_WhenPaymentExists_ShouldUpdatePayment()
    {
        // Arrange
        using var context = CreateContext();
        var repository = new PaymentRepository(context);
        
        var paymentId = Guid.NewGuid();
        var orderId = Guid.NewGuid();
        var entity = new PaymentEntity
        {
            Id = paymentId,
            OrderId = orderId,
            Status = (int)EnumPaymentStatus.NotStarted,
            TotalAmount = 100.50m,
            CreatedAt = DateTime.UtcNow,
            OrderSnapshot = "{\"orderId\":\"123\"}"
        };
        
        context.Payments.Add(entity);
        await context.SaveChangesAsync();

        var payment = new Payment(orderId, 200.75m, "{\"orderId\":\"456\"}");
        // Usar reflexão para definir o Id
        var idProperty = typeof(Payment).GetProperty("Id", 
            System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
        idProperty?.SetValue(payment, paymentId);
        
        payment.GenerateQrCode("https://qrcode.example.com");
        payment.Approve("EXT-123");

        // Act
        await repository.UpdateAsync(payment);

        // Assert
        var updatedEntity = await context.Payments.FindAsync(paymentId);
        updatedEntity.Should().NotBeNull();
        updatedEntity!.Status.Should().Be((int)EnumPaymentStatus.Approved);
        updatedEntity.ExternalTransactionId.Should().Be("EXT-123");
        updatedEntity.QrCodeUrl.Should().Be("https://qrcode.example.com");
    }

    [Fact]
    public async Task UpdateAsync_WhenPaymentDoesNotExist_ShouldThrowInvalidOperationException()
    {
        // Arrange
        using var context = CreateContext();
        var repository = new PaymentRepository(context);
        
        var payment = new Payment(Guid.NewGuid(), 100.50m, "{\"orderId\":\"123\"}");
        var idProperty = typeof(Payment).GetProperty("Id", 
            System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
        idProperty?.SetValue(payment, Guid.NewGuid());

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => repository.UpdateAsync(payment));
    }

    [Fact]
    public async Task ExistsAsync_WhenPaymentExists_ShouldReturnTrue()
    {
        // Arrange
        using var context = CreateContext();
        var repository = new PaymentRepository(context);
        
        var paymentId = Guid.NewGuid();
        var entity = new PaymentEntity
        {
            Id = paymentId,
            OrderId = Guid.NewGuid(),
            Status = (int)EnumPaymentStatus.NotStarted,
            TotalAmount = 100.50m,
            CreatedAt = DateTime.UtcNow,
            OrderSnapshot = "{\"orderId\":\"123\"}"
        };
        
        context.Payments.Add(entity);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.ExistsAsync(paymentId);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task ExistsAsync_WhenPaymentDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        using var context = CreateContext();
        var repository = new PaymentRepository(context);

        // Act
        var result = await repository.ExistsAsync(Guid.NewGuid());

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task AddAsync_ShouldMapAllPropertiesCorrectly()
    {
        // Arrange
        using var context = CreateContext();
        var repository = new PaymentRepository(context);
        
        var orderId = Guid.NewGuid();
        var payment = new Payment(orderId, 150.75m, "{\"orderId\":\"123\",\"items\":[]}");
        payment.GenerateQrCode("https://qrcode.test.com");
        payment.Approve("EXT-456");

        // Act
        var result = await repository.AddAsync(payment);

        // Assert
        result.Should().NotBeNull();
        result.OrderId.Should().Be(orderId);
        result.TotalAmount.Should().Be(150.75m);
        result.OrderSnapshot.Should().Be("{\"orderId\":\"123\",\"items\":[]}");
        result.QrCodeUrl.Should().Be("https://qrcode.test.com");
        result.ExternalTransactionId.Should().Be("EXT-456");
        result.Status.Should().Be(EnumPaymentStatus.Approved);
    }
}
