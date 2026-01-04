using FastFood.PayStream.Application.OutputModels;

namespace FastFood.PayStream.Application.Responses;

/// <summary>
/// Resposta retornada pelo endpoint de criação de pagamento.
/// Herda de CreatePaymentOutputModel para evitar duplicação de código.
/// </summary>
public class CreatePaymentResponse : CreatePaymentOutputModel
{
}
