# Subtask 01: Criar modelos para PaymentNotification (Input, Output, Response)

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Criar os modelos InputModel, OutputModel e Response para o endpoint de notificação de pagamento (webhook), seguindo o padrão estabelecido.

## Passos de implementação
- [ ] Criar arquivo `PaymentNotificationInputModel.cs` em `src/Core/FastFood.PayStream.Application/InputModels/`
- [ ] Definir classe pública `PaymentNotificationInputModel` com:
  - `OrderId` (Guid) - ID do pedido
  - `FakeCheckout` (bool) - Indica se deve usar gateway fake (default: false)
- [ ] Criar arquivo `PaymentNotificationOutputModel.cs` em `src/Core/FastFood.PayStream.Application/OutputModels/`
- [ ] Definir classe pública `PaymentNotificationOutputModel` com:
  - `PaymentId` (Guid) - ID do pagamento
  - `OrderId` (Guid) - ID do pedido
  - `Status` (int) - Status atualizado do pagamento (representa EnumPaymentStatus)
  - `ExternalTransactionId` (string?) - ID da transação no gateway externo (se aprovado)
  - `StatusMessage` (string) - Mensagem descritiva do status
- [ ] Criar arquivo `PaymentNotificationResponse.cs` em `src/Core/FastFood.PayStream.Application/Responses/`
- [ ] Definir classe pública `PaymentNotificationResponse` com mesma estrutura do OutputModel
- [ ] Adicionar comentários XML para documentação de todas as classes

## Como testar
- Executar `dotnet build` no projeto Application (deve compilar sem erros)
- Verificar que todas as classes podem ser instanciadas
- Validar que as estruturas estão corretas

## Critérios de aceite
- [ ] `PaymentNotificationInputModel` criado com OrderId e FakeCheckout
- [ ] `PaymentNotificationOutputModel` criado com PaymentId, OrderId, Status, ExternalTransactionId e StatusMessage
- [ ] `PaymentNotificationResponse` criado com mesma estrutura do OutputModel
- [ ] Comentários XML adicionados
- [ ] Projeto Application compila sem erros
