using FastFood.PayStream.Application.Ports.Parameters;

namespace FastFood.PayStream.Application.Ports;

/// <summary>
/// Interface que define o contrato para integração com gateways de pagamento (Mercado Pago real e Fake).
/// </summary>
public interface IPaymentGateway
{
    /// <summary>
    /// Gera um QR Code para pagamento no gateway.
    /// </summary>
    /// <param name="externalReference">Referência externa do pagamento (geralmente o ID do pagamento).</param>
    /// <param name="orderCode">Código do pedido.</param>
    /// <param name="items">Lista de itens do pedido para geração do QR Code.</param>
    /// <returns>URL do QR Code gerado.</returns>
    Task<string> GenerateQrCodeAsync(string externalReference, string orderCode, List<QrCodeItemModel> items);

    /// <summary>
    /// Obtém o comprovante de pagamento do gateway.
    /// </summary>
    /// <param name="paymentId">ID da transação no gateway externo.</param>
    /// <returns>Comprovante de pagamento.</returns>
    Task<PaymentReceipt> GetReceiptFromGatewayAsync(string paymentId);

    /// <summary>
    /// Verifica o status do pagamento no gateway.
    /// </summary>
    /// <param name="externalReference">Referência externa do pagamento.</param>
    /// <returns>Resultado com o status do pagamento.</returns>
    Task<PaymentStatusResult> CheckPaymentStatusAsync(string externalReference);
}
