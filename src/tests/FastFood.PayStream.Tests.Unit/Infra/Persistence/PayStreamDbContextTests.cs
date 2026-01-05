using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using FastFood.PayStream.Infra.Persistence;
using FastFood.PayStream.Infra.Persistence.Entities;

namespace FastFood.PayStream.Tests.Unit.Infra.Persistence;

public class PayStreamDbContextTests
{
    [Fact]
    public void Constructor_ShouldInitializeDbContext()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<PayStreamDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        // Act
        using var context = new PayStreamDbContext(options);

        // Assert
        context.Should().NotBeNull();
        context.Payments.Should().NotBeNull();
    }

    [Fact]
    public void Payments_ShouldBeDbSet()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<PayStreamDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var context = new PayStreamDbContext(options);

        // Act & Assert
        context.Payments.Should().NotBeNull();
    }

    [Fact]
    public void OnModelCreating_ShouldApplyPaymentConfiguration()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<PayStreamDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var context = new PayStreamDbContext(options);

        // Act
        var entityType = context.Model.FindEntityType(typeof(PaymentEntity));

        // Assert
        entityType.Should().NotBeNull();
        entityType!.GetTableName().Should().Be("Payments");
    }

    [Fact]
    public async Task SaveChangesAsync_ShouldPersistPaymentEntity()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<PayStreamDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var context = new PayStreamDbContext(options);
        var payment = new PaymentEntity
        {
            Id = Guid.NewGuid(),
            OrderId = Guid.NewGuid(),
            Status = 1,
            TotalAmount = 100.50m,
            CreatedAt = DateTime.UtcNow,
            OrderSnapshot = "{\"orderId\":\"123\"}"
        };

        // Act
        context.Payments.Add(payment);
        await context.SaveChangesAsync();

        // Assert
        var savedPayment = await context.Payments.FindAsync(payment.Id);
        savedPayment.Should().NotBeNull();
        savedPayment!.Id.Should().Be(payment.Id);
        savedPayment.OrderId.Should().Be(payment.OrderId);
    }
}
