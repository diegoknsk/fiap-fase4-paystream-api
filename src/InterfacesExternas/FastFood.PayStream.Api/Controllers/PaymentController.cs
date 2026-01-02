using Microsoft.AspNetCore.Mvc;
using FastFood.PayStream.Application.Models.Common;
using FastFood.PayStream.Application.InputModels;
using FastFood.PayStream.Application.UseCases;
using FastFood.PayStream.Application.Responses;

namespace FastFood.PayStream.Api.Controllers;

/// <summary>
/// Controller responsável pelos endpoints de pagamento.
/// Expõe operações relacionadas à criação de pagamentos, geração de QR Code e obtenção de comprovantes.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly CreatePaymentUseCase _createPaymentUseCase;

    /// <summary>
    /// Construtor do PaymentController.
    /// </summary>
    /// <param name="createPaymentUseCase">UseCase para criação de pagamentos.</param>
    public PaymentController(CreatePaymentUseCase createPaymentUseCase)
    {
        _createPaymentUseCase = createPaymentUseCase;
    }

    /// <summary>
    /// Cria um novo pagamento.
    /// </summary>
    /// <param name="input">Dados do pagamento a ser criado.</param>
    /// <returns>Dados do pagamento criado.</returns>
    /// <response code="201">Pagamento criado com sucesso.</response>
    /// <response code="400">Dados inválidos fornecidos.</response>
    [HttpPost("create")]
    [ProducesResponseType(typeof(ApiResponse<CreatePaymentResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<CreatePaymentResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreatePaymentInputModel input)
    {
        try
        {
            var response = await _createPaymentUseCase.ExecuteAsync(input);
            return StatusCode(StatusCodes.Status201Created, ApiResponse<CreatePaymentResponse>.Ok(response, "Pagamento criado com sucesso."));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ApiResponse<CreatePaymentResponse>.Fail(ex.Message));
        }
    }

    // Endpoints a serem implementados nas próximas stories:
    // - POST /api/payment/{id}/generate-qrcode - Gerar QR Code
    // - GET /api/payment/{id}/receipt - Obter comprovante
}
