using FluentAssertions;
using FastFood.PayStream.Infra.Auth;

namespace FastFood.PayStream.Tests.Unit.Infra.Auth;

public class JwtOptionsTests
{
    [Fact]
    public void Constructor_ShouldInitializeAllProperties()
    {
        // Arrange
        var issuer = "https://test-issuer.com";
        var audience = "test-audience";
        var secretKey = "test-secret-key";
        var expiresInMinutes = 60;

        // Act
        var options = new JwtOptions(issuer, audience, secretKey, expiresInMinutes);

        // Assert
        options.Issuer.Should().Be(issuer);
        options.Audience.Should().Be(audience);
        options.SecretKey.Should().Be(secretKey);
        options.ExpiresInMinutes.Should().Be(expiresInMinutes);
    }

    [Fact]
    public void Constructor_WithDifferentValues_ShouldSetCorrectly()
    {
        // Arrange
        var issuer = "https://another-issuer.com";
        var audience = "another-audience";
        var secretKey = "another-secret-key";
        var expiresInMinutes = 120;

        // Act
        var options = new JwtOptions(issuer, audience, secretKey, expiresInMinutes);

        // Assert
        options.Issuer.Should().Be(issuer);
        options.Audience.Should().Be(audience);
        options.SecretKey.Should().Be(secretKey);
        options.ExpiresInMinutes.Should().Be(expiresInMinutes);
    }

    [Fact]
    public void RecordEquality_ShouldWorkCorrectly()
    {
        // Arrange
        var options1 = new JwtOptions("issuer", "audience", "secret", 60);
        var options2 = new JwtOptions("issuer", "audience", "secret", 60);

        // Act & Assert
        options1.Should().Be(options2);
        (options1 == options2).Should().BeTrue();
    }

    [Fact]
    public void RecordInequality_ShouldWorkCorrectly()
    {
        // Arrange
        var options1 = new JwtOptions("issuer1", "audience", "secret", 60);
        var options2 = new JwtOptions("issuer2", "audience", "secret", 60);

        // Act & Assert
        options1.Should().NotBe(options2);
        (options1 != options2).Should().BeTrue();
    }
}
