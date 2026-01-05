using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
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
    private readonly GenerateQrCodeUseCase _generateQrCodeUseCase;
    private readonly GetReceiptUseCase _getReceiptUseCase;

    /// <summary>
    /// Construtor do PaymentController.
    /// </summary>
    /// <param name="createPaymentUseCase">UseCase para criação de pagamentos.</param>
    /// <param name="generateQrCodeUseCase">UseCase para geração de QR Code.</param>
    /// <param name="getReceiptUseCase">UseCase para obtenção de comprovante.</param>
    public PaymentController(
        CreatePaymentUseCase createPaymentUseCase,
        GenerateQrCodeUseCase generateQrCodeUseCase,
        GetReceiptUseCase getReceiptUseCase)
    {
        _createPaymentUseCase = createPaymentUseCase;
        _generateQrCodeUseCase = generateQrCodeUseCase;
        _getReceiptUseCase = getReceiptUseCase;
    }

    /// <summary>
    /// Cria um novo pagamento.
    /// </summary>
    /// <param name="input">Dados do pagamento a ser criado.</param>
    /// <returns>Dados do pagamento criado.</returns>
    /// <response code="201">Pagamento criado com sucesso.</response>
    /// <response code="400">Dados inválidos fornecidos.</response>
    [HttpPost("create")]
    [Authorize(AuthenticationSchemes = "CustomerBearer", Policy = "Customer")]
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

    /// <summary>
    /// Gera um QR Code para pagamento.
    /// </summary>
    /// <param name="orderId">ID do pedido relacionado ao pagamento.</param>
    /// <param name="fakeCheckout">Indica se deve usar gateway fake para desenvolvimento/testes (default: false).</param>
    /// <returns>Dados do QR Code gerado.</returns>
    /// <response code="200">QR Code gerado com sucesso.</response>
    /// <response code="400">Dados inválidos fornecidos.</response>
    /// <response code="404">Pagamento não encontrado.</response>
    [HttpPost("generate-qrcode")]
    [Authorize(AuthenticationSchemes = "CustomerBearer", Policy = "Customer")]
    [ProducesResponseType(typeof(ApiResponse<GenerateQrCodeResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<GenerateQrCodeResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<GenerateQrCodeResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GenerateQrCode([FromQuery] Guid orderId, [FromQuery] bool fakeCheckout = false)
    {
        try
        {
            var input = new GenerateQrCodeInputModel
            {
                OrderId = orderId,
                FakeCheckout = fakeCheckout
            };

            var response = await _generateQrCodeUseCase.ExecuteAsync(input);
            return Ok(ApiResponse<GenerateQrCodeResponse>.Ok(response, "QR Code gerado com sucesso."));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ApiResponse<GenerateQrCodeResponse>.Fail(ex.Message));
        }
        catch (ApplicationException ex)
        {
            return NotFound(ApiResponse<GenerateQrCodeResponse>.Fail(ex.Message));
        }
    }

    /// <summary>
    /// Obtém o comprovante de pagamento do gateway.
    /// </summary>
    /// <param name="orderId">ID do pedido relacionado ao pagamento.</param>
    /// <param name="fakeCheckout">Indica se deve usar gateway fake para desenvolvimento/testes (default: false).</param>
    /// <returns>Dados do comprovante de pagamento.</returns>
    /// <response code="200">Comprovante obtido com sucesso.</response>
    /// <response code="400">Dados inválidos fornecidos ou pagamento não possui ExternalTransactionId.</response>
    /// <response code="404">Pagamento não encontrado.</response>
    [HttpGet("receipt-from-gateway")]
    [Authorize(AuthenticationSchemes = "CustomerBearer", Policy = "Customer")]
    [ProducesResponseType(typeof(ApiResponse<GetReceiptResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<GetReceiptResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<GetReceiptResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetReceiptFromGateway([FromQuery] Guid orderId, [FromQuery] bool fakeCheckout = false)
    {
        try
        {
            var input = new GetReceiptInputModel
            {
                OrderId = orderId,
                FakeCheckout = fakeCheckout
            };

            var response = await _getReceiptUseCase.ExecuteAsync(input);
            return Ok(ApiResponse<GetReceiptResponse>.Ok(response, "Comprovante obtido com sucesso."));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ApiResponse<GetReceiptResponse>.Fail(ex.Message));
        }
        catch (ApplicationException ex)
        {
            if (ex.Message.Contains("não encontrado"))
            {
                return NotFound(ApiResponse<GetReceiptResponse>.Fail(ex.Message));
            }
            return BadRequest(ApiResponse<GetReceiptResponse>.Fail(ex.Message));
        }
    }
}
