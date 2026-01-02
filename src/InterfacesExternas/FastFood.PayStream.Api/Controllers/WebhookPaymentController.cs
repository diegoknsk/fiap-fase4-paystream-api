using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FastFood.PayStream.Application.Models.Common;

namespace FastFood.PayStream.Api.Controllers;

/// <summary>
/// Controller responsável por receber notificações de webhook do gateway de pagamento.
/// Este controller geralmente permite acesso anônimo para receber notificações externas.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class WebhookPaymentController : ControllerBase
{
    /// <summary>
    /// Construtor do WebhookPaymentController.
    /// Os UseCases serão injetados via construtor nas próximas stories.
    /// </summary>
    public WebhookPaymentController()
    {
        // UseCases serão injetados aqui nas próximas stories
    }

    // Endpoints serão implementados nas próximas stories:
    // - POST /api/webhookpayment - Receber notificação de webhook do gateway
}
