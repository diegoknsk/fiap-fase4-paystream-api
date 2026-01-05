using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using FastFood.PayStream.Infra.Auth;

namespace FastFood.PayStream.Tests.Unit.Infra.Auth;

public class AuthorizationConfigTests
{
    [Fact]
    public void AdminPolicy_ShouldReturnCorrectValue()
    {
        // Act
        var policyName = AuthorizationConfig.AdminPolicy;

        // Assert
        policyName.Should().Be("Admin");
    }

    [Fact]
    public void CustomerPolicy_ShouldReturnCorrectValue()
    {
        // Act
        var policyName = AuthorizationConfig.CustomerPolicy;

        // Assert
        policyName.Should().Be("Customer");
    }

    [Fact]
    public void CustomerWithScopePolicy_ShouldReturnCorrectValue()
    {
        // Act
        var policyName = AuthorizationConfig.CustomerWithScopePolicy;

        // Assert
        policyName.Should().Be("CustomerWithScope");
    }

    [Fact]
    public void AddAuthorizationPolicies_ShouldAddPoliciesToServices()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddLogging();

        // Act
        services.AddAuthorizationPolicies();

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var authorizationService = serviceProvider.GetRequiredService<IAuthorizationService>();
        authorizationService.Should().NotBeNull();
    }

    [Fact]
    public void AddAuthorizationPolicies_ShouldReturnServiceCollection()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var result = services.AddAuthorizationPolicies();

        // Assert
        result.Should().BeSameAs(services);
    }

    [Fact]
    public void AddAuthorizationPolicies_ShouldRegisterAuthorizationService()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddAuthorization();

        // Act
        services.AddAuthorizationPolicies();

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var authorizationService = serviceProvider.GetRequiredService<IAuthorizationService>();
        authorizationService.Should().NotBeNull();
    }
}
