using FastFood.PayStream.Application.OutputModels;
using FastFood.PayStream.Application.Responses;

namespace FastFood.PayStream.Application.Presenters;

/// <summary>
/// Presenter responsável por transformar o OutputModel em Response para o endpoint de notificação de pagamento.
/// </summary>
public class PaymentNotificationPresenter
{
    /// <summary>
    /// Transforma o OutputModel em Response.
    /// Como PaymentNotificationResponse herda de PaymentNotificationOutputModel, apenas copia as propriedades.
    /// </summary>
    /// <param name="output">OutputModel com os dados do pagamento atualizado.</param>
    /// <returns>Response com os dados do pagamento.</returns>
    public PaymentNotificationResponse Present(PaymentNotificationOutputModel output)
    {
        return new PaymentNotificationResponse
        {
            PaymentId = output.PaymentId,
            OrderId = output.OrderId,
            Status = output.Status,
            ExternalTransactionId = output.ExternalTransactionId,
            StatusMessage = output.StatusMessage
        };
    }
}
