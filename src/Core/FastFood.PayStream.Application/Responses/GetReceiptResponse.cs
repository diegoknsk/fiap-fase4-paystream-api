using FastFood.PayStream.Application.OutputModels;

namespace FastFood.PayStream.Application.Responses;

/// <summary>
/// Resposta da operação de obtenção de comprovante de pagamento do gateway.
/// Herda de GetReceiptOutputModel para evitar duplicação de código.
/// </summary>
public class GetReceiptResponse : GetReceiptOutputModel
{
}
