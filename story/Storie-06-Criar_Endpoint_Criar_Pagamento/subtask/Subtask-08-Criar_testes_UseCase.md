# Subtask 08: Criar testes unitários do UseCase

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Criar testes unitários completos para o CreatePaymentUseCase, validando todos os cenários: sucesso, validações de entrada e comportamento do repositório.

## Passos de implementação
- [ ] Verificar se o projeto de testes `FastFood.PayStream.Tests.Unit` existe
- [ ] Criar diretório `UseCases/` no projeto de testes se não existir
- [ ] Criar arquivo `CreatePaymentUseCaseTests.cs` no diretório UseCases
- [ ] Adicionar usings necessários:
  - `Moq` (ou framework de mock escolhido)
  - `FastFood.PayStream.Application.InputModels`
  - `FastFood.PayStream.Application.UseCases`
  - `FastFood.PayStream.Application.Ports`
  - `FastFood.PayStream.Domain.Entities`
  - `FastFood.PayStream.Domain.Common.Enums`
- [ ] Criar classe de teste `CreatePaymentUseCaseTests`
- [ ] Criar testes para validações:
  - Teste: ExecuteAsync_WhenOrderIdIsEmpty_ShouldThrowArgumentException
  - Teste: ExecuteAsync_WhenTotalAmountIsZero_ShouldThrowArgumentException
  - Teste: ExecuteAsync_WhenTotalAmountIsNegative_ShouldThrowArgumentException
  - Teste: ExecuteAsync_WhenOrderSnapshotIsNull_ShouldThrowArgumentException
  - Teste: ExecuteAsync_WhenOrderSnapshotIsEmpty_ShouldThrowArgumentException
- [ ] Criar testes para fluxo de sucesso:
  - Teste: ExecuteAsync_WithValidInput_ShouldCreatePayment
  - Teste: ExecuteAsync_WithValidInput_ShouldCallRepositoryAddAsync
  - Teste: ExecuteAsync_WithValidInput_ShouldReturnResponseWithCorrectData
  - Teste: ExecuteAsync_WithValidInput_ShouldSetPaymentStatusToNotStarted
- [ ] Usar mocks para `IPaymentRepository` e `CreatePaymentPresenter`
- [ ] Validar que o Payment criado tem as propriedades corretas

## Como testar
- Executar `dotnet test` no projeto de testes (todos os testes devem passar)
- Verificar cobertura de código do UseCase (deve estar acima de 90%)
- Validar que todos os cenários estão cobertos
- Verificar que os testes são independentes

## Critérios de aceite
- [ ] Arquivo `CreatePaymentUseCaseTests.cs` criado em `src/tests/FastFood.PayStream.Tests.Unit/UseCases/`
- [ ] Mínimo de 8 testes unitários criados
- [ ] Todos os testes passam (`dotnet test`)
- [ ] Testes cobrem validações de entrada (OrderId, TotalAmount, OrderSnapshot)
- [ ] Testes cobrem fluxo de sucesso
- [ ] Testes validam interação com repositório
- [ ] Testes validam que Payment é criado com status NotStarted
- [ ] Mocks usados corretamente para dependências
- [ ] Cobertura de código do UseCase acima de 90%
- [ ] Testes são independentes e podem ser executados em qualquer ordem
