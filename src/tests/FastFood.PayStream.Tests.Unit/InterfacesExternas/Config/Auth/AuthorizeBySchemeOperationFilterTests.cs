using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using FastFood.PayStream.Api.Config.Auth;
using System.Reflection;

namespace FastFood.PayStream.Tests.Unit.InterfacesExternas.Config.Auth;

public class AuthorizeBySchemeOperationFilterTests
{
    private readonly AuthorizeBySchemeOperationFilter _filter;

    public AuthorizeBySchemeOperationFilterTests()
    {
        _filter = new AuthorizeBySchemeOperationFilter();
    }

    [Fact]
    public void Constructor_ShouldCreateInstance()
    {
        // Arrange & Act
        var filter = new AuthorizeBySchemeOperationFilter();

        // Assert
        filter.Should().NotBeNull();
    }

    [Fact]
    public void Apply_WhenMethodHasAllowAnonymous_ShouldNotAddSecurity()
    {
        // Arrange
        var operation = new OpenApiOperation();
        var methodInfo = typeof(TestController).GetMethod(nameof(TestController.AllowAnonymousMethod))!;
        var context = CreateOperationFilterContext(methodInfo);

        // Act
        _filter.Apply(operation, context);

        // Assert
        operation.Security.Should().BeEmpty();
    }

    [Fact]
    public void Apply_WhenClassHasAllowAnonymous_ShouldNotAddSecurity()
    {
        // Arrange
        var operation = new OpenApiOperation();
        var methodInfo = typeof(TestControllerWithClassAllowAnonymous).GetMethod(nameof(TestControllerWithClassAllowAnonymous.Method))!;
        var context = CreateOperationFilterContext(methodInfo);

        // Act
        _filter.Apply(operation, context);

        // Assert
        operation.Security.Should().BeEmpty();
    }

    [Fact]
    public void Apply_WhenMethodHasNoAuthorize_ShouldNotAddSecurity()
    {
        // Arrange
        var operation = new OpenApiOperation();
        var methodInfo = typeof(TestController).GetMethod(nameof(TestController.NoAuthorizeMethod))!;
        var context = CreateOperationFilterContext(methodInfo);

        // Act
        _filter.Apply(operation, context);

        // Assert
        operation.Security.Should().BeEmpty();
    }

    [Fact]
    public void Apply_WhenMethodHasAuthorizeWithoutScheme_ShouldAddDefaultSchemes()
    {
        // Arrange
        var operation = new OpenApiOperation();
        var methodInfo = typeof(TestController).GetMethod(nameof(TestController.AuthorizeMethod))!;
        var context = CreateOperationFilterContext(methodInfo);

        // Act
        _filter.Apply(operation, context);

        // Assert
        operation.Security.Should().NotBeEmpty();
        operation.Security.Should().HaveCount(2);
        operation.Security.Should().Contain(s => s.Keys.Any(k => k.Reference.Id == "CustomerBearer"));
        operation.Security.Should().Contain(s => s.Keys.Any(k => k.Reference.Id == "Cognito"));
    }

    [Fact]
    public void Apply_WhenMethodHasAuthorizeWithSingleScheme_ShouldAddSpecifiedScheme()
    {
        // Arrange
        var operation = new OpenApiOperation();
        var methodInfo = typeof(TestController).GetMethod(nameof(TestController.AuthorizeMethodWithScheme))!;
        var context = CreateOperationFilterContext(methodInfo);

        // Act
        _filter.Apply(operation, context);

        // Assert
        operation.Security.Should().NotBeEmpty();
        operation.Security.Should().HaveCount(1);
        operation.Security.Should().Contain(s => s.Keys.Any(k => k.Reference.Id == "Cognito"));
    }

    [Fact]
    public void Apply_WhenMethodHasAuthorizeWithMultipleSchemes_ShouldAddAllSchemes()
    {
        // Arrange
        var operation = new OpenApiOperation();
        var methodInfo = typeof(TestController).GetMethod(nameof(TestController.AuthorizeMethodWithMultipleSchemes))!;
        var context = CreateOperationFilterContext(methodInfo);

        // Act
        _filter.Apply(operation, context);

        // Assert
        operation.Security.Should().NotBeEmpty();
        operation.Security.Should().HaveCount(2);
        operation.Security.Should().Contain(s => s.Keys.Any(k => k.Reference.Id == "CustomerBearer"));
        operation.Security.Should().Contain(s => s.Keys.Any(k => k.Reference.Id == "Cognito"));
    }

    [Fact]
    public void Apply_WhenClassHasAuthorize_ShouldAddSecurity()
    {
        // Arrange
        var operation = new OpenApiOperation();
        var methodInfo = typeof(TestControllerWithClassAuthorize).GetMethod(nameof(TestControllerWithClassAuthorize.Method))!;
        var context = CreateOperationFilterContext(methodInfo);

        // Act
        _filter.Apply(operation, context);

        // Assert
        operation.Security.Should().NotBeEmpty();
        operation.Security.Should().HaveCount(2);
    }

    [Fact]
    public void Apply_WhenSchemeAlreadyExists_ShouldNotAddDuplicate()
    {
        // Arrange
        var operation = new OpenApiOperation();
        operation.Security.Add(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Cognito"
                    }
                },
                Array.Empty<string>()
            }
        });

        var methodInfo = typeof(TestController).GetMethod(nameof(TestController.AuthorizeMethodWithScheme))!;
        var context = CreateOperationFilterContext(methodInfo);

        // Act
        _filter.Apply(operation, context);

        // Assert
        operation.Security.Should().HaveCount(1);
    }

    [Fact]
    public void Apply_WhenAuthorizeHasEmptySchemes_ShouldAddDefaultSchemes()
    {
        // Arrange
        var operation = new OpenApiOperation();
        var methodInfo = typeof(TestController).GetMethod(nameof(TestController.AuthorizeMethodWithEmptySchemes))!;
        var context = CreateOperationFilterContext(methodInfo);

        // Act
        _filter.Apply(operation, context);

        // Assert
        operation.Security.Should().NotBeEmpty();
        operation.Security.Should().HaveCount(2);
    }

    private OperationFilterContext CreateOperationFilterContext(MethodInfo methodInfo)
    {
        // Criar um contexto simplificado usando Moq ou criar manualmente
        var schemaRepository = new SchemaRepository();
        var schemaGeneratorOptions = new SchemaGeneratorOptions();
        var jsonSerializerOptions = new System.Text.Json.JsonSerializerOptions();
        var dataContractResolver = new JsonSerializerDataContractResolver(jsonSerializerOptions);
        var schemaGenerator = new SchemaGenerator(schemaGeneratorOptions, dataContractResolver);
        
        // Criar ApiDescription manualmente
        var apiDescription = new Microsoft.AspNetCore.Mvc.ApiExplorer.ApiDescription();
        
        // Usar reflexão para definir o MethodInfo no ApiDescription
        var methodInfoField = typeof(Microsoft.AspNetCore.Mvc.ApiExplorer.ApiDescription)
            .GetField("_methodInfo", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        methodInfoField?.SetValue(apiDescription, methodInfo);
        
        return new OperationFilterContext(
            apiDescription,
            schemaGenerator,
            schemaRepository,
            methodInfo);
    }

    private class TestController
    {
        [AllowAnonymous]
        public void AllowAnonymousMethod() { }

        public void NoAuthorizeMethod() { }

        [Authorize]
        public void AuthorizeMethod() { }

        [Authorize(AuthenticationSchemes = "Cognito")]
        public void AuthorizeMethodWithScheme() { }

        [Authorize(AuthenticationSchemes = "CustomerBearer,Cognito")]
        public void AuthorizeMethodWithMultipleSchemes() { }

        [Authorize(AuthenticationSchemes = "")]
        public void AuthorizeMethodWithEmptySchemes() { }
    }

    [AllowAnonymous]
    private class TestControllerWithClassAllowAnonymous
    {
        public void Method() { }
    }

    [Authorize]
    private class TestControllerWithClassAuthorize
    {
        public void Method() { }
    }
}
