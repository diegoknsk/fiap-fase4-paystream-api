# Subtask 08: Aplicar [Authorize] no PaymentController

## Status
- **Estado:** 🔄 Pendente
- **Data de Início:** -
- **Data de Conclusão:** -

## Descrição
Adicionar o atributo `[Authorize]` com esquema CustomerBearer e política Customer em todos os endpoints do PaymentController.

## Objetivo
Proteger todos os endpoints do PaymentController exigindo autenticação CustomerBearer com política Customer.

## Arquivo a Modificar

### `src/InterfacesExternas/FastFood.PayStream.Api/Controllers/PaymentController.cs`

## Endpoints a Proteger

1. **POST `/api/payment/create`** - Criar pagamento
2. **POST `/api/payment/generate-qrcode`** - Gerar QR Code
3. **GET `/api/payment/receipt-from-gateway`** - Obter comprovante

## Passos de Implementação

1. [ ] Adicionar using no início do arquivo:
   ```csharp
   using Microsoft.AspNetCore.Authorization;
   ```

2. [ ] Adicionar `[Authorize]` em cada método do PaymentController:
   ```csharp
   [HttpPost("create")]
   [Authorize(AuthenticationSchemes = "CustomerBearer", Policy = "Customer")]
   [ProducesResponseType(...)]
   public async Task<IActionResult> Create([FromBody] CreatePaymentInputModel input)
   {
       // ...
   }
   ```

3. [ ] Aplicar o mesmo padrão para os outros endpoints:
   - `GenerateQrCode`
   - `GetReceiptFromGateway`

4. [ ] Verificar que o código compila sem erros

## Exemplo Completo

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FastFood.PayStream.Application.Models.Common;
using FastFood.PayStream.Application.InputModels;
using FastFood.PayStream.Application.UseCases;
using FastFood.PayStream.Application.Responses;

namespace FastFood.PayStream.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    // ... campos e construtor ...

    [HttpPost("create")]
    [Authorize(AuthenticationSchemes = "CustomerBearer", Policy = "Customer")]
    [ProducesResponseType(typeof(ApiResponse<CreatePaymentResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<CreatePaymentResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreatePaymentInputModel input)
    {
        // ... implementação existente ...
    }

    [HttpPost("generate-qrcode")]
    [Authorize(AuthenticationSchemes = "CustomerBearer", Policy = "Customer")]
    [ProducesResponseType(typeof(ApiResponse<GenerateQrCodeResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<GenerateQrCodeResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<GenerateQrCodeResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GenerateQrCode([FromQuery] Guid orderId, [FromQuery] bool fakeCheckout = false)
    {
        // ... implementação existente ...
    }

    [HttpGet("receipt-from-gateway")]
    [Authorize(AuthenticationSchemes = "CustomerBearer", Policy = "Customer")]
    [ProducesResponseType(typeof(ApiResponse<GetReceiptResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<GetReceiptResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<GetReceiptResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetReceiptFromGateway([FromQuery] Guid orderId, [FromQuery] bool fakeCheckout = false)
    {
        // ... implementação existente ...
    }
}
```

## Como Testar

- Executar `dotnet build` no projeto Api (deve compilar sem erros)
- Executar `dotnet run` e acessar Swagger
- Verificar que os endpoints mostram o ícone de cadeado no Swagger
- Testar chamada sem token → deve retornar 401
- Testar chamada com token Customer válido → deve funcionar
- Testar chamada com token Cognito → deve retornar 403

## Critérios de Aceite

- [ ] Using `Microsoft.AspNetCore.Authorization` adicionado
- [ ] `[Authorize(AuthenticationSchemes = "CustomerBearer", Policy = "Customer")]` adicionado no método `Create`
- [ ] `[Authorize(AuthenticationSchemes = "CustomerBearer", Policy = "Customer")]` adicionado no método `GenerateQrCode`
- [ ] `[Authorize(AuthenticationSchemes = "CustomerBearer", Policy = "Customer")]` adicionado no método `GetReceiptFromGateway`
- [ ] Código compila sem erros
- [ ] Endpoints aparecem protegidos no Swagger
- [ ] Chamadas sem token retornam 401
- [ ] Chamadas com token Customer válido funcionam
- [ ] Chamadas com token Cognito retornam 403

## Observações

- **Esquema:** "CustomerBearer" (deve ser exatamente este nome)
- **Política:** "Customer" (deve ser exatamente este nome)
- **Ordem dos Atributos:** `[Authorize]` pode vir antes ou depois de `[HttpPost]`/`[HttpGet]`, mas deve vir antes de `[ProducesResponseType]`
