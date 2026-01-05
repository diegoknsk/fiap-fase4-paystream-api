namespace FastFood.PayStream.Application.Ports.Parameters;

/// <summary>
/// Modelo que representa o resultado da verificação de status do pagamento no gateway.
/// </summary>
public class PaymentStatusResult
{
    /// <summary>
    /// Indica se o pagamento foi aprovado.
    /// </summary>
    public bool IsApproved { get; set; }

    /// <summary>
    /// Indica se o pagamento está pendente.
    /// </summary>
    public bool IsPending { get; set; }

    /// <summary>
    /// Indica se o pagamento foi rejeitado.
    /// </summary>
    public bool IsRejected { get; set; }

    /// <summary>
    /// Indica se o pagamento foi cancelado.
    /// </summary>
    public bool IsCanceled { get; set; }

    /// <summary>
    /// ID da transação no gateway (se disponível).
    /// </summary>
    public string? TransactionId { get; set; }
}
