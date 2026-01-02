using FastFood.PayStream.Domain.Common.Enums;
using FastFood.PayStream.Domain.Common.Exceptions;

namespace FastFood.PayStream.Domain.Entities;

/// <summary>
/// Entidade de domínio que representa um pagamento no sistema.
/// </summary>
public class Payment
{
    /// <summary>
    /// Identificador único do pagamento.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// ID do pedido relacionado ao pagamento.
    /// </summary>
    public Guid OrderId { get; private set; }

    /// <summary>
    /// Status atual do pagamento.
    /// </summary>
    public EnumPaymentStatus Status { get; private set; }

    /// <summary>
    /// ID da transação no gateway externo (ex: Mercado Pago).
    /// </summary>
    public string? ExternalTransactionId { get; private set; }

    /// <summary>
    /// URL do QR Code gerado para o pagamento.
    /// </summary>
    public string? QrCodeUrl { get; private set; }

    /// <summary>
    /// Data e hora de criação do pagamento.
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Valor total do pedido replicado no momento da criação do pagamento.
    /// </summary>
    public decimal TotalAmount { get; private set; }

    /// <summary>
    /// Resumo do pedido serializado como JSON (JSONB).
    /// </summary>
    public string OrderSnapshot { get; private set; }

    /// <summary>
    /// Construtor protegido sem parâmetros para suporte ao Entity Framework Core.
    /// </summary>
    protected Payment()
    {
        OrderSnapshot = string.Empty;
    }

    /// <summary>
    /// Construtor público para criar uma nova instância de Payment.
    /// </summary>
    /// <param name="orderId">ID do pedido relacionado.</param>
    /// <param name="totalAmount">Valor total do pedido.</param>
    /// <param name="orderSnapshot">Resumo do pedido serializado como JSON.</param>
    public Payment(Guid orderId, decimal totalAmount, string orderSnapshot)
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
        Status = EnumPaymentStatus.NotStarted;
        CreatedAt = DateTime.UtcNow;
        TotalAmount = totalAmount;
        OrderSnapshot = orderSnapshot;
        ExternalTransactionId = null;
        QrCodeUrl = null;
    }

    /// <summary>
    /// Inicia o processo de pagamento, alterando o status para Started.
    /// </summary>
    public void Start()
    {
        Status = EnumPaymentStatus.Started;
    }

    /// <summary>
    /// Gera o QR Code para o pagamento e atualiza o status.
    /// </summary>
    /// <param name="qrCodeUrl">URL do QR Code gerado.</param>
    /// <exception cref="ArgumentException">Lançada quando qrCodeUrl é null, vazio ou contém apenas espaços em branco.</exception>
    public void GenerateQrCode(string qrCodeUrl)
    {
        DomainValidation.ThrowIfNullOrWhiteSpace(qrCodeUrl, "QR Code is required.");
        QrCodeUrl = qrCodeUrl;
        Status = EnumPaymentStatus.QrCodeGenerated;
    }

    /// <summary>
    /// Aprova o pagamento com o ID da transação do gateway externo.
    /// </summary>
    /// <param name="transactionId">ID da transação no gateway externo.</param>
    /// <exception cref="ArgumentException">Lançada quando transactionId é null, vazio ou contém apenas espaços em branco.</exception>
    public void Approve(string transactionId)
    {
        DomainValidation.ThrowIfNullOrWhiteSpace(transactionId, "Transaction ID is required.");
        ExternalTransactionId = transactionId;
        Status = EnumPaymentStatus.Approved;
    }

    /// <summary>
    /// Rejeita o pagamento, alterando o status para Rejected.
    /// </summary>
    public void Reject()
    {
        Status = EnumPaymentStatus.Rejected;
    }

    /// <summary>
    /// Cancela o pagamento, alterando o status para Canceled.
    /// </summary>
    public void Cancel()
    {
        Status = EnumPaymentStatus.Canceled;
    }
}
