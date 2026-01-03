# Subtask 08: Criar teste BDD básico com SpecFlow

## Status
- **Estado:** 🔄 Pendente
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Criar pelo menos um teste BDD usando SpecFlow para validar um fluxo crítico do sistema de pagamento. O teste BDD deve seguir o padrão Gherkin e validar um cenário end-to-end do sistema.

## Fluxo crítico a testar

Sugestão de fluxo (adaptar conforme necessário):
```
Cenário: Cliente cria pedido e processa pagamento
  Dado que um pedido foi criado no OrderHub
  Quando o PayStream recebe a solicitação de criação de pagamento
  E o QR Code é gerado com sucesso
  E o pagamento é confirmado via webhook
  Então o pagamento deve estar com status "Paid"
  E o comprovante deve estar disponível
```

## Passos de implementação

### 1. Configurar SpecFlow
- [ ] Verificar que pacotes SpecFlow estão instalados (Subtask 01)
- [ ] Criar arquivo `specflow.json` se necessário
- [ ] Configurar bindings do SpecFlow

### 2. Criar arquivo .feature
- [ ] Criar diretório `Features/` em `FastFood.PayStream.Tests.Bdd/`
- [ ] Criar arquivo `PaymentFlow.feature` (ou nome apropriado)
- [ ] Escrever cenário em Gherkin

### 3. Implementar step definitions
- [ ] Criar classe `PaymentFlowSteps.cs`
- [ ] Implementar steps do Gherkin
- [ ] Configurar mocks/stubs necessários
- [ ] Implementar assertions

### 4. Configurar projeto BDD
- [ ] Verificar que projeto referencia Application e Domain
- [ ] Adicionar referências necessárias
- [ ] Configurar FluentAssertions no projeto BDD

## Exemplo de Feature (Gherkin)

```gherkin
Feature: Payment Flow
    As a customer
    I want to create and process a payment
    So that I can complete my order

    Scenario: Customer creates payment and processes successfully
        Given I have a valid order with ID "123e4567-e89b-12d3-a456-426614174000"
        And the order total amount is 100.00
        When I create a payment for this order
        Then the payment should be created with status "NotStarted"
        When I generate a QR code for the payment
        Then the payment should have status "QrCodeGenerated"
        And the payment should have a QR code URL
        When the payment gateway confirms the payment
        Then the payment should have status "Paid"
        And the payment should have an external transaction ID
```

## Exemplo de Step Definitions

```csharp
[Binding]
public class PaymentFlowSteps
{
    private Guid _orderId;
    private Payment? _payment;
    private Mock<IPaymentRepository> _mockRepository = new();
    
    [Given(@"I have a valid order with ID ""(.*)""")]
    public void GivenIHaveAValidOrderWithId(string orderId)
    {
        _orderId = Guid.Parse(orderId);
    }
    
    [When(@"I create a payment for this order")]
    public async Task WhenICreateAPaymentForThisOrder()
    {
        var useCase = new CreatePaymentUseCase(_mockRepository.Object);
        var input = new CreatePaymentInputModel
        {
            OrderId = _orderId,
            TotalAmount = 100.00m,
            OrderSnapshot = "{}"
        };
        var output = await useCase.ExecuteAsync(input);
        _payment = await _mockRepository.Object.GetByIdAsync(output.PaymentId);
    }
    
    [Then(@"the payment should be created with status ""(.*)""")]
    public void ThenThePaymentShouldBeCreatedWithStatus(string status)
    {
        _payment.Should().NotBeNull();
        _payment.Status.ToString().Should().Be(status);
    }
    
    // ... outros steps
}
```

## Como testar
- [ ] Executar `dotnet test` no projeto BDD
- [ ] Verificar que SpecFlow encontra o arquivo .feature
- [ ] Verificar que todos os steps estão implementados
- [ ] Executar teste BDD individualmente
- [ ] Verificar que teste passa

## Critérios de aceite
- [ ] Arquivo `.feature` criado com cenário em Gherkin
- [ ] Step definitions implementados para todos os steps
- [ ] Teste BDD executa sem erros
- [ ] Teste BDD passa
- [ ] Teste valida fluxo crítico do sistema
- [ ] SpecFlow configurado corretamente
- [ ] Teste faz parte da suíte de testes do projeto

## Referências
- [Documento de Lições Aprendidas - Testes BDD](./docs/PROMPT_MICROSERVICOS_TESTES_DEPLOY.md#12-projeto-de-testes-bdd-opcional-mas-recomendado)
- [SpecFlow Documentation](https://docs.specflow.org/)
