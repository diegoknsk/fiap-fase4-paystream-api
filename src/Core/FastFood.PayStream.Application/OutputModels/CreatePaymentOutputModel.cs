namespace FastFood.PayStream.Application.OutputModels;

/// <summary>
/// Modelo de saída retornado pelo UseCase após criar um pagamento.
/// </summary>
public class CreatePaymentOutputModel
{
    /// <summary>
    /// ID do pagamento criado.
    /// </summary>
    public Guid PaymentId { get; set; }

    /// <summary>
    /// ID do pedido relacionado ao pagamento.
    /// </summary>
    public Guid OrderId { get; set; }

    /// <summary>
    /// Status do pagamento (representa EnumPaymentStatus como int).
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// Valor total do pedido.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Data e hora de criação do pagamento.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
