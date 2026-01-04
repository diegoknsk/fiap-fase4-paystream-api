using System.Text.Json;
using FastFood.PayStream.Application.InputModels;
using FastFood.PayStream.Application.OutputModels;
using FastFood.PayStream.Application.Ports;
using FastFood.PayStream.Application.Ports.Parameters;
using FastFood.PayStream.Application.Presenters;
using FastFood.PayStream.Application.Responses;
using FastFood.PayStream.Domain.Entities;

namespace FastFood.PayStream.Application.UseCases;

/// <summary>
/// UseCase responsável por gerar QR Code para um pagamento.
/// Busca o Payment, deserializa OrderSnapshot, gera QR Code via gateway e atualiza o Payment.
/// </summary>
public class GenerateQrCodeUseCase
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IPaymentGateway _realPaymentGateway;
    private readonly IPaymentGateway _fakePaymentGateway;
    private readonly GenerateQrCodePresenter _presenter;

    /// <summary>
    /// Construtor que recebe as dependências necessárias.
    /// </summary>
    /// <param name="paymentRepository">Repositório para acesso aos dados de pagamento.</param>
    /// <param name="realPaymentGateway">Gateway de pagamento real (Mercado Pago).</param>
    /// <param name="fakePaymentGateway">Gateway de pagamento fake para desenvolvimento/testes.</param>
    /// <param name="presenter">Presenter para transformar OutputModel em Response.</param>
    public GenerateQrCodeUseCase(
        IPaymentRepository paymentRepository,
        IPaymentGateway realPaymentGateway,
        IPaymentGateway fakePaymentGateway,
        GenerateQrCodePresenter presenter)
    {
        _paymentRepository = paymentRepository;
        _realPaymentGateway = realPaymentGateway;
        _fakePaymentGateway = fakePaymentGateway;
        _presenter = presenter;
    }

    /// <summary>
    /// Executa a geração de QR Code para um pagamento.
    /// </summary>
    /// <param name="input">Dados de entrada para geração do QR Code.</param>
    /// <returns>Response com os dados do QR Code gerado.</returns>
    /// <exception cref="ArgumentException">Lançada quando os dados de entrada são inválidos.</exception>
    /// <exception cref="ApplicationException">Lançada quando o pagamento não é encontrado ou não tem OrderSnapshot válido.</exception>
    public async Task<GenerateQrCodeResponse> ExecuteAsync(GenerateQrCodeInputModel input)
    {
        // Validações
        if (input.OrderId == Guid.Empty)
        {
            throw new ArgumentException("OrderId não pode ser vazio.", nameof(input));
        }

        // Buscar Payment por OrderId
        var payment = await _paymentRepository.GetByOrderIdAsync(input.OrderId);
        if (payment == null)
        {
            throw new ApplicationException($"Pagamento não encontrado para o OrderId: {input.OrderId}");
        }

        // Validar que Payment tem OrderSnapshot não nulo/vazio
        if (string.IsNullOrWhiteSpace(payment.OrderSnapshot))
        {
            throw new ApplicationException($"Pagamento {payment.Id} não possui OrderSnapshot válido.");
        }

        // Deserializar OrderSnapshot (JSON) para obter dados do pedido
        OrderSnapshotDto? orderSnapshot;
        try
        {
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                MaxDepth = 64, // Limitar profundidade para prevenir stack overflow
                AllowTrailingCommas = false,
                ReadCommentHandling = JsonCommentHandling.Skip
            };
            orderSnapshot = JsonSerializer.Deserialize<OrderSnapshotDto>(payment.OrderSnapshot, jsonOptions);
        }
        catch (JsonException ex)
        {
            throw new ApplicationException($"Erro ao deserializar OrderSnapshot: {ex.Message}");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            throw new ApplicationException($"Erro ao deserializar OrderSnapshot: profundidade máxima excedida. {ex.Message}");
        }

        if (orderSnapshot == null)
        {
            throw new ApplicationException("OrderSnapshot deserializado é nulo.");
        }

        // Criar lista de QrCodeItemModel a partir dos produtos do OrderSnapshot
        var items = new List<QrCodeItemModel>();
        if (orderSnapshot.OrderedProducts != null && orderSnapshot.OrderedProducts.Count > 0)
        {
            foreach (var product in orderSnapshot.OrderedProducts)
            {
                items.Add(new QrCodeItemModel
                {
                    Title = product.Name ?? string.Empty,
                    Description = product.Description ?? string.Empty,
                    UnitPrice = product.UnitPrice,
                    Quantity = product.Quantity,
                    UnitMeasure = product.UnitMeasure ?? "unit"
                });
            }
        }

        // Obter gateway (real ou fake) baseado em input.FakeCheckout
        var gateway = input.FakeCheckout ? _fakePaymentGateway : _realPaymentGateway;

        // Chamar GenerateQrCodeAsync do gateway
        var qrCodeUrl = await gateway.GenerateQrCodeAsync(
            payment.Id.ToString(),
            orderSnapshot.Code ?? string.Empty,
            items);

        // Atualizar Payment: chamar payment.GenerateQrCode(qrCodeUrl)
        payment.GenerateQrCode(qrCodeUrl);

        // Salvar Payment atualizado via repositório
        await _paymentRepository.UpdateAsync(payment);

        // Criar OutputModel
        var output = new GenerateQrCodeOutputModel
        {
            QrCodeUrl = qrCodeUrl,
            PaymentId = payment.Id,
            OrderId = payment.OrderId
        };

        // Retornar Response via Presenter
        return _presenter.Present(output);
    }
}

/// <summary>
/// DTO para deserialização do OrderSnapshot (JSON).
/// </summary>
internal class OrderSnapshotDto
{
    /// <summary>
    /// Código do pedido.
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Lista de produtos do pedido.
    /// </summary>
    public List<OrderedProductDto>? OrderedProducts { get; set; }
}

/// <summary>
/// DTO para deserialização de um produto do pedido.
/// </summary>
internal class OrderedProductDto
{
    /// <summary>
    /// Nome do produto.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Descrição do produto.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Preço unitário do produto.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Quantidade do produto.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Unidade de medida do produto.
    /// </summary>
    public string? UnitMeasure { get; set; }
}
