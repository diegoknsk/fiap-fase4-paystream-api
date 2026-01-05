namespace FastFood.PayStream.Application.OutputModels;

/// <summary>
/// Modelo de saída para obtenção de comprovante de pagamento do gateway.
/// </summary>
public class GetReceiptOutputModel
{
    /// <summary>
    /// ID do pagamento no gateway.
    /// </summary>
    public string PaymentId { get; set; } = string.Empty;

    /// <summary>
    /// Referência externa do pagamento.
    /// </summary>
    public string ExternalReference { get; set; } = string.Empty;

    /// <summary>
    /// Status do pagamento.
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Detalhes do status do pagamento.
    /// </summary>
    public string StatusDetail { get; set; } = string.Empty;

    /// <summary>
    /// Valor total pago.
    /// </summary>
    public decimal TotalPaidAmount { get; set; }

    /// <summary>
    /// Método de pagamento utilizado.
    /// </summary>
    public string PaymentMethod { get; set; } = string.Empty;

    /// <summary>
    /// Tipo de pagamento.
    /// </summary>
    public string PaymentType { get; set; } = string.Empty;

    /// <summary>
    /// Moeda utilizada.
    /// </summary>
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Data de aprovação do pagamento.
    /// </summary>
    public DateTime DateApproved { get; set; }
}
