using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using FluentAssertions;
using Moq;
using FastFood.PayStream.Infra.Services;
using FastFood.PayStream.Application.Ports.Parameters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Http;

namespace FastFood.PayStream.Tests.Unit.Infra.Services;

public class KitchenServiceTests
{
    private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly Mock<IConfigurationSection> _kitchenApiSectionMock;
    private readonly KitchenService _kitchenService;
    private readonly HttpClient _httpClient;
    private readonly HttpMessageHandler _messageHandler;

    public KitchenServiceTests()
    {
        _httpClientFactoryMock = new Mock<IHttpClientFactory>();
        _configurationMock = new Mock<IConfiguration>();
        _kitchenApiSectionMock = new Mock<IConfigurationSection>();

        // Configurar mocks de configuração
        _configurationMock
            .Setup(c => c["KitchenApi:BaseUrl"])
            .Returns("http://localhost:5010");
        
        _configurationMock
            .Setup(c => c["KitchenApi:Token"])
            .Returns("test-token-123");

        // Criar HttpClient com handler mockado
        _messageHandler = new MockHttpMessageHandler();
        _httpClient = new HttpClient(_messageHandler)
        {
            BaseAddress = new Uri("http://localhost:5010")
        };

        _httpClientFactoryMock
            .Setup(f => f.CreateClient(It.IsAny<string>()))
            .Returns(_httpClient);

        _kitchenService = new KitchenService(_httpClientFactoryMock.Object, _configurationMock.Object);
    }

    [Fact]
    public async Task SendToPreparationAsync_WhenValidInput_ShouldSendRequestSuccessfully()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var orderSnapshot = "{\"items\":[],\"order\":{},\"pricing\":{},\"version\":1}";
        var handler = (MockHttpMessageHandler)_messageHandler;
        handler.SetupResponse(HttpStatusCode.OK, "{}");

        // Act
        var action = async () => await _kitchenService.SendToPreparationAsync(orderId, orderSnapshot);

        // Assert
        await action.Should().NotThrowAsync();
        handler.Requests.Should().HaveCount(1);
        var request = handler.Requests[0];
        request.Method.Should().Be(HttpMethod.Post);
        request.RequestUri.Should().NotBeNull();
        request.RequestUri!.AbsolutePath.Should().Be("/api/Preparation");
    }

    [Fact]
    public async Task SendToPreparationAsync_WhenValidInput_ShouldSendCorrectHeaders()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var orderSnapshot = "{\"items\":[],\"order\":{},\"pricing\":{},\"version\":1}";
        var handler = (MockHttpMessageHandler)_messageHandler;
        handler.SetupResponse(HttpStatusCode.OK, "{}");

        // Act
        await _kitchenService.SendToPreparationAsync(orderId, orderSnapshot);

        // Assert
        var request = handler.Requests[0];
        request.Headers.Authorization.Should().NotBeNull();
        request.Headers.Authorization!.Scheme.Should().Be("Bearer");
        request.Headers.Authorization.Parameter.Should().Be("test-token-123");
        request.Headers.Accept.Should().Contain(h => h.MediaType == "application/json");
    }

    [Fact]
    public async Task SendToPreparationAsync_WhenValidInput_ShouldSendCorrectBody()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var orderSnapshot = "{\"items\":[],\"order\":{},\"pricing\":{},\"version\":1}";
        var handler = (MockHttpMessageHandler)_messageHandler;
        handler.SetupResponse(HttpStatusCode.OK, "{}");

        // Act
        await _kitchenService.SendToPreparationAsync(orderId, orderSnapshot);

        // Assert
        var request = handler.Requests[0];
        var content = await request.Content!.ReadAsStringAsync();
        var requestBody = JsonSerializer.Deserialize<KitchenPreparationRequest>(
            content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        requestBody.Should().NotBeNull();
        requestBody!.OrderId.Should().Be(orderId);
        requestBody.OrderSnapshot.Should().Be(orderSnapshot);
    }

    [Fact]
    public async Task SendToPreparationAsync_WhenOrderIdIsEmpty_ShouldThrowArgumentException()
    {
        // Arrange
        var orderSnapshot = "{\"items\":[]}";

        // Act & Assert
        var action = async () => await _kitchenService.SendToPreparationAsync(Guid.Empty, orderSnapshot);
        await action.Should().ThrowAsync<ArgumentException>()
            .WithMessage("OrderId não pode ser vazio.*");
    }

    [Fact]
    public async Task SendToPreparationAsync_WhenOrderSnapshotIsNull_ShouldThrowArgumentException()
    {
        // Arrange
        var orderId = Guid.NewGuid();

        // Act & Assert
        var action = async () => await _kitchenService.SendToPreparationAsync(orderId, null!);
        await action.Should().ThrowAsync<ArgumentException>()
            .WithMessage("OrderSnapshot não pode ser nulo ou vazio.*");
    }

    [Fact]
    public async Task SendToPreparationAsync_WhenOrderSnapshotIsEmpty_ShouldThrowArgumentException()
    {
        // Arrange
        var orderId = Guid.NewGuid();

        // Act & Assert
        var action = async () => await _kitchenService.SendToPreparationAsync(orderId, string.Empty);
        await action.Should().ThrowAsync<ArgumentException>()
            .WithMessage("OrderSnapshot não pode ser nulo ou vazio.*");
    }

    [Fact]
    public async Task SendToPreparationAsync_WhenOrderSnapshotIsWhitespace_ShouldThrowArgumentException()
    {
        // Arrange
        var orderId = Guid.NewGuid();

        // Act & Assert
        var action = async () => await _kitchenService.SendToPreparationAsync(orderId, "   ");
        await action.Should().ThrowAsync<ArgumentException>()
            .WithMessage("OrderSnapshot não pode ser nulo ou vazio.*");
    }

    [Fact]
    public async Task SendToPreparationAsync_WhenHttpError_ShouldThrowHttpRequestException()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var orderSnapshot = "{\"items\":[]}";
        var handler = (MockHttpMessageHandler)_messageHandler;
        handler.SetupResponse(HttpStatusCode.BadRequest, "{\"error\":\"Invalid request\"}");

        // Act & Assert
        var action = async () => await _kitchenService.SendToPreparationAsync(orderId, orderSnapshot);
        await action.Should().ThrowAsync<HttpRequestException>()
            .WithMessage("*Status: BadRequest*");
    }

    [Fact]
    public async Task SendToPreparationAsync_WhenHttp500Error_ShouldThrowHttpRequestException()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var orderSnapshot = "{\"items\":[]}";
        var handler = (MockHttpMessageHandler)_messageHandler;
        handler.SetupResponse(HttpStatusCode.InternalServerError, "{\"error\":\"Internal server error\"}");

        // Act & Assert
        var action = async () => await _kitchenService.SendToPreparationAsync(orderId, orderSnapshot);
        await action.Should().ThrowAsync<HttpRequestException>()
            .WithMessage("*Status: InternalServerError*");
    }

    [Fact]
    public async Task SendToPreparationAsync_WhenHttp401Error_ShouldThrowHttpRequestException()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var orderSnapshot = "{\"items\":[]}";
        var handler = (MockHttpMessageHandler)_messageHandler;
        handler.SetupResponse(HttpStatusCode.Unauthorized, "{\"error\":\"Unauthorized\"}");

        // Act & Assert
        var action = async () => await _kitchenService.SendToPreparationAsync(orderId, orderSnapshot);
        await action.Should().ThrowAsync<HttpRequestException>()
            .WithMessage("*Status: Unauthorized*");
    }

    [Fact]
    public void Constructor_WhenHttpClientFactoryIsNull_ShouldThrowArgumentNullException()
    {
        // Act & Assert
        var action = () => new KitchenService(null!, _configurationMock.Object);
        action.Should().Throw<ArgumentNullException>()
            .WithParameterName("httpClientFactory");
    }

    [Fact]
    public void Constructor_WhenBaseUrlIsMissing_ShouldThrowInvalidOperationException()
    {
        // Arrange
        _configurationMock
            .Setup(c => c["KitchenApi:BaseUrl"])
            .Returns((string?)null);

        // Act & Assert
        var action = () => new KitchenService(_httpClientFactoryMock.Object, _configurationMock.Object);
        action.Should().Throw<InvalidOperationException>()
            .WithMessage("*KitchenApi:BaseUrl*");
    }

    [Fact]
    public void Constructor_WhenTokenIsMissing_ShouldThrowInvalidOperationException()
    {
        // Arrange
        _configurationMock
            .Setup(c => c["KitchenApi:Token"])
            .Returns((string?)null);

        // Act & Assert
        var action = () => new KitchenService(_httpClientFactoryMock.Object, _configurationMock.Object);
        action.Should().Throw<InvalidOperationException>()
            .WithMessage("*KitchenApi:Token*");
    }
}

/// <summary>
/// Mock HttpMessageHandler para testes de HttpClient
/// </summary>
public class MockHttpMessageHandler : HttpMessageHandler
{
    private HttpResponseMessage? _response;
    public List<HttpRequestMessage> Requests { get; } = new();

    public void SetupResponse(HttpStatusCode statusCode, string content)
    {
        _response = new HttpResponseMessage(statusCode)
        {
            Content = new StringContent(content, Encoding.UTF8, "application/json")
        };
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        Requests.Add(request);
        return Task.FromResult(_response ?? new HttpResponseMessage(HttpStatusCode.OK));
    }

    protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        Requests.Add(request);
        return _response ?? new HttpResponseMessage(HttpStatusCode.OK);
    }
}
