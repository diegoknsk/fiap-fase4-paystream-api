using FluentAssertions;
using FastFood.PayStream.Infra.Auth;

namespace FastFood.PayStream.Tests.Unit.Infra.Auth;

public class CognitoOptionsTests
{
    [Fact]
    public void SectionName_ShouldReturnCorrectValue()
    {
        // Act
        var sectionName = CognitoOptions.SectionName;

        // Assert
        sectionName.Should().Be("Authentication:Cognito");
    }

    [Fact]
    public void Authority_ShouldReturnCorrectUrl()
    {
        // Arrange
        var options = new CognitoOptions
        {
            UserPoolId = "us-east-1_TestPool",
            Region = "us-east-1"
        };

        // Act
        var authority = options.Authority;

        // Assert
        authority.Should().Be("https://cognito-idp.us-east-1.amazonaws.com/us-east-1_TestPool");
    }

    [Fact]
    public void Authority_WithDifferentRegion_ShouldReturnCorrectUrl()
    {
        // Arrange
        var options = new CognitoOptions
        {
            UserPoolId = "sa-east-1_TestPool",
            Region = "sa-east-1"
        };

        // Act
        var authority = options.Authority;

        // Assert
        authority.Should().Be("https://cognito-idp.sa-east-1.amazonaws.com/sa-east-1_TestPool");
    }

    [Fact]
    public void ClockSkewMinutes_ShouldDefaultTo5()
    {
        // Arrange & Act
        var options = new CognitoOptions();

        // Assert
        options.ClockSkewMinutes.Should().Be(5);
    }

    [Fact]
    public void ClockSkewMinutes_ShouldBeSettable()
    {
        // Arrange
        var options = new CognitoOptions
        {
            ClockSkewMinutes = 10
        };

        // Act & Assert
        options.ClockSkewMinutes.Should().Be(10);
    }

    [Fact]
    public void Properties_ShouldBeInitializedWithDefaultValues()
    {
        // Arrange & Act
        var options = new CognitoOptions();

        // Assert
        options.UserPoolId.Should().BeEmpty();
        options.ClientId.Should().BeEmpty();
        options.Region.Should().Be("us-east-1");
        options.ClockSkewMinutes.Should().Be(5);
    }
}
