using FluentAssertions;
using FastFood.PayStream.Application.Models.Common;
using FastFood.PayStream.Application.Responses;

namespace FastFood.PayStream.Tests.Unit.Application.Models.Common;

public class ApiResponseTests
{
    [Fact]
    public void Ok_WhenDataIsNotNull_ShouldReturnSuccessResponse()
    {
        // Arrange
        var data = new CreatePaymentResponse
        {
            PaymentId = Guid.NewGuid(),
            OrderId = Guid.NewGuid(),
            Status = 0,
            TotalAmount = 100.50m,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var result = ApiResponse<CreatePaymentResponse>.Ok(data, "Test message");

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Message.Should().Be("Test message");
        result.Content.Should().Be(data);
    }

    [Fact]
    public void Ok_WhenDataIsNull_ShouldReturnSuccessResponseWithNullContent()
    {
        // Act
        var result = ApiResponse<CreatePaymentResponse>.Ok(null, "Test message");

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Message.Should().Be("Test message");
        result.Content.Should().BeNull();
    }

    [Fact]
    public void Ok_WhenNoData_ShouldReturnSuccessResponse()
    {
        // Act
        var result = ApiResponse<CreatePaymentResponse>.Ok("Test message");

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Message.Should().Be("Test message");
        result.Content.Should().BeNull();
    }

    [Fact]
    public void Ok_WhenNoMessage_ShouldUseDefaultMessage()
    {
        // Arrange
        var data = new CreatePaymentResponse
        {
            PaymentId = Guid.NewGuid(),
            OrderId = Guid.NewGuid(),
            Status = 0,
            TotalAmount = 100.50m,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var result = ApiResponse<CreatePaymentResponse>.Ok(data);

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Message.Should().Be("Requisição bem-sucedida.");
        result.Content.Should().Be(data);
    }

    [Fact]
    public void Fail_WhenMessageProvided_ShouldReturnFailureResponse()
    {
        // Act
        var result = ApiResponse<CreatePaymentResponse>.Fail("Error message");

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeFalse();
        result.Message.Should().Be("Error message");
        result.Content.Should().BeNull();
    }

    [Fact]
    public void Fail_WhenMessageIsNull_ShouldReturnFailureResponse()
    {
        // Act
        var result = ApiResponse<CreatePaymentResponse>.Fail(null);

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeFalse();
        result.Message.Should().BeNull();
        result.Content.Should().BeNull();
    }

    [Fact]
    public void Constructor_WhenAllParametersProvided_ShouldSetProperties()
    {
        // Arrange
        var content = new CreatePaymentResponse
        {
            PaymentId = Guid.NewGuid(),
            OrderId = Guid.NewGuid(),
            Status = 0,
            TotalAmount = 100.50m,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var result = new ApiResponse<CreatePaymentResponse>(content, "Custom message", true);

        // Assert
        result.Content.Should().Be(content);
        result.Message.Should().Be("Custom message");
        result.Success.Should().BeTrue();
    }

    [Fact]
    public void ToNamedContent_ShouldReturnSameObject()
    {
        // Arrange
        var obj = new CreatePaymentResponse
        {
            PaymentId = Guid.NewGuid(),
            OrderId = Guid.NewGuid(),
            Status = 0,
            TotalAmount = 100.50m,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var result = obj.ToNamedContent();

        // Assert
        result.Should().BeSameAs(obj);
    }
}
