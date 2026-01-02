using FastFood.PayStream.Application.OutputModels;
using FastFood.PayStream.Application.Responses;

namespace FastFood.PayStream.Application.Presenters;

/// <summary>
/// Presenter responsável por transformar o OutputModel em Response para o endpoint de criação de pagamento.
/// </summary>
public class CreatePaymentPresenter
{
    /// <summary>
    /// Transforma o OutputModel em Response.
    /// </summary>
    /// <param name="output">OutputModel com os dados do pagamento criado.</param>
    /// <returns>Response com os dados do pagamento.</returns>
    public CreatePaymentResponse Present(CreatePaymentOutputModel output)
    {
        return new CreatePaymentResponse
        {
            PaymentId = output.PaymentId,
            OrderId = output.OrderId,
            Status = output.Status,
            TotalAmount = output.TotalAmount,
            CreatedAt = output.CreatedAt
        };
    }
}
