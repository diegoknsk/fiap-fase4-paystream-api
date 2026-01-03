# Subtask 05: Criar testes para Controllers

## Status
- **Estado:** 🔄 Pendente
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Criar testes unitários para os controllers da API: `PaymentController`, `WebhookPaymentController`, `HealthController`, e `HelloController`. Os testes devem validar o contrato HTTP, respostas, e comportamento dos endpoints.

## Controllers a testar

### 1. PaymentController
- [ ] Teste: `CreatePayment_WhenValidRequest_ShouldReturn201Created`
- [ ] Teste: `CreatePayment_WhenInvalidRequest_ShouldReturn400BadRequest`
- [ ] Teste: `GenerateQrCode_WhenValidRequest_ShouldReturn200Ok`
- [ ] Teste: `GenerateQrCode_WhenPaymentNotFound_ShouldReturn404NotFound`
- [ ] Teste: `GetReceipt_WhenValidRequest_ShouldReturn200Ok`
- [ ] Teste: `GetReceipt_WhenPaymentNotFound_ShouldReturn404NotFound`

### 2. WebhookPaymentController
- [ ] Teste: `ProcessWebhook_WhenValidNotification_ShouldReturn200Ok`
- [ ] Teste: `ProcessWebhook_WhenInvalidPayload_ShouldReturn400BadRequest`
- [ ] Teste: `ProcessWebhook_WhenUseCaseFails_ShouldHandleError`

### 3. HealthController
- [ ] Teste: `Get_ShouldReturn200Ok`
- [ ] Teste: `Get_ShouldReturnHealthStatus`

### 4. HelloController
- [ ] Teste: `Get_ShouldReturn200Ok`
- [ ] Teste: `Get_ShouldReturnHelloMessage`

## Passos de implementação

### 1. Criar estrutura de testes
- [ ] Criar diretório `InterfacesExternas/Controllers/` em testes
- [ ] Criar arquivo `PaymentControllerTests.cs`
- [ ] Criar arquivo `WebhookPaymentControllerTests.cs`
- [ ] Criar arquivo `HealthControllerTests.cs`
- [ ] Criar arquivo `HelloControllerTests.cs`

### 2. Configurar mocks
- [ ] Mock de UseCases para PaymentController
- [ ] Mock de UseCase para WebhookPaymentController
- [ ] Configurar retornos dos mocks conforme cenário

### 3. Implementar testes
- [ ] Usar `Microsoft.AspNetCore.Mvc.Testing` ou criar controllers diretamente
- [ ] Testar respostas HTTP (status codes)
- [ ] Testar estrutura de respostas (ApiResponse)
- [ ] Testar validações de entrada
- [ ] Testar tratamento de erros

### 4. Adicionar referência à API
- [ ] Verificar que projeto de testes referencia `FastFood.PayStream.Api`
- [ ] Adicionar referência se necessário

## Padrão de teste (exemplo)

```csharp
[Fact]
public async Task CreatePayment_WhenValidRequest_ShouldReturn201Created()
{
    // Arrange
    var mockUseCase = new Mock<ICreatePaymentUseCase>();
    var mockPresenter = new Mock<ICreatePaymentPresenter>();
    
    var inputModel = new CreatePaymentInputModel 
    { 
        OrderId = Guid.NewGuid(),
        TotalAmount = 100.00m,
        OrderSnapshot = "{}"
    };
    
    var outputModel = new CreatePaymentOutputModel 
    { 
        PaymentId = Guid.NewGuid(),
        OrderId = inputModel.OrderId,
        Status = (int)EnumPaymentStatus.NotStarted,
        TotalAmount = inputModel.TotalAmount,
        CreatedAt = DateTime.UtcNow
    };
    
    mockUseCase.Setup(u => u.ExecuteAsync(It.IsAny<CreatePaymentInputModel>()))
        .ReturnsAsync(outputModel);
    mockPresenter.Setup(p => p.Present(It.IsAny<CreatePaymentOutputModel>()))
        .Returns(new CreatePaymentResponse { /* ... */ });
    
    var controller = new PaymentController(mockUseCase.Object, mockPresenter.Object);
    
    // Act
    var result = await controller.CreatePayment(inputModel);
    
    // Assert
    result.Should().NotBeNull();
    var objectResult = result as ObjectResult;
    objectResult.Should().NotBeNull();
    objectResult.StatusCode.Should().Be(201);
    objectResult.Value.Should().BeOfType<ApiResponse<CreatePaymentResponse>>();
}
```

## Como testar
- [ ] Executar `dotnet test` para verificar compilação
- [ ] Executar testes individualmente para validar comportamento
- [ ] Executar `dotnet test /p:CollectCoverage=true` para verificar cobertura
- [ ] Verificar que todos os testes passam

## Critérios de aceite
- [ ] Testes criados para `PaymentController` (mínimo 6 cenários)
- [ ] Testes criados para `WebhookPaymentController` (mínimo 3 cenários)
- [ ] Testes criados para `HealthController` (mínimo 2 cenários)
- [ ] Testes criados para `HelloController` (mínimo 2 cenários)
- [ ] Todos os testes seguem padrão AAA
- [ ] Todos os testes usam FluentAssertions
- [ ] Todos os testes validam status codes HTTP
- [ ] Todos os testes validam estrutura de respostas
- [ ] Todos os testes passam
- [ ] Cobertura dos Controllers é adequada (>80%)

## Referências
- [Testes de Controllers - Padrão AAA](./docs/PROMPT_MICROSERVICOS_TESTES_DEPLOY.md#padrão-aaa-arrange-act-assert)
