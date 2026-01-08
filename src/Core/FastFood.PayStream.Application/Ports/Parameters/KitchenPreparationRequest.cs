using System.Text.Json.Serialization;

namespace FastFood.PayStream.Application.Ports.Parameters;

/// <summary>
/// Modelo que representa o payload da requisição para a API de preparação da cozinha.
/// </summary>
public class KitchenPreparationRequest
{
    /// <summary>
    /// ID do pedido a ser enviado para preparação.
    /// </summary>
    [JsonPropertyName("orderId")]
    public Guid OrderId { get; set; }

    /// <summary>
    /// Snapshot do pedido serializado como JSON string.
    /// </summary>
    [JsonPropertyName("orderSnapshot")]
    public string OrderSnapshot { get; set; } = string.Empty;
}
