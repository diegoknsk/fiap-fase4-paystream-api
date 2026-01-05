# Subtask 05: Criar Presenter e UseCase GetReceiptUseCase

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Criar o Presenter e o UseCase GetReceiptUseCase que busca o Payment, valida que tem ExternalTransactionId e obtém o comprovante do gateway.

## Passos de implementação
- [ ] Criar arquivo `GetReceiptPresenter.cs` em `src/Core/FastFood.PayStream.Application/Presenters/`
- [ ] Implementar método `Present(GetReceiptOutputModel output)` retornando `GetReceiptResponse`
- [ ] Criar arquivo `GetReceiptUseCase.cs` em `src/Core/FastFood.PayStream.Application/UseCases/`
- [ ] Adicionar dependências no construtor:
  - `IPaymentRepository _paymentRepository`
  - `IPaymentGateway _realPaymentGateway`
  - `IPaymentGateway _fakePaymentGateway`
  - `GetReceiptPresenter _presenter`
- [ ] Implementar método `ExecuteAsync(GetReceiptInputModel input)`:
  - Buscar Payment por OrderId via repositório
  - Validar que Payment existe (lançar exceção se não existir)
  - Validar que Payment tem ExternalTransactionId não nulo/vazio (lançar exceção se não tiver)
  - Obter gateway (real ou fake) baseado em input.FakeCheckout
  - Chamar `GetReceiptFromGatewayAsync` do gateway passando Payment.ExternalTransactionId
  - Mapear PaymentReceipt para OutputModel
  - Retornar Response via Presenter
- [ ] Adicionar tratamento de exceções apropriado
- [ ] Adicionar comentários XML

## Como testar
- Executar `dotnet build` no projeto Application (deve compilar sem erros)
- Criar testes unitários validando validações
- Testar fluxo completo com mock do gateway

## Critérios de aceite
- [ ] `GetReceiptPresenter` criado e funcionando
- [ ] `GetReceiptUseCase` criado
- [ ] UseCase busca Payment por OrderId
- [ ] UseCase valida que Payment existe
- [ ] UseCase valida que Payment tem ExternalTransactionId
- [ ] UseCase seleciona gateway correto (real ou fake)
- [ ] UseCase chama GetReceiptFromGatewayAsync do gateway
- [ ] UseCase mapeia PaymentReceipt para OutputModel
- [ ] UseCase retorna Response via Presenter
- [ ] Comentários XML adicionados
- [ ] Projeto Application compila sem erros
