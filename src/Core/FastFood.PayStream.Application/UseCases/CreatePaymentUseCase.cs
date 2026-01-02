using FastFood.PayStream.Application.InputModels;
using FastFood.PayStream.Application.OutputModels;
using FastFood.PayStream.Application.Presenters;
using FastFood.PayStream.Application.Ports;
using FastFood.PayStream.Application.Responses;
using FastFood.PayStream.Domain.Entities;
using FastFood.PayStream.Domain.Common.Enums;

namespace FastFood.PayStream.Application.UseCases;

/// <summary>
/// UseCase responsável por criar um novo pagamento.
/// Valida os dados de entrada, cria a entidade de domínio Payment com status NotStarted,
/// persiste no repositório e retorna o Response através do Presenter.
/// </summary>
public class CreatePaymentUseCase
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly CreatePaymentPresenter _presenter;

    /// <summary>
    /// Construtor que recebe as dependências necessárias.
    /// </summary>
    /// <param name="paymentRepository">Repositório para persistência de pagamentos.</param>
    /// <param name="presenter">Presenter para transformar OutputModel em Response.</param>
    public CreatePaymentUseCase(IPaymentRepository paymentRepository, CreatePaymentPresenter presenter)
    {
        _paymentRepository = paymentRepository;
        _presenter = presenter;
    }

    /// <summary>
    /// Executa a criação de um novo pagamento.
    /// </summary>
    /// <param name="input">Dados de entrada para criação do pagamento.</param>
    /// <returns>Response com os dados do pagamento criado.</returns>
    /// <exception cref="ArgumentException">Lançada quando os dados de entrada são inválidos.</exception>
    public async Task<CreatePaymentResponse> ExecuteAsync(CreatePaymentInputModel input)
    {
        // Validações
        if (input.OrderId == Guid.Empty)
        {
            throw new ArgumentException("OrderId não pode ser vazio.", nameof(input));
        }

        if (input.TotalAmount <= 0)
        {
            throw new ArgumentException("TotalAmount deve ser maior que zero.", nameof(input));
        }

        if (string.IsNullOrWhiteSpace(input.OrderSnapshot))
        {
            throw new ArgumentException("OrderSnapshot não pode ser nulo ou vazio.", nameof(input));
        }

        // Criar entidade Payment de domínio
        var payment = new Payment(input.OrderId, input.TotalAmount, input.OrderSnapshot);

        // Salvar via repositório
        var savedPayment = await _paymentRepository.AddAsync(payment);

        // Criar OutputModel
        var output = new CreatePaymentOutputModel
        {
            PaymentId = savedPayment.Id,
            OrderId = savedPayment.OrderId,
            Status = (int)savedPayment.Status,
            TotalAmount = savedPayment.TotalAmount,
            CreatedAt = savedPayment.CreatedAt
        };

        // Retornar Response via Presenter
        return _presenter.Present(output);
    }
}
