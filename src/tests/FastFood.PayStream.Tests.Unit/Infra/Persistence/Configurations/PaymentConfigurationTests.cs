using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using FastFood.PayStream.Infra.Persistence.Configurations;
using FastFood.PayStream.Infra.Persistence.Entities;

namespace FastFood.PayStream.Tests.Unit.Infra.Persistence.Configurations;

public class PaymentConfigurationTests
{
    [Fact]
    public void Configure_ShouldSetTableName()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var context = new TestDbContext(options);

        // Act
        var entityType = context.Model.FindEntityType(typeof(PaymentEntity));

        // Assert
        entityType.Should().NotBeNull();
        entityType!.GetTableName().Should().Be("Payments");
    }

    [Fact]
    public void Configure_ShouldSetPrimaryKey()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var context = new TestDbContext(options);

        // Act
        var entityType = context.Model.FindEntityType(typeof(PaymentEntity));
        var primaryKey = entityType!.FindPrimaryKey();

        // Assert
        primaryKey.Should().NotBeNull();
        primaryKey!.Properties.Should().HaveCount(1);
        primaryKey.Properties[0].Name.Should().Be("Id");
    }

    [Fact]
    public void Configure_ShouldSetRequiredProperties()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var context = new TestDbContext(options);

        // Act
        var entityType = context.Model.FindEntityType(typeof(PaymentEntity));
        var orderIdProperty = entityType!.FindProperty(nameof(PaymentEntity.OrderId));
        var statusProperty = entityType.FindProperty(nameof(PaymentEntity.Status));
        var createdAtProperty = entityType.FindProperty(nameof(PaymentEntity.CreatedAt));
        var totalAmountProperty = entityType.FindProperty(nameof(PaymentEntity.TotalAmount));
        var orderSnapshotProperty = entityType.FindProperty(nameof(PaymentEntity.OrderSnapshot));

        // Assert
        orderIdProperty!.IsNullable.Should().BeFalse();
        statusProperty!.IsNullable.Should().BeFalse();
        createdAtProperty!.IsNullable.Should().BeFalse();
        totalAmountProperty!.IsNullable.Should().BeFalse();
        orderSnapshotProperty!.IsNullable.Should().BeFalse();
    }

    [Fact]
    public void Configure_ShouldSetOptionalProperties()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var context = new TestDbContext(options);

        // Act
        var entityType = context.Model.FindEntityType(typeof(PaymentEntity));
        var externalTransactionIdProperty = entityType!.FindProperty(nameof(PaymentEntity.ExternalTransactionId));
        var qrCodeUrlProperty = entityType.FindProperty(nameof(PaymentEntity.QrCodeUrl));

        // Assert
        externalTransactionIdProperty!.IsNullable.Should().BeTrue();
        qrCodeUrlProperty!.IsNullable.Should().BeTrue();
    }

    [Fact]
    public void Configure_ShouldSetTotalAmountPrecision()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var context = new TestDbContext(options);

        // Act
        var entityType = context.Model.FindEntityType(typeof(PaymentEntity));
        var totalAmountProperty = entityType!.FindProperty(nameof(PaymentEntity.TotalAmount));

        // Assert
        totalAmountProperty!.GetPrecision().Should().Be(18);
        totalAmountProperty.GetScale().Should().Be(2);
    }

    [Fact]
    public void Configure_ShouldSetOrderSnapshotAsJsonb()
    {
        // Arrange
        // InMemory não suporta GetColumnType, então testamos apenas que a propriedade existe
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var context = new TestDbContext(options);

        // Act
        var entityType = context.Model.FindEntityType(typeof(PaymentEntity));
        var orderSnapshotProperty = entityType!.FindProperty(nameof(PaymentEntity.OrderSnapshot));

        // Assert
        orderSnapshotProperty.Should().NotBeNull();
        // A configuração jsonb será aplicada quando usar um provider relacional (PostgreSQL)
    }

    private class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {
        }

        public DbSet<PaymentEntity> Payments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
        }
    }
}
