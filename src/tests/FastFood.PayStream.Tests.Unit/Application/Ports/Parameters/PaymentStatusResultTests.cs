using FluentAssertions;
using FastFood.PayStream.Application.Ports.Parameters;

namespace FastFood.PayStream.Tests.Unit.Application.Ports.Parameters;

public class PaymentStatusResultTests
{
    [Fact]
    public void IsApproved_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var result = new PaymentStatusResult();

        // Act
        result.IsApproved = true;

        // Assert
        result.IsApproved.Should().BeTrue();
    }

    [Fact]
    public void IsPending_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var result = new PaymentStatusResult();

        // Act
        result.IsPending = true;

        // Assert
        result.IsPending.Should().BeTrue();
    }

    [Fact]
    public void IsRejected_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var result = new PaymentStatusResult();

        // Act
        result.IsRejected = true;

        // Assert
        result.IsRejected.Should().BeTrue();
    }

    [Fact]
    public void IsCanceled_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var result = new PaymentStatusResult();

        // Act
        result.IsCanceled = true;

        // Assert
        result.IsCanceled.Should().BeTrue();
    }

    [Fact]
    public void TransactionId_WhenSet_ShouldReturnValue()
    {
        // Arrange
        var result = new PaymentStatusResult();
        var transactionId = "trans-789";

        // Act
        result.TransactionId = transactionId;

        // Assert
        result.TransactionId.Should().Be(transactionId);
    }

    [Fact]
    public void TransactionId_WhenNotSet_ShouldReturnNull()
    {
        // Arrange & Act
        var result = new PaymentStatusResult();

        // Assert
        result.TransactionId.Should().BeNull();
    }

    [Fact]
    public void AllFlags_CanBeSetIndependently()
    {
        // Arrange & Act
        var result = new PaymentStatusResult
        {
            IsApproved = true,
            IsPending = false,
            IsRejected = false,
            IsCanceled = false
        };

        // Assert
        result.IsApproved.Should().BeTrue();
        result.IsPending.Should().BeFalse();
        result.IsRejected.Should().BeFalse();
        result.IsCanceled.Should().BeFalse();
    }
}
