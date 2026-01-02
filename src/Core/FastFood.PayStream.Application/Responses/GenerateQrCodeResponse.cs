namespace FastFood.PayStream.Application.Responses;

/// <summary>
/// Resposta da operação de geração de QR Code de pagamento.
/// </summary>
public class GenerateQrCodeResponse
{
    /// <summary>
    /// URL do QR Code gerado.
    /// </summary>
    public string QrCodeUrl { get; set; } = string.Empty;

    /// <summary>
    /// ID do pagamento.
    /// </summary>
    public Guid PaymentId { get; set; }

    /// <summary>
    /// ID do pedido relacionado.
    /// </summary>
    public Guid OrderId { get; set; }
}
