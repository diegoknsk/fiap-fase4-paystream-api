using FastFood.PayStream.Application.InputModels;
using FastFood.PayStream.Application.OutputModels;
using FastFood.PayStream.Application.Ports;
using FastFood.PayStream.Application.Presenters;
using FastFood.PayStream.Application.Responses;
using FastFood.PayStream.Domain.Common.Enums;
using FastFood.PayStream.Domain.Entities;

namespace FastFood.PayStream.Application.UseCases;

/// <summary>
/// UseCase responsável por processar notificações de pagamento (webhook).
/// Busca o Payment, verifica status no gateway, atualiza o Payment e envia para preparação se aprovado.
/// </summary>
public class PaymentNotificationUseCase
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IPaymentGateway _realPaymentGateway;
    private readonly IPaymentGateway _fakePaymentGateway;
    private readonly PaymentNotificationPresenter _presenter;

    /// <summary>
    /// Construtor que recebe as dependências necessárias.
    /// </summary>
    /// <param name="paymentRepository">Repositório para acesso aos dados de pagamento.</param>
    /// <param name="realPaymentGateway">Gateway de pagamento real (Mercado Pago).</param>
    /// <param name="fakePaymentGateway">Gateway de pagamento fake para desenvolvimento/testes.</param>
    /// <param name="presenter">Presenter para transformar OutputModel em Response.</param>
    public PaymentNotificationUseCase(
        IPaymentRepository paymentRepository,
        IPaymentGateway realPaymentGateway,
        IPaymentGateway fakePaymentGateway,
        PaymentNotificationPresenter presenter)
    {
        _paymentRepository = paymentRepository;
        _realPaymentGateway = realPaymentGateway;
        _fakePaymentGateway = fakePaymentGateway;
        _presenter = presenter;
    }

    /// <summary>
    /// Obtém o gateway apropriado (real ou fake) baseado no parâmetro fakeCheckout.
    /// </summary>
    /// <param name="fakeCheckout">Indica se deve usar gateway fake.</param>
    /// <returns>Gateway de pagamento (real ou fake).</returns>
    private IPaymentGateway GetGateway(bool fakeCheckout)
    {
        return fakeCheckout ? _fakePaymentGateway : _realPaymentGateway;
    }

    /// <summary>
    /// Envia o item pago para preparação (simulado - será substituído por chamada de API no futuro).
    /// </summary>
    /// <param name="orderId">ID do pedido a ser enviado para preparação.</param>
    /// <returns>Task representando a operação assíncrona.</returns>
    private async Task SendOrderToPreparationAsync(Guid orderId)
    {
        // TODO: Substituir esta implementação simulada por uma chamada real à API de preparação
        // Exemplo futuro:
        // await _preparationServiceClient.SendOrderToPreparationAsync(orderId);
        
        // Simulação atual: apenas log (em produção, isso seria substituído por chamada HTTP)
        await Task.CompletedTask;
        
        // Em desenvolvimento, podemos logar para debug
        // Console.WriteLine($"[SIMULADO] Enviando pedido {orderId} para preparação...");
    }

    /// <summary>
    /// Obtém a mensagem descritiva do status do pagamento.
    /// </summary>
    /// <param name="status">Status do pagamento.</param>
    /// <returns>Mensagem descritiva do status.</returns>
    private string GetStatusMessage(EnumPaymentStatus status)
    {
        return status switch
        {
            EnumPaymentStatus.Approved => "Pagamento aprovado.",
            EnumPaymentStatus.Rejected => "Pagamento rejeitado.",
            EnumPaymentStatus.Canceled => "Pagamento cancelado.",
            EnumPaymentStatus.Started => "Pagamento iniciado.",
            EnumPaymentStatus.QrCodeGenerated => "QR Code gerado.",
            EnumPaymentStatus.NotStarted => "Pagamento não iniciado.",
            _ => "Status desconhecido."
        };
    }

    /// <summary>
    /// Executa o processamento da notificação de pagamento.
    /// </summary>
    /// <param name="input">Dados de entrada para processamento da notificação.</param>
    /// <returns>Response com os dados do pagamento atualizado.</returns>
    /// <exception cref="ArgumentException">Lançada quando os dados de entrada são inválidos.</exception>
    /// <exception cref="ApplicationException">Lançada quando o pagamento não é encontrado.</exception>
    public async Task<PaymentNotificationResponse> ExecuteAsync(PaymentNotificationInputModel input)
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

        // Obter gateway (real ou fake) baseado em input.FakeCheckout
        var gateway = GetGateway(input.FakeCheckout);

        // Chamar CheckPaymentStatusAsync do gateway usando Payment.Id como externalReference
        var result = await gateway.CheckPaymentStatusAsync(payment.Id.ToString());

        // Atualizar Payment baseado no PaymentStatusResult
        if (result.IsApproved && !string.IsNullOrWhiteSpace(result.TransactionId))
        {
            // Pagamento aprovado: chamar payment.Approve(transactionId)
            payment.Approve(result.TransactionId);
            
            // Enviar item para preparação (simulado - será substituído por API no futuro)
            await SendOrderToPreparationAsync(payment.OrderId);
        }
        else if (result.IsRejected)
        {
            // Pagamento rejeitado: chamar payment.Reject()
            payment.Reject();
        }
        else if (result.IsCanceled)
        {
            // Pagamento cancelado: chamar payment.Cancel()
            payment.Cancel();
        }
        // Se result.IsPending, manter status atual (ou atualizar para Started se necessário)
        // Por padrão, não fazemos nada se estiver pendente

        // Salvar Payment atualizado via repositório
        await _paymentRepository.UpdateAsync(payment);

        // Criar OutputModel com dados do Payment atualizado
        var output = new PaymentNotificationOutputModel
        {
            PaymentId = payment.Id,
            OrderId = payment.OrderId,
            Status = (int)payment.Status,
            ExternalTransactionId = payment.ExternalTransactionId,
            StatusMessage = GetStatusMessage(payment.Status)
        };

        // Retornar Response via Presenter
        return _presenter.Present(output);
    }
}
