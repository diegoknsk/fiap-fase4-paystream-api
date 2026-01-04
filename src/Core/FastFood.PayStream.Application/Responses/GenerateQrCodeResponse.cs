using FastFood.PayStream.Application.OutputModels;

namespace FastFood.PayStream.Application.Responses;

/// <summary>
/// Resposta da operação de geração de QR Code de pagamento.
/// Herda de GenerateQrCodeOutputModel para evitar duplicação de código.
/// </summary>
public class GenerateQrCodeResponse : GenerateQrCodeOutputModel
{
}
