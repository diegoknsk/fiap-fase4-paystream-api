# Subtask 04: Criar endpoint payment-notification no WebhookPaymentController

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Criar o endpoint POST para receber notificações de pagamento no WebhookPaymentController, injetando o UseCase e configurando como AllowAnonymous para permitir chamadas do gateway externo.

## Passos de implementação
- [ ] Abrir arquivo `src/InterfacesExternas/FastFood.PayStream.Api/Controllers/WebhookPaymentController.cs`
- [ ] Adicionar usings necessários:
  - `Microsoft.AspNetCore.Mvc`
  - `Microsoft.AspNetCore.Authorization`
  - `FastFood.PayStream.Application.InputModels`
  - `FastFood.PayStream.Application.UseCases`
  - `FastFood.PayStream.Application.Models.Common`
- [ ] Adicionar campo privado readonly `PaymentNotificationUseCase _paymentNotificationUseCase` na classe
- [ ] Atualizar construtor para receber `PaymentNotificationUseCase` e armazenar no campo
- [ ] Criar método público assíncrono `PaymentNotification([FromQuery] Guid orderId, [FromQuery] bool fakeCheckout = false)` retornando `IActionResult`:
  - Adicionar atributo `[HttpPost("payment-notification")]`
  - Adicionar atributo `[AllowAnonymous]` para permitir chamadas externas do gateway
  - Adicionar atributos `[ProducesResponseType]` para documentação (200 OK, 400 BadRequest, 404 NotFound)
  - Adicionar comentário XML para documentação Swagger explicando que é um webhook
  - Implementar método:
    - Criar `PaymentNotificationInputModel` com OrderId e FakeCheckout
    - Chamar `await _paymentNotificationUseCase.ExecuteAsync(input)`
    - Retornar `Ok` com `ApiResponse<PaymentNotificationResponse>.Ok(response, "Notificação de pagamento processada com sucesso.")`
    - Tratar exceções `ApplicationException` retornando `NotFound` ou `BadRequest` conforme apropriado
    - Tratar exceções `ArgumentException` retornando `BadRequest`
- [ ] Adicionar comentários XML para documentação do endpoint

## Como testar
- Executar `dotnet build` no projeto Api (deve compilar sem erros)
- Executar `dotnet run` e acessar Swagger
- Testar endpoint com OrderId válido
- Testar endpoint com fakeCheckout=true e fakeCheckout=false
- Testar validações (Payment não existe)
- Verificar que endpoint pode ser chamado sem autenticação (AllowAnonymous)

## Critérios de aceite
- [ ] Campo `_paymentNotificationUseCase` adicionado na classe WebhookPaymentController
- [ ] Construtor atualizado para receber `PaymentNotificationUseCase`
- [ ] Método `PaymentNotification` criado com parâmetros orderId e fakeCheckout
- [ ] Atributo `[HttpPost("payment-notification")]` aplicado
- [ ] Atributo `[AllowAnonymous]` aplicado
- [ ] Atributos `[ProducesResponseType]` adicionados
- [ ] Endpoint chama `_paymentNotificationUseCase.ExecuteAsync`
- [ ] Endpoint retorna `ApiResponse<PaymentNotificationResponse>` com status 200 OK
- [ ] Tratamento de exceções implementado
- [ ] Comentários XML adicionados para Swagger
- [ ] Projeto Api compila sem erros
- [ ] Endpoint funciona corretamente via Swagger
- [ ] Endpoint pode ser chamado sem autenticação
