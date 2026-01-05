# Subtask 03: Criar UseCase PaymentNotificationUseCase

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Criar o UseCase que orquestra a notificação de pagamento, buscando o Payment, verificando status no gateway e atualizando o Payment conforme o resultado.

## Passos de implementação
- [ ] Criar arquivo `PaymentNotificationUseCase.cs` em `src/Core/FastFood.PayStream.Application/UseCases/`
- [ ] Definir namespace `FastFood.PayStream.Application.UseCases`
- [ ] Adicionar usings necessários:
  - `FastFood.PayStream.Application.InputModels`
  - `FastFood.PayStream.Application.OutputModels`
  - `FastFood.PayStream.Application.Presenters`
  - `FastFood.PayStream.Application.Ports`
  - `FastFood.PayStream.Domain.Common.Enums`
- [ ] Criar classe pública `PaymentNotificationUseCase`
- [ ] Adicionar campos privados readonly:
  - `IPaymentRepository _paymentRepository`
  - `IPaymentGateway _realPaymentGateway`
  - `IPaymentGateway _fakePaymentGateway`
  - `PaymentNotificationPresenter _presenter`
- [ ] Criar construtor recebendo todas as dependências
- [ ] Criar método privado `GetGateway(bool fakeCheckout)` retornando `IPaymentGateway`:
  - Retornar `_fakePaymentGateway` se fakeCheckout for true
  - Retornar `_realPaymentGateway` caso contrário
- [ ] Implementar método público assíncrono `ExecuteAsync(PaymentNotificationInputModel input)` retornando `PaymentNotificationResponse`:
  - Buscar Payment por OrderId via repositório: `await _paymentRepository.GetByOrderIdAsync(input.OrderId)`
  - Validar que Payment existe (lançar ApplicationException se não existir)
  - Obter gateway usando `GetGateway(input.FakeCheckout)`
  - Chamar `CheckPaymentStatusAsync` do gateway passando `Payment.Id.ToString()` como externalReference
  - Atualizar Payment baseado no PaymentStatusResult:
    - Se `result.IsApproved` e `result.TransactionId` não é nulo:
      - Chamar `payment.Approve(result.TransactionId)`
    - Se `result.IsRejected`:
      - Chamar `payment.Reject()`
    - Se `result.IsCanceled`:
      - Chamar `payment.Cancel()`
    - Se `result.IsPending`:
      - Manter status atual (ou atualizar para Started se necessário)
  - Salvar Payment atualizado via repositório: `await _paymentRepository.UpdateAsync(payment)`
  - Criar OutputModel com dados do Payment atualizado
  - Determinar StatusMessage baseado no status (ex: "Pagamento aprovado", "Pagamento rejeitado", etc.)
  - Retornar Response via Presenter
- [ ] Adicionar tratamento de exceções apropriado
- [ ] Adicionar comentários XML para documentação

## Como testar
- Executar `dotnet build` no projeto Application (deve compilar sem erros)
- Criar testes unitários validando cada cenário (aprovado, rejeitado, cancelado, pendente)
- Testar fluxo completo com mock do gateway

## Critérios de aceite
- [ ] Arquivo `PaymentNotificationUseCase.cs` criado
- [ ] Classe `PaymentNotificationUseCase` criada
- [ ] Construtor recebe todas as dependências
- [ ] Método `GetGateway(bool fakeCheckout)` implementado
- [ ] Método `ExecuteAsync` implementado
- [ ] UseCase busca Payment por OrderId
- [ ] UseCase valida que Payment existe
- [ ] UseCase seleciona gateway correto (real ou fake)
- [ ] UseCase chama CheckPaymentStatusAsync do gateway
- [ ] UseCase atualiza Payment conforme resultado (Approve, Reject, Cancel)
- [ ] UseCase salva Payment atualizado
- [ ] UseCase retorna Response via Presenter
- [ ] Comentários XML adicionados
- [ ] Projeto Application compila sem erros
