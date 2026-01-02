# Subtask 05: Criar UseCase CreatePaymentUseCase

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Criar o UseCase que orquestra a criação de pagamento, validando dados de entrada, criando a entidade de domínio Payment com status NotStarted, persistindo no repositório e retornando o OutputModel.

## Passos de implementação
- [ ] Criar diretório `src/Core/FastFood.PayStream.Application/UseCases/` se não existir
- [ ] Criar arquivo `CreatePaymentUseCase.cs` no diretório UseCases
- [ ] Definir namespace `FastFood.PayStream.Application.UseCases`
- [ ] Adicionar usings necessários:
  - `FastFood.PayStream.Application.InputModels`
  - `FastFood.PayStream.Application.OutputModels`
  - `FastFood.PayStream.Application.Presenters`
  - `FastFood.PayStream.Application.Ports`
  - `FastFood.PayStream.Domain.Entities`
  - `FastFood.PayStream.Domain.Common.Enums`
- [ ] Criar classe pública `CreatePaymentUseCase`
- [ ] Adicionar campo privado readonly `IPaymentRepository _paymentRepository`
- [ ] Adicionar campo privado readonly `CreatePaymentPresenter _presenter`
- [ ] Criar construtor recebendo `IPaymentRepository` e `CreatePaymentPresenter`
- [ ] Criar método público assíncrono `ExecuteAsync(CreatePaymentInputModel input)` retornando `CreatePaymentResponse`:
  - Validar que OrderId não é Guid.Empty (lançar ArgumentException se for)
  - Validar que TotalAmount é maior que zero (lançar ArgumentException se não for)
  - Validar que OrderSnapshot não é nulo/vazio (lançar ArgumentException se for)
  - Criar entidade Payment de domínio usando construtor: `new Payment(input.OrderId, input.TotalAmount, input.OrderSnapshot)`
  - Salvar Payment via repositório: `await _paymentRepository.AddAsync(payment)`
  - Criar OutputModel com dados do Payment salvo
  - Retornar Response usando Presenter: `return _presenter.Present(output)`
- [ ] Adicionar comentários XML para documentação

## Como testar
- Executar `dotnet build` no projeto Application (deve compilar sem erros)
- Verificar que o UseCase pode ser instanciado
- Validar que as validações funcionam corretamente
- Testar fluxo completo de criação de pagamento

## Critérios de aceite
- [ ] Arquivo `CreatePaymentUseCase.cs` criado em `src/Core/FastFood.PayStream.Application/UseCases/`
- [ ] Classe `CreatePaymentUseCase` criada
- [ ] Construtor recebe `IPaymentRepository` e `CreatePaymentPresenter`
- [ ] Método `ExecuteAsync(CreatePaymentInputModel input)` implementado
- [ ] Validação de OrderId != Guid.Empty implementada
- [ ] Validação de TotalAmount > 0 implementada
- [ ] Validação de OrderSnapshot não nulo/vazio implementada
- [ ] Entidade Payment criada usando construtor com OrderId, TotalAmount e OrderSnapshot
- [ ] Payment salvo via repositório
- [ ] OutputModel criado com dados do Payment
- [ ] Response retornado via Presenter
- [ ] Comentários XML adicionados
- [ ] Projeto Application compila sem erros
