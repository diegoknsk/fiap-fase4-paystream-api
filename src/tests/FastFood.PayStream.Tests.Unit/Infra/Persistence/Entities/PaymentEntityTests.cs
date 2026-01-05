using FluentAssertions;
using FastFood.PayStream.Infra.Persistence.Entities;

namespace FastFood.PayStream.Tests.Unit.Infra.Persistence.Entities;

public class PaymentEntityTests
{
    [Fact]
    public void PaymentEntity_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var entity = new PaymentEntity();

        // Assert
        entity.Id.Should().Be(Guid.Empty);
        entity.OrderId.Should().Be(Guid.Empty);
        entity.Status.Should().Be(0);
        entity.ExternalTransactionId.Should().BeNull();
        entity.QrCodeUrl.Should().BeNull();
        entity.CreatedAt.Should().Be(default(DateTime));
        entity.TotalAmount.Should().Be(0);
        entity.OrderSnapshot.Should().BeEmpty();
    }

    [Fact]
    public void PaymentEntity_ShouldAllowSettingAllProperties()
    {
        // Arrange
        var id = Guid.NewGuid();
        var orderId = Guid.NewGuid();
        var status = 1;
        var externalTransactionId = "EXT-123";
        var qrCodeUrl = "https://qrcode.example.com";
        var createdAt = DateTime.UtcNow;
        var totalAmount = 150.75m;
        var orderSnapshot = "{\"orderId\":\"123\"}";

        // Act
        var entity = new PaymentEntity
        {
            Id = id,
            OrderId = orderId,
            Status = status,
            ExternalTransactionId = externalTransactionId,
            QrCodeUrl = qrCodeUrl,
            CreatedAt = createdAt,
            TotalAmount = totalAmount,
            OrderSnapshot = orderSnapshot
        };

        // Assert
        entity.Id.Should().Be(id);
        entity.OrderId.Should().Be(orderId);
        entity.Status.Should().Be(status);
        entity.ExternalTransactionId.Should().Be(externalTransactionId);
        entity.QrCodeUrl.Should().Be(qrCodeUrl);
        entity.CreatedAt.Should().Be(createdAt);
        entity.TotalAmount.Should().Be(totalAmount);
        entity.OrderSnapshot.Should().Be(orderSnapshot);
    }

    [Fact]
    public void PaymentEntity_ShouldAllowNullOptionalProperties()
    {
        // Arrange & Act
        var entity = new PaymentEntity
        {
            Id = Guid.NewGuid(),
            OrderId = Guid.NewGuid(),
            Status = 1,
            ExternalTransactionId = null,
            QrCodeUrl = null,
            CreatedAt = DateTime.UtcNow,
            TotalAmount = 100m,
            OrderSnapshot = "{}"
        };

        // Assert
        entity.ExternalTransactionId.Should().BeNull();
        entity.QrCodeUrl.Should().BeNull();
    }
}
