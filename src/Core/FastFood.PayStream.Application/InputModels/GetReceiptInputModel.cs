namespace FastFood.PayStream.Application.InputModels;

/// <summary>
/// Modelo de entrada para obtenção de comprovante de pagamento do gateway.
/// </summary>
public class GetReceiptInputModel
{
    /// <summary>
    /// ID do pedido relacionado ao pagamento.
    /// </summary>
    public Guid OrderId { get; set; }

    /// <summary>
    /// Indica se deve usar gateway fake para desenvolvimento/testes (default: false).
    /// </summary>
    public bool FakeCheckout { get; set; } = false;
}
