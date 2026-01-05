# Subtask 06: Completar testes de Domain (DomainValidation, Payment)

## Status
- **Estado:** 🔄 Pendente
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Completar os testes unitários para as classes do Domain, garantindo cobertura completa de `DomainValidation` e `Payment`. Verificar se os testes existentes estão completos e adicionar testes faltantes.

## Classes a testar

### 1. DomainValidation
- [ ] Teste: `ThrowIfNullOrWhiteSpace_WhenValueIsNull_ShouldThrowArgumentException`
- [ ] Teste: `ThrowIfNullOrWhiteSpace_WhenValueIsEmpty_ShouldThrowArgumentException`
- [ ] Teste: `ThrowIfNullOrWhiteSpace_WhenValueIsWhitespace_ShouldThrowArgumentException`
- [ ] Teste: `ThrowIfNullOrWhiteSpace_WhenValueIsValid_ShouldNotThrow`
- [ ] Teste: `ThrowIfNullOrWhiteSpace_WhenMessageIsNull_ShouldUseDefaultMessage`
- [ ] Adicionar testes para outros métodos se existirem

### 2. Payment (entidade de domínio)
- [ ] Verificar testes existentes em `PaymentTests.cs`
- [ ] Teste: `Constructor_WhenValidInput_ShouldCreatePayment`
- [ ] Teste: `Constructor_WhenOrderIdIsEmpty_ShouldThrowException`
- [ ] Teste: `Constructor_WhenTotalAmountIsZero_ShouldThrowException`
- [ ] Teste: `Constructor_WhenTotalAmountIsNegative_ShouldThrowException`
- [ ] Teste: `Constructor_WhenOrderSnapshotIsNull_ShouldThrowException`
- [ ] Teste: `MarkAsQrCodeGenerated_ShouldUpdateStatus`
- [ ] Teste: `MarkAsQrCodeGenerated_ShouldSetQrCodeUrl`
- [ ] Teste: `MarkAsPaid_ShouldUpdateStatus`
- [ ] Teste: `MarkAsPaid_ShouldSetExternalTransactionId`
- [ ] Teste: `MarkAsPaid_ShouldSetPaidAt`
- [ ] Teste: `MarkAsFailed_ShouldUpdateStatus`
- [ ] Teste: `MarkAsFailed_ShouldSetFailureReason`
- [ ] Teste: `MarkAsFailed_ShouldSetFailedAt`
- [ ] Teste: `UpdateFromNotification_WhenValidNotification_ShouldUpdateStatus`
- [ ] Teste: `UpdateFromNotification_WhenInvalidStatus_ShouldHandleGracefully`
- [ ] Testar todos os métodos públicos da entidade

### 3. EnumPaymentStatus
- [ ] Verificar se enum precisa de testes (geralmente não necessário, mas verificar se há lógica)

## Passos de implementação

### 1. Verificar testes existentes
- [ ] Ler arquivo `PaymentTests.cs` existente
- [ ] Identificar cenários não cobertos
- [ ] Listar métodos e propriedades não testados

### 2. Criar/atualizar testes para DomainValidation
- [ ] Criar arquivo `DomainValidationTests.cs` em `Domain/Common/Exceptions/`
- [ ] Implementar todos os cenários listados
- [ ] Usar FluentAssertions para assertions

### 3. Completar testes de Payment
- [ ] Atualizar `PaymentTests.cs` com testes faltantes
- [ ] Garantir cobertura de todos os métodos públicos
- [ ] Garantir cobertura de validações no construtor
- [ ] Garantir cobertura de métodos de domínio (MarkAs*, UpdateFrom*)

### 4. Validar cobertura
- [ ] Executar `dotnet test` com cobertura
- [ ] Verificar cobertura específica do Domain
- [ ] Ajustar testes se necessário

## Padrão de teste (exemplo)

```csharp
[Fact]
public void ThrowIfNullOrWhiteSpace_WhenValueIsNull_ShouldThrowArgumentException()
{
    // Arrange
    string? value = null;
    var message = "Value cannot be null";
    
    // Act & Assert
    var action = () => DomainValidation.ThrowIfNullOrWhiteSpace(value, message);
    action.Should().Throw<ArgumentException>()
        .WithMessage(message);
}

[Fact]
public void MarkAsQrCodeGenerated_WhenValidQrCodeUrl_ShouldUpdateStatus()
{
    // Arrange
    var payment = new Payment(Guid.NewGuid(), 100.00m, "{}");
    var qrCodeUrl = "https://qr.mercadopago.com/...";
    
    // Act
    payment.MarkAsQrCodeGenerated(qrCodeUrl);
    
    // Assert
    payment.Status.Should().Be(EnumPaymentStatus.QrCodeGenerated);
    payment.QrCodeUrl.Should().Be(qrCodeUrl);
}
```

## Como testar
- [ ] Executar `dotnet test` para verificar compilação
- [ ] Executar testes individualmente para validar comportamento
- [ ] Executar `dotnet test /p:CollectCoverage=true` para verificar cobertura
- [ ] Verificar cobertura específica do Domain (deve ser >95%)

## Critérios de aceite
- [ ] Testes completos para `DomainValidation` (todos os métodos)
- [ ] Testes completos para `Payment` (construtor, métodos de domínio, validações)
- [ ] Todos os testes seguem padrão AAA
- [ ] Todos os testes usam FluentAssertions
- [ ] Todos os testes passam
- [ ] Cobertura do Domain é muito alta (>95%)
- [ ] Todos os métodos públicos estão cobertos
- [ ] Validações e exceções estão cobertas

## Referências
- [Testes de Domain - Padrão AAA](./docs/PROMPT_MICROSERVICOS_TESTES_DEPLOY.md#padrão-aaa-arrange-act-assert)
