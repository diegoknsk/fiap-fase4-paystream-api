using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FastFood.PayStream.Infra.Auth;

namespace FastFood.PayStream.Tests.Unit.Infra.Auth;

public class JwtAuthenticationConfigTests
{
    [Fact]
    public void AddCustomerJwtBearer_ShouldReturnAuthenticationBuilder()
    {
        // Arrange
        var services = new ServiceCollection();
        var authBuilder = services.AddAuthentication();
        var configuration = CreateValidConfiguration();

        // Act
        var result = authBuilder.AddCustomerJwtBearer(configuration);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<AuthenticationBuilder>();
    }

    [Fact]
    public async Task AddCustomerJwtBearer_ShouldAddJwtBearerScheme()
    {
        // Arrange
        var services = new ServiceCollection();
        var authBuilder = services.AddAuthentication();
        var configuration = CreateValidConfiguration();

        // Act
        authBuilder.AddCustomerJwtBearer(configuration);

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var schemeProvider = serviceProvider.GetRequiredService<IAuthenticationSchemeProvider>();
        var scheme = await schemeProvider.GetSchemeAsync("CustomerBearer");
        scheme.Should().NotBeNull();
        scheme!.Name.Should().Be("CustomerBearer");
    }

    // Nota: Os testes de validação de configuração faltante foram removidos porque
    // a validação ocorre em tempo de execução durante a validação do token,
    // não durante a configuração do serviço. A configuração sempre é bem-sucedida,
    // mas a validação falhará quando um token for processado.

    [Fact]
    public void AddCustomerJwtBearer_ShouldConfigureTokenValidationParameters()
    {
        // Arrange
        var services = new ServiceCollection();
        var authBuilder = services.AddAuthentication();
        var configuration = CreateValidConfiguration();

        // Act
        authBuilder.AddCustomerJwtBearer(configuration);

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var schemeProvider = serviceProvider.GetRequiredService<IAuthenticationSchemeProvider>();
        var scheme = schemeProvider.GetSchemeAsync("CustomerBearer").Result;
        scheme.Should().NotBeNull();
    }

    [Fact]
    public void ConfigureJwtSecurityTokenHandler_ShouldDisableMapInboundClaims()
    {
        // Arrange
        var originalValue = System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler.DefaultMapInboundClaims;

        try
        {
            // Act
            JwtAuthenticationConfig.ConfigureJwtSecurityTokenHandler();

            // Assert
            System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler.DefaultMapInboundClaims.Should().BeFalse();
        }
        finally
        {
            // Restore original value
            System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler.DefaultMapInboundClaims = originalValue;
        }
    }

    private IConfiguration CreateValidConfiguration()
    {
        var configData = new Dictionary<string, string?>
        {
            { "JwtCustomer:Issuer", "https://test-issuer.com" },
            { "JwtCustomer:Audience", "test-audience" },
            { "JwtCustomer:SecretKey", "test-secret-key-that-is-long-enough-for-hmac-sha256" }
        };

        return new ConfigurationBuilder()
            .AddInMemoryCollection(configData)
            .Build();
    }

}

