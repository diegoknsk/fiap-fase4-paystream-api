# Subtask 09: Validar [AllowAnonymous] no WebhookPaymentController

## Status
- **Estado:** 🔄 Pendente
- **Data de Início:** -
- **Data de Conclusão:** -

## Descrição
Validar que o WebhookPaymentController está configurado corretamente com `[AllowAnonymous]` para permitir acesso público ao endpoint de webhook.

## Objetivo
Garantir que o endpoint de webhook permanece público e acessível sem autenticação, permitindo que gateways de pagamento externos possam chamar o endpoint.

## Arquivo a Verificar

### `src/InterfacesExternas/FastFood.PayStream.Api/Controllers/WebhookPaymentController.cs`

## Passos de Validação

1. [ ] Verificar que a classe tem o atributo `[AllowAnonymous]`:
   ```csharp
   [ApiController]
   [Route("api/[controller]")]
   [AllowAnonymous]
   public class WebhookPaymentController : ControllerBase
   ```

2. [ ] Verificar que o método `PaymentNotification` não tem `[Authorize]`

3. [ ] Testar que o endpoint funciona sem token

4. [ ] Verificar que o Swagger não mostra botão de autenticação para este endpoint

## Estado Atual Esperado

O WebhookPaymentController já deve estar configurado com `[AllowAnonymous]` conforme a story anterior. Esta subtask é apenas para validar que está correto.

## Como Testar

- Executar `dotnet build` no projeto Api (deve compilar sem erros)
- Executar `dotnet run` e acessar Swagger
- Verificar que o endpoint de webhook NÃO mostra ícone de cadeado
- Testar chamada sem token → deve funcionar (200 OK)
- Testar chamada com token → também deve funcionar (200 OK)

## Critérios de Aceite

- [ ] `[AllowAnonymous]` está presente na classe `WebhookPaymentController`
- [ ] Método `PaymentNotification` não tem `[Authorize]`
- [ ] Endpoint funciona sem token
- [ ] Endpoint funciona com token (opcional, mas deve aceitar)
- [ ] Swagger não mostra botão de autenticação para este endpoint
- [ ] Código compila sem erros

## Observações

- O endpoint de webhook DEVE permanecer público
- Gateways de pagamento externos não têm tokens de autenticação
- Esta é uma exceção à regra geral de autenticação da API
