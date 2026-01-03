using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FastFood.PayStream.Application.Models.Common;
using FastFood.PayStream.Application.InputModels;
using FastFood.PayStream.Application.UseCases;
using FastFood.PayStream.Application.Responses;

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
    private readonly PaymentNotificationUseCase _paymentNotificationUseCase;

    /// <summary>
    /// Construtor do WebhookPaymentController.
    /// </summary>
    /// <param name="paymentNotificationUseCase">UseCase para processamento de notificações de pagamento.</param>
    public WebhookPaymentController(PaymentNotificationUseCase paymentNotificationUseCase)
    {
        _paymentNotificationUseCase = paymentNotificationUseCase;
    }

    /// <summary>
    /// Recebe notificação de pagamento do gateway externo (webhook).
    /// Este endpoint é público (AllowAnonymous) para permitir chamadas do gateway de pagamento.
    /// Verifica o status do pagamento no gateway, atualiza o Payment no banco de dados
    /// e envia o item para preparação se o pagamento for aprovado.
    /// </summary>
    /// <param name="orderId">ID do pedido relacionado ao pagamento.</param>
    /// <param name="fakeCheckout">Indica se deve usar gateway fake para desenvolvimento/testes (default: false).</param>
    /// <returns>Dados do pagamento atualizado.</returns>
    /// <response code="200">Notificação processada com sucesso.</response>
    /// <response code="400">Dados inválidos fornecidos.</response>
    /// <response code="404">Pagamento não encontrado.</response>
    [HttpPost("payment-notification")]
    [ProducesResponseType(typeof(ApiResponse<PaymentNotificationResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<PaymentNotificationResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<PaymentNotificationResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PaymentNotification([FromQuery] Guid orderId, [FromQuery] bool fakeCheckout = false)
    {
        try
        {
            var input = new PaymentNotificationInputModel
            {
                OrderId = orderId,
                FakeCheckout = fakeCheckout
            };

            var response = await _paymentNotificationUseCase.ExecuteAsync(input);
            return Ok(ApiResponse<PaymentNotificationResponse>.Ok(response, "Notificação de pagamento processada com sucesso."));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ApiResponse<PaymentNotificationResponse>.Fail(ex.Message));
        }
        catch (ApplicationException ex)
        {
            if (ex.Message.Contains("não encontrado"))
            {
                return NotFound(ApiResponse<PaymentNotificationResponse>.Fail(ex.Message));
            }
            return BadRequest(ApiResponse<PaymentNotificationResponse>.Fail(ex.Message));
        }
    }
}
