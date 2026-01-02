namespace FastFood.PayStream.Application.Responses;

/// <summary>
/// Resposta retornada pelo endpoint de criação de pagamento.
/// Segue o padrão do projeto onde Response tem a mesma estrutura do OutputModel.
/// </summary>
public class CreatePaymentResponse
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
