# Storie-08: Trazer Webhook (WebhookPaymentController)

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Como desenvolvedor, quero trazer o endpoint de webhook do monolito para o microserviço de pagamento, adaptando para receber notificações do gateway de pagamento (Mercado Pago) e atualizar o status do pagamento no banco de dados, suportando tanto o fluxo padrão quanto o fake para desenvolvimento/testes.

## Objetivo
Criar o UseCase, InputModel, OutputModel, Response, Presenter e endpoint no WebhookPaymentController para receber notificações de pagamento do gateway externo, verificar o status do pagamento, atualizar o Payment no banco de dados e retornar resposta apropriada. O webhook deve suportar tanto o fluxo real (Mercado Pago) quanto o fake para desenvolvimento/testes.

## Escopo Técnico
- Tecnologias: .NET 8, ASP.NET Core, Clean Architecture
- Arquivos afetados:
  - `src/Core/FastFood.PayStream.Application/InputModels/PaymentNotificationInputModel.cs`
  - `src/Core/FastFood.PayStream.Application/OutputModels/PaymentNotificationOutputModel.cs`
  - `src/Core/FastFood.PayStream.Application/Responses/PaymentNotificationResponse.cs`
  - `src/Core/FastFood.PayStream.Application/Presenters/PaymentNotificationPresenter.cs`
  - `src/Core/FastFood.PayStream.Application/UseCases/PaymentNotificationUseCase.cs`
  - `src/InterfacesExternas/FastFood.PayStream.Api/Controllers/WebhookPaymentController.cs`
  - `src/InterfacesExternas/FastFood.PayStream.Api/Program.cs` (registro de DI)
- Comportamento do webhook:
  - Recebe OrderId e fakeCheckout como parâmetros (query string)
  - Busca Payment por OrderId
  - Verifica status do pagamento no gateway usando CheckPaymentStatusAsync
  - Atualiza Payment com status e ExternalTransactionId se aprovado
  - Salva Payment atualizado no repositório
  - Retorna resposta com status atualizado
- Nota: O webhook pode ser chamado pelo gateway externo ou manualmente para testes

## Subtasks

- [ ] [Subtask 01: Criar modelos para PaymentNotification (Input, Output, Response)](./subtask/Subtask-01-Criar_modelos_PaymentNotification.md)
- [ ] [Subtask 02: Criar Presenter PaymentNotificationPresenter](./subtask/Subtask-02-Criar_Presenter_PaymentNotification.md)
- [ ] [Subtask 03: Criar UseCase PaymentNotificationUseCase](./subtask/Subtask-03-Criar_UseCase_PaymentNotification.md)
- [ ] [Subtask 04: Criar endpoint payment-notification no WebhookPaymentController](./subtask/Subtask-04-Criar_endpoint_webhook.md)
- [ ] [Subtask 05: Registrar UseCase e Presenter no DI](./subtask/Subtask-05-Registrar_DI.md)
- [ ] [Subtask 06: Configurar endpoint como AllowAnonymous](./subtask/Subtask-06-Configurar_AllowAnonymous.md)

## Critérios de Aceite da História

- [ ] `PaymentNotificationInputModel` criado com OrderId (Guid) e FakeCheckout (bool)
- [ ] `PaymentNotificationOutputModel` criado com PaymentId, OrderId, Status, ExternalTransactionId
- [ ] `PaymentNotificationResponse` criado
- [ ] `PaymentNotificationPresenter` criado transformando OutputModel em Response
- [ ] `PaymentNotificationUseCase` criado recebendo IPaymentRepository e IPaymentGateway (real e fake)
- [ ] UseCase busca Payment por OrderId via repositório
- [ ] UseCase valida que Payment existe
- [ ] UseCase seleciona gateway correto (real ou fake) baseado em FakeCheckout
- [ ] UseCase chama CheckPaymentStatusAsync do gateway usando Payment.Id como externalReference
- [ ] UseCase atualiza Payment baseado no resultado:
  - Se aprovado: chama `payment.Approve(transactionId)` e atualiza ExternalTransactionId
  - Se rejeitado: chama `payment.Reject()`
  - Se cancelado: chama `payment.Cancel()`
  - Se pendente: mantém status atual (ou atualiza conforme necessário)
- [ ] UseCase salva Payment atualizado via repositório
- [ ] UseCase retorna OutputModel com status atualizado
- [ ] Endpoint POST `/api/webhookpayment/payment-notification` criado no WebhookPaymentController
- [ ] Endpoint recebe OrderId e fakeCheckout como query parameters
- [ ] Endpoint tem atributo `[AllowAnonymous]` para permitir chamadas do gateway externo
- [ ] Endpoint retorna ApiResponse<PaymentNotificationResponse> com status 200 OK
- [ ] UseCase e Presenter registrados no DI
- [ ] Projeto compila sem erros
- [ ] Endpoint funciona corretamente via Swagger e pode ser chamado externamente
