namespace FastFood.PayStream.Application.Responses;

/// <summary>
/// Resposta retornada pelo endpoint de notificação de pagamento (webhook).
/// Segue o padrão do projeto onde Response tem a mesma estrutura do OutputModel.
/// </summary>
public class PaymentNotificationResponse
{
    /// <summary>
    /// ID do pagamento atualizado.
    /// </summary>
    public Guid PaymentId { get; set; }

    /// <summary>
    /// ID do pedido relacionado ao pagamento.
    /// </summary>
    public Guid OrderId { get; set; }

    /// <summary>
    /// Status atualizado do pagamento (representa EnumPaymentStatus como int).
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// ID da transação no gateway externo (se aprovado).
    /// </summary>
    public string? ExternalTransactionId { get; set; }

    /// <summary>
    /// Mensagem descritiva do status.
    /// </summary>
    public string StatusMessage { get; set; } = string.Empty;
}
