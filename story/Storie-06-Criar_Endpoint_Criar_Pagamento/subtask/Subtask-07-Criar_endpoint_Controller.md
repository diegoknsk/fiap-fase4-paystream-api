# Subtask 07: Criar endpoint POST no PaymentController

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Criar o endpoint POST no PaymentController para criar novo pagamento, injetando o UseCase e retornando resposta padronizada.

## Passos de implementação
- [ ] Abrir arquivo `src/InterfacesExternas/FastFood.PayStream.Api/Controllers/PaymentController.cs`
- [ ] Adicionar usings necessários:
  - `FastFood.PayStream.Application.InputModels`
  - `FastFood.PayStream.Application.UseCases`
  - `FastFood.PayStream.Application.Models.Common`
- [ ] Adicionar campo privado readonly `CreatePaymentUseCase _createPaymentUseCase` na classe
- [ ] Atualizar construtor para receber `CreatePaymentUseCase` e armazenar no campo
- [ ] Criar método público assíncrono `Create([FromBody] CreatePaymentInputModel input)` retornando `IActionResult`:
  - Adicionar atributo `[HttpPost("create")]`
  - Adicionar atributo `[ProducesResponseType(typeof(ApiResponse<CreatePaymentResponse>), StatusCodes.Status201Created)]`
  - Adicionar atributo `[ProducesResponseType(typeof(ApiResponse<CreatePaymentResponse>), StatusCodes.Status400BadRequest)]`
  - Adicionar comentário XML para documentação Swagger
  - Implementar método:
    - Chamar `await _createPaymentUseCase.ExecuteAsync(input)`
    - Retornar `CreatedAtAction` ou `Ok` com `ApiResponse<CreatePaymentResponse>.Ok(response, "Pagamento criado com sucesso.")`
    - Tratar exceções `ArgumentException` retornando `BadRequest` com mensagem de erro
- [ ] Adicionar comentários XML para documentação do endpoint

## Como testar
- Executar `dotnet build` no projeto Api (deve compilar sem erros)
- Executar `dotnet run` e acessar Swagger
- Verificar que o endpoint aparece no Swagger
- Testar criação de pagamento via Swagger com dados válidos
- Testar validações com dados inválidos

## Critérios de aceite
- [ ] Campo `_createPaymentUseCase` adicionado na classe PaymentController
- [ ] Construtor atualizado para receber `CreatePaymentUseCase`
- [ ] Método `Create([FromBody] CreatePaymentInputModel input)` criado
- [ ] Atributo `[HttpPost("create")]` aplicado
- [ ] Atributos `[ProducesResponseType]` adicionados para documentação
- [ ] Endpoint chama `_createPaymentUseCase.ExecuteAsync(input)`
- [ ] Endpoint retorna `ApiResponse<CreatePaymentResponse>` com status 201 Created
- [ ] Tratamento de exceções `ArgumentException` implementado
- [ ] Comentários XML adicionados para Swagger
- [ ] Projeto Api compila sem erros
- [ ] Endpoint aparece no Swagger e funciona corretamente
