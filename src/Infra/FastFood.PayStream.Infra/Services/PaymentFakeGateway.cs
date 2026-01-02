using FastFood.PayStream.Application.Ports;
using FastFood.PayStream.Application.Ports.Parameters;

namespace FastFood.PayStream.Infra.Services;

/// <summary>
/// Implementação fake do gateway de pagamento para desenvolvimento e testes.
/// Retorna dados simulados sem fazer chamadas reais ao gateway.
/// </summary>
public class PaymentFakeGateway : IPaymentGateway
{
    /// <inheritdoc />
    public Task<string> GenerateQrCodeAsync(string externalReference, string orderCode, List<QrCodeItemModel> items)
    {
        // Retornar URL fake do QR Code
        var qrCodeUrl = $"https://fake-qrcode.example.com/payment/{externalReference}?order={orderCode}";
        return Task.FromResult(qrCodeUrl);
    }

    /// <inheritdoc />
    public Task<PaymentReceipt> GetReceiptFromGatewayAsync(string paymentId)
    {
        // Retornar comprovante fake
        var receipt = new PaymentReceipt
        {
            PaymentId = paymentId,
            ExternalReference = paymentId,
            Status = "approved",
            StatusDetail = "Pagamento aprovado (fake)",
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
        // Retornar status fake (aprovado)
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
