namespace FastFood.PayStream.Application.InputModels;

/// <summary>
/// Modelo de entrada para geração de QR Code de pagamento.
/// </summary>
public class GenerateQrCodeInputModel
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
