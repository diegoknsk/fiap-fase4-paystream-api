using FastFood.PayStream.Domain.Common.Exceptions;
using FluentAssertions;

namespace FastFood.PayStream.Tests.Unit.Domain.Common.Exceptions;

public class DomainValidationTests
{
    [Fact]
    public void ThrowIfNullOrWhiteSpace_WhenValueIsNull_ShouldThrowArgumentException()
    {
        // Arrange
        string? value = null;
        var message = "Value cannot be null";

        // Act & Assert
        var action = () => DomainValidation.ThrowIfNullOrWhiteSpace(value, message);
        action.Should().Throw<ArgumentException>()
            .WithMessage(message);
    }

    [Fact]
    public void ThrowIfNullOrWhiteSpace_WhenValueIsEmpty_ShouldThrowArgumentException()
    {
        // Arrange
        var value = string.Empty;
        var message = "Value cannot be empty";

        // Act & Assert
        var action = () => DomainValidation.ThrowIfNullOrWhiteSpace(value, message);
        action.Should().Throw<ArgumentException>()
            .WithMessage(message);
    }

    [Fact]
    public void ThrowIfNullOrWhiteSpace_WhenValueIsWhitespace_ShouldThrowArgumentException()
    {
        // Arrange
        var value = "   ";
        var message = "Value cannot be whitespace";

        // Act & Assert
        var action = () => DomainValidation.ThrowIfNullOrWhiteSpace(value, message);
        action.Should().Throw<ArgumentException>()
            .WithMessage(message);
    }

    [Fact]
    public void ThrowIfNullOrWhiteSpace_WhenValueIsValid_ShouldNotThrow()
    {
        // Arrange
        var value = "valid value";
        var message = "Should not throw";

        // Act & Assert
        var action = () => DomainValidation.ThrowIfNullOrWhiteSpace(value, message);
        action.Should().NotThrow();
    }

    [Fact]
    public void ThrowIfNullOrWhiteSpace_WhenValueIsValidWithSpaces_ShouldNotThrow()
    {
        // Arrange
        var value = "valid value with spaces";
        var message = "Should not throw";

        // Act & Assert
        var action = () => DomainValidation.ThrowIfNullOrWhiteSpace(value, message);
        action.Should().NotThrow();
    }
}
