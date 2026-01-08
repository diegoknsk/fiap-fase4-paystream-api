using System.Net.Http.Json;
using System.Text.Json;
using FastFood.PayStream.Application.Ports;
using FastFood.PayStream.Application.Ports.Parameters;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace FastFood.PayStream.Infra.Services;

/// <summary>
/// Implementação do serviço de integração com a API de preparação da cozinha.
/// </summary>
public class KitchenService : IKitchenService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string _baseUrl;

    /// <summary>
    /// Construtor que recebe as dependências necessárias.
    /// </summary>
    /// <param name="httpClientFactory">Factory para criação de HttpClient.</param>
    /// <param name="httpContextAccessor">Acesso ao contexto HTTP para obter o token do request.</param>
    /// <param name="configuration">Configuração para ler URL da API de preparação.</param>
    public KitchenService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        
        _baseUrl = configuration["KitchenflowService:BaseUrl"] 
            ?? throw new InvalidOperationException("Configuração 'KitchenflowService:BaseUrl' não encontrada.");
    }

    /// <summary>
    /// Extrai o token Bearer do header Authorization do request.
    /// </summary>
    /// <returns>Token Bearer ou null se não encontrado.</returns>
    public string? GetBearerToken()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
            return null;

        // Extrair token do header Authorization
        var authHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault();
        if (string.IsNullOrWhiteSpace(authHeader))
            return null;

        // Remover prefixo "Bearer " se presente
        if (authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            return authHeader.Substring(7);

        return authHeader;
    }

    /// <inheritdoc />
    public async Task SendToPreparationAsync(Guid orderId, string orderSnapshot)
    {
        if (orderId == Guid.Empty)
        {
            throw new ArgumentException("OrderId não pode ser vazio.", nameof(orderId));
        }

        if (string.IsNullOrWhiteSpace(orderSnapshot))
        {
            throw new ArgumentException("OrderSnapshot não pode ser nulo ou vazio.", nameof(orderSnapshot));
        }

        var httpClient = _httpClientFactory.CreateClient();
        
        // Configurar base address se necessário
        if (!string.IsNullOrWhiteSpace(_baseUrl))
        {
            httpClient.BaseAddress = new Uri(_baseUrl);
        }

        // Criar objeto de requisição
        var request = new KitchenPreparationRequest
        {
            OrderId = orderId,
            OrderSnapshot = orderSnapshot
        };

        // Criar mensagem HTTP
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, "/api/Preparation")
        {
            Content = JsonContent.Create(request, options: new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            })
        };

        // Adicionar headers
        var token = GetBearerToken();
        if (string.IsNullOrWhiteSpace(token))
        {
            throw new InvalidOperationException("Token de autenticação não encontrado no header Authorization do request.");
        }
        
        httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        httpRequest.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        // Enviar requisição
        var response = await httpClient.SendAsync(httpRequest);

        // Verificar se a requisição foi bem-sucedida
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(
                $"Erro ao enviar pedido para a cozinha. Status: {response.StatusCode}. Detalhes: {errorContent}");
        }
    }
}
