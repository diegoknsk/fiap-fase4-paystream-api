using Microsoft.AspNetCore.Mvc;
using FastFood.PayStream.Application.Models.Common;

namespace FastFood.PayStream.Api.Controllers;

/// <summary>
/// Controller responsável pelos endpoints de pagamento.
/// Expõe operações relacionadas à criação de pagamentos, geração de QR Code e obtenção de comprovantes.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    /// <summary>
    /// Construtor do PaymentController.
    /// Os UseCases serão injetados via construtor nas próximas stories.
    /// </summary>
    public PaymentController()
    {
        // UseCases serão injetados aqui nas próximas stories
    }

    // Endpoints serão implementados nas próximas stories:
    // - POST /api/payment - Criar pagamento
    // - POST /api/payment/{id}/generate-qrcode - Gerar QR Code
    // - GET /api/payment/{id}/receipt - Obter comprovante
}
