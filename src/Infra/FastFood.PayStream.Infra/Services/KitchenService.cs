using System.Net.Http.Json;
using System.Text.Json;
using FastFood.PayStream.Application.Ports;
using FastFood.PayStream.Application.Ports.Parameters;
using Microsoft.Extensions.Configuration;

namespace FastFood.PayStream.Infra.Services;

/// <summary>
/// Implementação do serviço de integração com a API de preparação da cozinha.
/// </summary>
public class KitchenService : IKitchenService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _baseUrl;
    private readonly string _token;

    /// <summary>
    /// Construtor que recebe as dependências necessárias.
    /// </summary>
    /// <param name="httpClientFactory">Factory para criação de HttpClient.</param>
    /// <param name="configuration">Configuração para ler URL e token da API de preparação.</param>
    public KitchenService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        
        _baseUrl = configuration["KitchenApi:BaseUrl"] 
            ?? throw new InvalidOperationException("Configuração 'KitchenApi:BaseUrl' não encontrada.");
        
        _token = configuration["KitchenApi:Token"] 
            ?? throw new InvalidOperationException("Configuração 'KitchenApi:Token' não encontrada.");
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
        httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
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
