using FluentAssertions;
using FastFood.PayStream.Api.Config.Auth;

namespace FastFood.PayStream.Tests.Unit.InterfacesExternas.Config.Auth;

/// <summary>
/// Testes básicos para AuthorizeBySchemeOperationFilter.
/// Testes mais complexos que requerem mock do OperationFilterContext
/// podem ser adicionados em testes de integração.
/// </summary>
public class AuthorizeBySchemeOperationFilterTests
{
    [Fact]
    public void Constructor_ShouldCreateInstance()
    {
        // Arrange & Act
        var filter = new AuthorizeBySchemeOperationFilter();

        // Assert
        filter.Should().NotBeNull();
    }
}
