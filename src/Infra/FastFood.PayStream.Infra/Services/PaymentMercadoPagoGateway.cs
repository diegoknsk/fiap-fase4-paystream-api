using FastFood.PayStream.Application.Ports;
using FastFood.PayStream.Application.Ports.Parameters;

namespace FastFood.PayStream.Infra.Services;

/// <summary>
/// Implementação real do gateway de pagamento do Mercado Pago.
/// Esta implementação será completada em uma story futura com a integração real com a API do Mercado Pago.
/// Por enquanto, retorna dados simulados.
/// </summary>
public class PaymentMercadoPagoGateway : IPaymentGateway
{
    /// <inheritdoc />
    public Task<string> GenerateQrCodeAsync(string externalReference, string orderCode, List<QrCodeItemModel> items)
    {
        // TODO: Implementar integração real com Mercado Pago
        // Por enquanto, retornar URL simulada
        var qrCodeUrl = $"https://api.mercadopago.com/qrcode/{externalReference}?order={orderCode}";
        return Task.FromResult(qrCodeUrl);
    }

    /// <inheritdoc />
    public Task<PaymentReceipt> GetReceiptFromGatewayAsync(string paymentId)
    {
        // TODO: Implementar integração real com Mercado Pago
        // Por enquanto, retornar comprovante simulado
        var receipt = new PaymentReceipt
        {
            PaymentId = paymentId,
            ExternalReference = paymentId,
            Status = "approved",
            StatusDetail = "Pagamento aprovado",
            TotalPaidAmount = 100.00m,
            PaymentMethod = "pix",
            PaymentType = "pix",
            Currency = "BRL",
            DateApproved = DateTime.UtcNow
        };

        return Task.FromResult(receipt);
    }

    /// <inheritdoc />
    public Task<PaymentStatusResult> CheckPaymentStatusAsync(string externalReference)
    {
        // TODO: Implementar integração real com Mercado Pago
        // Por enquanto, retornar status simulado
        var result = new PaymentStatusResult
        {
            IsApproved = true,
            IsPending = false,
            IsRejected = false,
            IsCanceled = false,
            TransactionId = externalReference
        };

        return Task.FromResult(result);
    }
}
