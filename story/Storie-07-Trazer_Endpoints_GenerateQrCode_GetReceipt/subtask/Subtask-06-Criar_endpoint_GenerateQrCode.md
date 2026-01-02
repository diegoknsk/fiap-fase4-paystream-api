# Subtask 06: Criar endpoint GenerateQrCode no PaymentController

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Criar o endpoint POST para gerar QR Code no PaymentController, injetando o UseCase e suportando parâmetro fakeCheckout.

## Passos de implementação
- [ ] Abrir arquivo `src/InterfacesExternas/FastFood.PayStream.Api/Controllers/PaymentController.cs`
- [ ] Adicionar campo privado readonly `GenerateQrCodeUseCase _generateQrCodeUseCase`
- [ ] Atualizar construtor para receber `GenerateQrCodeUseCase` e armazenar
- [ ] Criar método público assíncrono `GenerateQrCode([FromQuery] Guid orderId, [FromQuery] bool fakeCheckout = false)` retornando `IActionResult`:
  - Adicionar atributo `[HttpPost("generate-qrcode")]`
  - Adicionar atributos `[ProducesResponseType]` para documentação (200 OK, 400 BadRequest, 404 NotFound)
  - Adicionar comentário XML para Swagger
  - Implementar método:
    - Criar `GenerateQrCodeInputModel` com OrderId e FakeCheckout
    - Chamar `await _generateQrCodeUseCase.ExecuteAsync(input)`
    - Retornar `Ok` com `ApiResponse<GenerateQrCodeResponse>.Ok(response, "QR Code gerado com sucesso.")`
    - Tratar exceções `ArgumentException` retornando `BadRequest`
    - Tratar exceções `ApplicationException` retornando `NotFound` ou `BadRequest` conforme apropriado
- [ ] Adicionar comentários XML para documentação

## Como testar
- Executar `dotnet build` no projeto Api (deve compilar sem erros)
- Executar `dotnet run` e acessar Swagger
- Testar endpoint com OrderId válido
- Testar endpoint com fakeCheckout=true e fakeCheckout=false
- Testar validações com dados inválidos

## Critérios de aceite
- [ ] Campo `_generateQrCodeUseCase` adicionado
- [ ] Construtor atualizado para receber `GenerateQrCodeUseCase`
- [ ] Método `GenerateQrCode` criado com parâmetros orderId e fakeCheckout
- [ ] Atributo `[HttpPost("generate-qrcode")]` aplicado
- [ ] Atributos `[ProducesResponseType]` adicionados
- [ ] Endpoint chama `_generateQrCodeUseCase.ExecuteAsync`
- [ ] Endpoint retorna `ApiResponse<GenerateQrCodeResponse>` com status 200 OK
- [ ] Tratamento de exceções implementado
- [ ] Comentários XML adicionados
- [ ] Projeto Api compila sem erros
- [ ] Endpoint funciona corretamente via Swagger
