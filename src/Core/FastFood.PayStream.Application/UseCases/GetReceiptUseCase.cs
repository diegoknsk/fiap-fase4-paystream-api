using FastFood.PayStream.Application.InputModels;
using FastFood.PayStream.Application.OutputModels;
using FastFood.PayStream.Application.Ports;
using FastFood.PayStream.Application.Presenters;
using FastFood.PayStream.Application.Responses;

namespace FastFood.PayStream.Application.UseCases;

/// <summary>
/// UseCase responsável por obter o comprovante de pagamento do gateway.
/// Busca o Payment, valida que tem ExternalTransactionId e obtém o comprovante do gateway.
/// </summary>
public class GetReceiptUseCase
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IPaymentGateway _realPaymentGateway;
    private readonly IPaymentGateway _fakePaymentGateway;
    private readonly GetReceiptPresenter _presenter;

    /// <summary>
    /// Construtor que recebe as dependências necessárias.
    /// </summary>
    /// <param name="paymentRepository">Repositório para acesso aos dados de pagamento.</param>
    /// <param name="realPaymentGateway">Gateway de pagamento real (Mercado Pago).</param>
    /// <param name="fakePaymentGateway">Gateway de pagamento fake para desenvolvimento/testes.</param>
    /// <param name="presenter">Presenter para transformar OutputModel em Response.</param>
    public GetReceiptUseCase(
        IPaymentRepository paymentRepository,
        IPaymentGateway realPaymentGateway,
        IPaymentGateway fakePaymentGateway,
        GetReceiptPresenter presenter)
    {
        _paymentRepository = paymentRepository;
        _realPaymentGateway = realPaymentGateway;
        _fakePaymentGateway = fakePaymentGateway;
        _presenter = presenter;
    }

    /// <summary>
    /// Executa a obtenção do comprovante de pagamento do gateway.
    /// </summary>
    /// <param name="input">Dados de entrada para obtenção do comprovante.</param>
    /// <returns>Response com os dados do comprovante.</returns>
    /// <exception cref="ArgumentException">Lançada quando os dados de entrada são inválidos.</exception>
    /// <exception cref="ApplicationException">Lançada quando o pagamento não é encontrado ou não tem ExternalTransactionId.</exception>
    public async Task<GetReceiptResponse> ExecuteAsync(GetReceiptInputModel input)
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

        // Validar que Payment tem ExternalTransactionId não nulo/vazio
        if (string.IsNullOrWhiteSpace(payment.ExternalTransactionId))
        {
            throw new ApplicationException($"Pagamento {payment.Id} não possui ExternalTransactionId. O pagamento precisa ser aprovado primeiro.");
        }

        // Obter gateway (real ou fake) baseado em input.FakeCheckout
        var gateway = input.FakeCheckout ? _fakePaymentGateway : _realPaymentGateway;

        // Chamar GetReceiptFromGatewayAsync do gateway passando Payment.ExternalTransactionId
        var receipt = await gateway.GetReceiptFromGatewayAsync(payment.ExternalTransactionId);

        // Quando for fakeCheckout, ajustar o TotalPaidAmount com o valor real do pedido
        // para tornar o recibo fake mais realista
        var totalPaidAmount = receipt.TotalPaidAmount;
        if (input.FakeCheckout)
        {
            totalPaidAmount = payment.TotalAmount;
        }

        // Mapear PaymentReceipt para OutputModel
        var output = new GetReceiptOutputModel
        {
            PaymentId = receipt.PaymentId,
            ExternalReference = receipt.ExternalReference,
            Status = receipt.Status,
            StatusDetail = receipt.StatusDetail,
            TotalPaidAmount = totalPaidAmount,
            PaymentMethod = receipt.PaymentMethod,
            PaymentType = receipt.PaymentType,
            Currency = receipt.Currency,
            DateApproved = receipt.DateApproved
        };

        // Retornar Response via Presenter
        return _presenter.Present(output);
    }
}
