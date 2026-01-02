namespace FastFood.PayStream.Infra.Persistence.Entities;

/// <summary>
/// Entidade de persistência que representa a estrutura da tabela Payments no banco de dados PostgreSQL.
/// Esta é a entidade de persistência, separada da entidade de domínio Payment.
/// </summary>
public class PaymentEntity
{
    /// <summary>
    /// Identificador único do pagamento.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// ID do pedido relacionado ao pagamento.
    /// </summary>
    public Guid OrderId { get; set; }

    /// <summary>
    /// Status do pagamento (representa EnumPaymentStatus como int para persistência).
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// ID da transação no gateway externo (ex: Mercado Pago).
    /// </summary>
    public string? ExternalTransactionId { get; set; }

    /// <summary>
    /// URL do QR Code gerado para o pagamento.
    /// </summary>
    public string? QrCodeUrl { get; set; }

    /// <summary>
    /// Data e hora de criação do pagamento.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Valor total do pedido replicado no momento da criação do pagamento.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Resumo do pedido serializado como JSON (será mapeado como JSONB no PostgreSQL).
    /// </summary>
    public string OrderSnapshot { get; set; } = string.Empty;
}
