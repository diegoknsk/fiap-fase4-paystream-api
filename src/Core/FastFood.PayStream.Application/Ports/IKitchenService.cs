namespace FastFood.PayStream.Application.Ports;

/// <summary>
/// Interface que define o contrato para integração com a API de preparação da cozinha.
/// </summary>
public interface IKitchenService
{
    /// <summary>
    /// Envia um pedido para a cozinha para preparação.
    /// </summary>
    /// <param name="orderId">ID do pedido a ser enviado para preparação.</param>
    /// <param name="orderSnapshot">Snapshot do pedido serializado como JSON string.</param>
    /// <returns>Task que representa a operação assíncrona.</returns>
    /// <exception cref="HttpRequestException">Lançada quando ocorre erro na comunicação com a API de preparação.</exception>
    Task SendToPreparationAsync(Guid orderId, string orderSnapshot);
}
