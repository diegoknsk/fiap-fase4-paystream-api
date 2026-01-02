# Subtask 07: Criar endpoint GetReceiptFromGateway no PaymentController

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Criar o endpoint GET para obter comprovante de pagamento no PaymentController, injetando o UseCase e suportando parâmetro fakeCheckout.

## Passos de implementação
- [ ] Abrir arquivo `src/InterfacesExternas/FastFood.PayStream.Api/Controllers/PaymentController.cs`
- [ ] Adicionar campo privado readonly `GetReceiptUseCase _getReceiptUseCase`
- [ ] Atualizar construtor para receber `GetReceiptUseCase` e armazenar
- [ ] Criar método público assíncrono `GetReceiptFromGateway([FromQuery] Guid orderId, [FromQuery] bool fakeCheckout = false)` retornando `IActionResult`:
  - Adicionar atributo `[HttpGet("receipt-from-gateway")]`
  - Adicionar atributos `[ProducesResponseType]` para documentação (200 OK, 400 BadRequest, 404 NotFound)
  - Adicionar comentário XML para Swagger
  - Implementar método:
    - Criar `GetReceiptInputModel` com OrderId e FakeCheckout
    - Chamar `await _getReceiptUseCase.ExecuteAsync(input)`
    - Retornar `Ok` com `ApiResponse<GetReceiptResponse>.Ok(response, "Comprovante obtido com sucesso.")`
    - Tratar exceções `ArgumentException` retornando `BadRequest`
    - Tratar exceções `ApplicationException` retornando `NotFound` ou `BadRequest` conforme apropriado
- [ ] Adicionar comentários XML para documentação

## Como testar
- Executar `dotnet build` no projeto Api (deve compilar sem erros)
- Executar `dotnet run` e acessar Swagger
- Testar endpoint com OrderId válido que tem ExternalTransactionId
- Testar endpoint com fakeCheckout=true e fakeCheckout=false
- Testar validações (Payment não existe, sem ExternalTransactionId)

## Critérios de aceite
- [ ] Campo `_getReceiptUseCase` adicionado
- [ ] Construtor atualizado para receber `GetReceiptUseCase`
- [ ] Método `GetReceiptFromGateway` criado com parâmetros orderId e fakeCheckout
- [ ] Atributo `[HttpGet("receipt-from-gateway")]` aplicado
- [ ] Atributos `[ProducesResponseType]` adicionados
- [ ] Endpoint chama `_getReceiptUseCase.ExecuteAsync`
- [ ] Endpoint retorna `ApiResponse<GetReceiptResponse>` com status 200 OK
- [ ] Tratamento de exceções implementado
- [ ] Comentários XML adicionados
- [ ] Projeto Api compila sem erros
- [ ] Endpoint funciona corretamente via Swagger
