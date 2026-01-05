using FastFood.PayStream.Application.OutputModels;

namespace FastFood.PayStream.Application.Responses;

/// <summary>
/// Resposta retornada pelo endpoint de notificação de pagamento (webhook).
/// Herda de PaymentNotificationOutputModel para evitar duplicação de código.
/// </summary>
public class PaymentNotificationResponse : PaymentNotificationOutputModel
{
}
