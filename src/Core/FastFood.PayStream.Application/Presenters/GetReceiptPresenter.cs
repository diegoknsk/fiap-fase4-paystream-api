using FastFood.PayStream.Application.OutputModels;
using FastFood.PayStream.Application.Responses;

namespace FastFood.PayStream.Application.Presenters;

/// <summary>
/// Presenter responsável por transformar o OutputModel em Response para o endpoint de obtenção de comprovante.
/// </summary>
public class GetReceiptPresenter
{
    /// <summary>
    /// Transforma o OutputModel em Response.
    /// </summary>
    /// <param name="output">OutputModel com os dados do comprovante.</param>
    /// <returns>Response com os dados do comprovante.</returns>
    public GetReceiptResponse Present(GetReceiptOutputModel output)
    {
        return new GetReceiptResponse
        {
            PaymentId = output.PaymentId,
            ExternalReference = output.ExternalReference,
            Status = output.Status,
            StatusDetail = output.StatusDetail,
            TotalPaidAmount = output.TotalPaidAmount,
            PaymentMethod = output.PaymentMethod,
            PaymentType = output.PaymentType,
            Currency = output.Currency,
            DateApproved = output.DateApproved
        };
    }
}
