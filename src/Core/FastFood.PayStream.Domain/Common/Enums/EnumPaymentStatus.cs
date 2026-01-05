namespace FastFood.PayStream.Domain.Common.Enums;

/// <summary>
/// Enum que representa os possíveis status de um pagamento no sistema.
/// </summary>
public enum EnumPaymentStatus
{
    /// <summary>
    /// Nenhuma tentativa de pagamento foi iniciada.
    /// </summary>
    NotStarted = 0,

    /// <summary>
    /// Cliente iniciou o processo de pagamento.
    /// </summary>
    Started = 1,

    /// <summary>
    /// QR Code foi gerado e está disponível para o cliente.
    /// </summary>
    QrCodeGenerated = 2,

    /// <summary>
    /// Pagamento foi confirmado e aprovado.
    /// </summary>
    Approved = 3,

    /// <summary>
    /// Pagamento foi recusado pelo gateway ou banco.
    /// </summary>
    Rejected = 4,

    /// <summary>
    /// Cliente cancelou o pagamento, ocorreu timeout ou erro no processamento.
    /// </summary>
    Canceled = 5
}
