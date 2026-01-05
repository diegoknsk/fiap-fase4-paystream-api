# Subtask 04: Criar testes para UseCases faltantes

## Status
- **Estado:** 🔄 Pendente
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Criar testes unitários completos para os UseCases que ainda não possuem cobertura adequada: `GenerateQrCodeUseCase`, `GetReceiptUseCase`, e `PaymentNotificationUseCase`. Os testes devem seguir o padrão AAA e usar FluentAssertions.

## UseCases a testar

### 1. GenerateQrCodeUseCase
- [ ] Teste: `GenerateQrCode_WhenValidInput_ShouldReturnQrCodeUrl`
- [ ] Teste: `GenerateQrCode_WhenPaymentNotFound_ShouldThrowNotFoundException`
- [ ] Teste: `GenerateQrCode_WhenOrderSnapshotInvalid_ShouldThrowException`
- [ ] Teste: `GenerateQrCode_WhenGatewayFails_ShouldHandleError`
- [ ] Teste: `GenerateQrCode_WhenFakeCheckout_ShouldUseFakeGateway`
- [ ] Teste: `GenerateQrCode_WhenSuccess_ShouldUpdatePaymentStatus`

### 2. GetReceiptUseCase
- [ ] Teste: `GetReceipt_WhenValidInput_ShouldReturnReceipt`
- [ ] Teste: `GetReceipt_WhenPaymentNotFound_ShouldThrowNotFoundException`
- [ ] Teste: `GetReceipt_WhenNoExternalTransactionId_ShouldThrowException`
- [ ] Teste: `GetReceipt_WhenGatewayFails_ShouldHandleError`
- [ ] Teste: `GetReceipt_WhenFakeCheckout_ShouldUseFakeGateway`

### 3. PaymentNotificationUseCase
- [ ] Teste: `PaymentNotification_WhenValidNotification_ShouldUpdatePayment`
- [ ] Teste: `PaymentNotification_WhenPaymentNotFound_ShouldHandleGracefully`
- [ ] Teste: `PaymentNotification_WhenInvalidStatus_ShouldHandleError`
- [ ] Teste: `PaymentNotification_WhenNotificationProcessed_ShouldUpdateStatusCorrectly`
- [ ] Teste: `PaymentNotification_WhenRepositoryFails_ShouldHandleError`

## Passos de implementação

### 1. Criar estrutura de testes
- [ ] Criar arquivo `GenerateQrCodeUseCaseTests.cs` em `Application/UseCases/`
- [ ] Criar arquivo `GetReceiptUseCaseTests.cs` em `Application/UseCases/`
- [ ] Criar arquivo `PaymentNotificationUseCaseTests.cs` em `Application/UseCases/`

### 2. Configurar mocks
- [ ] Mock de `IPaymentRepository` para todos os testes
- [ ] Mock de `IPaymentGateway` para testes de GenerateQrCode e GetReceipt
- [ ] Configurar retornos dos mocks conforme cenário

### 3. Implementar testes
- [ ] Seguir padrão AAA (Arrange, Act, Assert)
- [ ] Usar FluentAssertions para assertions
- [ ] Testar casos de sucesso e falha
- [ ] Testar validações de entrada
- [ ] Testar tratamento de erros

### 4. Validar cobertura
- [ ] Executar `dotnet test` com cobertura
- [ ] Verificar que todos os caminhos dos UseCases estão cobertos
- [ ] Ajustar testes se necessário para aumentar cobertura

## Padrão de teste (exemplo)

```csharp
[Fact]
public async Task GenerateQrCode_WhenValidInput_ShouldReturnQrCodeUrl()
{
    // Arrange
    var paymentId = Guid.NewGuid();
    var orderId = Guid.NewGuid();
    var qrCodeUrl = "https://qr.mercadopago.com/...";
    
    var mockRepository = new Mock<IPaymentRepository>();
    var mockGateway = new Mock<IPaymentGateway>();
    
    var payment = new Payment(orderId, 100.00m, "{}");
    mockRepository.Setup(r => r.GetByOrderIdAsync(orderId))
        .ReturnsAsync(payment);
    mockGateway.Setup(g => g.GenerateQrCodeAsync(It.IsAny<QrCodeRequest>()))
        .ReturnsAsync(new QrCodeResponse { QrCodeUrl = qrCodeUrl });
    
    var useCase = new GenerateQrCodeUseCase(mockRepository.Object, mockGateway.Object);
    
    // Act
    var result = await useCase.ExecuteAsync(new GenerateQrCodeInputModel 
    { 
        OrderId = orderId 
    });
    
    // Assert
    result.Should().NotBeNull();
    result.IsSuccess.Should().BeTrue();
    result.Value.QrCodeUrl.Should().Be(qrCodeUrl);
    mockRepository.Verify(r => r.UpdateAsync(It.IsAny<Payment>()), Times.Once);
}
```

## Como testar
- [ ] Executar `dotnet test` para verificar compilação
- [ ] Executar testes individualmente para validar comportamento
- [ ] Executar `dotnet test /p:CollectCoverage=true` para verificar cobertura
- [ ] Verificar que todos os testes passam

## Critérios de aceite
- [ ] Testes criados para `GenerateQrCodeUseCase` (mínimo 6 cenários)
- [ ] Testes criados para `GetReceiptUseCase` (mínimo 5 cenários)
- [ ] Testes criados para `PaymentNotificationUseCase` (mínimo 5 cenários)
- [ ] Todos os testes seguem padrão AAA
- [ ] Todos os testes usam FluentAssertions
- [ ] Todos os testes passam
- [ ] Cobertura dos UseCases é alta (>90%)
- [ ] Mocks configurados corretamente
- [ ] Casos de sucesso e falha cobertos

## Referências
- [Padrão AAA](./docs/PROMPT_MICROSERVICOS_TESTES_DEPLOY.md#padrão-aaa-arrange-act-assert)
- [Nomenclatura de Testes](./docs/PROMPT_MICROSERVICOS_TESTES_DEPLOY.md#padrão-de-nomenclatura)
