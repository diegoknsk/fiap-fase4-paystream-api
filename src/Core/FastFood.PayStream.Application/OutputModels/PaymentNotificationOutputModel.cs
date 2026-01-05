namespace FastFood.PayStream.Application.OutputModels;

/// <summary>
/// Modelo de saída retornado pelo UseCase após processar notificação de pagamento.
/// </summary>
public class PaymentNotificationOutputModel
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
