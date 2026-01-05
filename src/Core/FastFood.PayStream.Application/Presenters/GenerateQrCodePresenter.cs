using FastFood.PayStream.Application.OutputModels;
using FastFood.PayStream.Application.Responses;

namespace FastFood.PayStream.Application.Presenters;

/// <summary>
/// Presenter responsável por transformar o OutputModel em Response para o endpoint de geração de QR Code.
/// </summary>
public class GenerateQrCodePresenter
{
    /// <summary>
    /// Transforma o OutputModel em Response.
    /// Como GenerateQrCodeResponse herda de GenerateQrCodeOutputModel, apenas copia as propriedades.
    /// </summary>
    /// <param name="output">OutputModel com os dados do QR Code gerado.</param>
    /// <returns>Response com os dados do QR Code.</returns>
    public GenerateQrCodeResponse Present(GenerateQrCodeOutputModel output)
    {
        return new GenerateQrCodeResponse
        {
            QrCodeUrl = output.QrCodeUrl,
            PaymentId = output.PaymentId,
            OrderId = output.OrderId
        };
    }
}
