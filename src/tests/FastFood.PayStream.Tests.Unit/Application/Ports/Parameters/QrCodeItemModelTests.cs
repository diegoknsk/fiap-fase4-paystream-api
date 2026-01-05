using FluentAssertions;
using FastFood.PayStream.Application.Ports.Parameters;

namespace FastFood.PayStream.Tests.Unit.Application.Ports.Parameters;

public class QrCodeItemModelTests
{
    [Fact]
    public void Title_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var item = new QrCodeItemModel();
        var title = "Hambúrguer";

        // Act
        item.Title = title;

        // Assert
        item.Title.Should().Be(title);
    }

    [Fact]
    public void Title_WhenNotSet_ShouldReturnEmptyString()
    {
        // Arrange & Act
        var item = new QrCodeItemModel();

        // Assert
        item.Title.Should().BeEmpty();
    }

    [Fact]
    public void Description_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var item = new QrCodeItemModel();
        var description = "Hambúrguer artesanal";

        // Act
        item.Description = description;

        // Assert
        item.Description.Should().Be(description);
    }

    [Fact]
    public void UnitPrice_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var item = new QrCodeItemModel();
        var unitPrice = 25.50m;

        // Act
        item.UnitPrice = unitPrice;

        // Assert
        item.UnitPrice.Should().Be(unitPrice);
    }

    [Fact]
    public void Quantity_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var item = new QrCodeItemModel();
        var quantity = 3;

        // Act
        item.Quantity = quantity;

        // Assert
        item.Quantity.Should().Be(quantity);
    }

    [Fact]
    public void UnitMeasure_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var item = new QrCodeItemModel();
        var unitMeasure = "unidade";

        // Act
        item.UnitMeasure = unitMeasure;

        // Assert
        item.UnitMeasure.Should().Be(unitMeasure);
    }

    [Fact]
    public void TotalAmount_ShouldCalculateCorrectly()
    {
        // Arrange
        var item = new QrCodeItemModel
        {
            UnitPrice = 10.50m,
            Quantity = 2
        };

        // Act
        var totalAmount = item.TotalAmount;

        // Assert
        totalAmount.Should().Be(21.00m);
    }

    [Fact]
    public void TotalAmount_WhenQuantityIsZero_ShouldReturnZero()
    {
        // Arrange
        var item = new QrCodeItemModel
        {
            UnitPrice = 10.50m,
            Quantity = 0
        };

        // Act
        var totalAmount = item.TotalAmount;

        // Assert
        totalAmount.Should().Be(0m);
    }

    [Fact]
    public void TotalAmount_WhenUnitPriceIsZero_ShouldReturnZero()
    {
        // Arrange
        var item = new QrCodeItemModel
        {
            UnitPrice = 0m,
            Quantity = 5
        };

        // Act
        var totalAmount = item.TotalAmount;

        // Assert
        totalAmount.Should().Be(0m);
    }

    [Fact]
    public void TotalAmount_WithDecimalValues_ShouldCalculateCorrectly()
    {
        // Arrange
        var item = new QrCodeItemModel
        {
            UnitPrice = 15.99m,
            Quantity = 3
        };

        // Act
        var totalAmount = item.TotalAmount;

        // Assert
        totalAmount.Should().Be(47.97m);
    }
}
