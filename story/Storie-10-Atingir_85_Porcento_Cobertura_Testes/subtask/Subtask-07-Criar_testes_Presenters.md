# Subtask 07: Criar testes para Presenters

## Status
- **Estado:** 🔄 Pendente
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Criar testes unitários para os Presenters da camada Application. Os Presenters são responsáveis por transformar OutputModels em Responses, e devem ser testados para garantir que as transformações estão corretas.

## Presenters a testar

Identificar todos os Presenters existentes e criar testes para cada um:

### 1. CreatePaymentPresenter
- [ ] Teste: `Present_WhenValidOutputModel_ShouldReturnResponse`
- [ ] Teste: `Present_WhenOutputModelIsNull_ShouldHandleGracefully` (se aplicável)

### 2. GenerateQrCodePresenter
- [ ] Teste: `Present_WhenValidOutputModel_ShouldReturnResponse`
- [ ] Teste: `Present_WhenOutputModelIsNull_ShouldHandleGracefully` (se aplicável)

### 3. GetReceiptPresenter
- [ ] Teste: `Present_WhenValidOutputModel_ShouldReturnResponse`
- [ ] Teste: `Present_WhenOutputModelIsNull_ShouldHandleGracefully` (se aplicável)

### 4. PaymentNotificationPresenter (se existir)
- [ ] Teste: `Present_WhenValidOutputModel_ShouldReturnResponse`

## Passos de implementação

### 1. Identificar Presenters
- [ ] Listar todos os Presenters em `src/Core/FastFood.PayStream.Application/Presenters/`
- [ ] Verificar quais já têm testes
- [ ] Criar lista de Presenters a testar

### 2. Criar estrutura de testes
- [ ] Criar diretório `Application/Presenters/` em testes
- [ ] Criar arquivo de teste para cada Presenter

### 3. Implementar testes
- [ ] Testar transformação de OutputModel para Response
- [ ] Validar que todas as propriedades são mapeadas corretamente
- [ ] Testar casos de null/edge cases se aplicável

## Padrão de teste (exemplo)

```csharp
[Fact]
public void Present_WhenValidOutputModel_ShouldReturnResponse()
{
    // Arrange
    var presenter = new CreatePaymentPresenter();
    var outputModel = new CreatePaymentOutputModel
    {
        PaymentId = Guid.NewGuid(),
        OrderId = Guid.NewGuid(),
        Status = (int)EnumPaymentStatus.NotStarted,
        TotalAmount = 100.00m,
        CreatedAt = DateTime.UtcNow
    };
    
    // Act
    var result = presenter.Present(outputModel);
    
    // Assert
    result.Should().NotBeNull();
    result.PaymentId.Should().Be(outputModel.PaymentId);
    result.OrderId.Should().Be(outputModel.OrderId);
    result.Status.Should().Be(outputModel.Status);
    result.TotalAmount.Should().Be(outputModel.TotalAmount);
    result.CreatedAt.Should().Be(outputModel.CreatedAt);
}
```

## Como testar
- [ ] Executar `dotnet test` para verificar compilação
- [ ] Executar testes individualmente para validar comportamento
- [ ] Executar `dotnet test /p:CollectCoverage=true` para verificar cobertura
- [ ] Verificar que todos os testes passam

## Critérios de aceite
- [ ] Testes criados para todos os Presenters existentes
- [ ] Todos os testes validam mapeamento correto de propriedades
- [ ] Todos os testes seguem padrão AAA
- [ ] Todos os testes usam FluentAssertions
- [ ] Todos os testes passam
- [ ] Cobertura dos Presenters é alta (>90%)

## Notas
- Presenters geralmente são classes simples que fazem transformações diretas
- Focar em validar que o mapeamento está correto
- Se Presenters têm lógica adicional, testar essa lógica também

## Referências
- [Padrão AAA](./docs/PROMPT_MICROSERVICOS_TESTES_DEPLOY.md#padrão-aaa-arrange-act-assert)
