# Subtask 03: Criar Presenter e UseCase GenerateQrCodeUseCase

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Criar o Presenter e o UseCase GenerateQrCodeUseCase que busca o Payment, deserializa OrderSnapshot, gera QR Code via gateway e atualiza o Payment.

## Passos de implementação
- [ ] Criar arquivo `GenerateQrCodePresenter.cs` em `src/Core/FastFood.PayStream.Application/Presenters/`
- [ ] Implementar método `Present(GenerateQrCodeOutputModel output)` retornando `GenerateQrCodeResponse`
- [ ] Criar arquivo `GenerateQrCodeUseCase.cs` em `src/Core/FastFood.PayStream.Application/UseCases/`
- [ ] Adicionar dependências no construtor:
  - `IPaymentRepository _paymentRepository`
  - `IPaymentGateway _realPaymentGateway`
  - `IPaymentGateway _fakePaymentGateway`
  - `GenerateQrCodePresenter _presenter`
- [ ] Implementar método `ExecuteAsync(GenerateQrCodeInputModel input)`:
  - Buscar Payment por OrderId via repositório
  - Validar que Payment existe (lançar exceção se não existir)
  - Validar que Payment tem OrderSnapshot não nulo/vazio
  - Deserializar OrderSnapshot (JSON) para obter dados do pedido:
    - Criar classe DTO para OrderSnapshot (ex: OrderSnapshotDto com Code, OrderedProducts, etc.)
    - Usar System.Text.Json para deserializar
  - Criar lista de QrCodeItemModel a partir dos produtos do OrderSnapshot
  - Obter gateway (real ou fake) baseado em input.FakeCheckout
  - Chamar `GenerateQrCodeAsync` do gateway passando Payment.Id como externalReference, código do pedido e itens
  - Atualizar Payment: chamar `payment.GenerateQrCode(qrCodeUrl)`
  - Salvar Payment atualizado via repositório
  - Criar OutputModel e retornar via Presenter
- [ ] Adicionar tratamento de exceções apropriado
- [ ] Adicionar comentários XML

## Como testar
- Executar `dotnet build` no projeto Application (deve compilar sem erros)
- Criar testes unitários validando deserialização de JSON
- Testar fluxo completo com mock do gateway

## Critérios de aceite
- [ ] `GenerateQrCodePresenter` criado e funcionando
- [ ] `GenerateQrCodeUseCase` criado
- [ ] UseCase busca Payment por OrderId
- [ ] UseCase deserializa OrderSnapshot (JSON) corretamente
- [ ] UseCase cria QrCodeItemModel a partir dos dados do OrderSnapshot
- [ ] UseCase seleciona gateway correto (real ou fake)
- [ ] UseCase chama GenerateQrCodeAsync do gateway
- [ ] UseCase atualiza Payment com QrCodeUrl
- [ ] UseCase salva Payment atualizado
- [ ] UseCase retorna Response via Presenter
- [ ] Comentários XML adicionados
- [ ] Projeto Application compila sem erros
