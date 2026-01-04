using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FastFood.PayStream.Infra.Auth;

namespace FastFood.PayStream.Tests.Unit.Infra.Auth;

public class CognitoAuthenticationConfigTests
{
    [Fact]
    public void AddCognitoJwtBearer_ShouldReturnAuthenticationBuilder()
    {
        // Arrange
        var services = new ServiceCollection();
        var authBuilder = services.AddAuthentication();
        var configuration = CreateConfiguration();

        // Act
        var result = authBuilder.AddCognitoJwtBearer(configuration);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<AuthenticationBuilder>();
    }

    [Fact]
    public void AddCognitoJwtBearer_ShouldConfigureCognitoOptions()
    {
        // Arrange
        var services = new ServiceCollection();
        var authBuilder = services.AddAuthentication();
        var configuration = CreateConfiguration();

        // Act
        authBuilder.AddCognitoJwtBearer(configuration);

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var options = serviceProvider.GetRequiredService<Microsoft.Extensions.Options.IOptions<CognitoOptions>>();
        options.Value.Should().NotBeNull();
        options.Value.UserPoolId.Should().Be("us-east-1_TestPool");
        options.Value.ClientId.Should().Be("test-client-id");
    }

    [Fact]
    public async Task AddCognitoJwtBearer_ShouldAddJwtBearerScheme()
    {
        // Arrange
        var services = new ServiceCollection();
        var authBuilder = services.AddAuthentication();
        var configuration = CreateConfiguration();

        // Act
        authBuilder.AddCognitoJwtBearer(configuration);

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var schemeProvider = serviceProvider.GetRequiredService<IAuthenticationSchemeProvider>();
        var scheme = await schemeProvider.GetSchemeAsync("Cognito");
        scheme.Should().NotBeNull();
        scheme!.Name.Should().Be("Cognito");
    }

    private IConfiguration CreateConfiguration()
    {
        var configData = new Dictionary<string, string?>
        {
            { "Authentication:Cognito:UserPoolId", "us-east-1_TestPool" },
            { "Authentication:Cognito:ClientId", "test-client-id" },
            { "Authentication:Cognito:Region", "us-east-1" },
            { "Authentication:Cognito:ClockSkewMinutes", "5" }
        };

        return new ConfigurationBuilder()
            .AddInMemoryCollection(configData)
            .Build();
    }
}
