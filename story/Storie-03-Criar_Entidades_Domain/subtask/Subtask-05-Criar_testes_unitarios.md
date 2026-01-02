# Subtask 05: Criar testes unitários para entidade Payment

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Criar testes unitários completos para a entidade Payment, validando todos os métodos de domínio, regras de negócio e comportamentos esperados.

## Passos de implementação
- [ ] Verificar se o projeto de testes `FastFood.PayStream.Tests.Unit` existe
- [ ] Criar diretório `Domain/Entities/` no projeto de testes se não existir
- [ ] Criar arquivo `PaymentTests.cs` no diretório Domain/Entities
- [ ] Criar testes para o construtor:
  - Teste: Construtor deve inicializar Payment com Status NotStarted
  - Teste: Construtor deve inicializar CreatedAt com DateTime.UtcNow
  - Teste: Construtor deve inicializar Id como novo Guid
- [ ] Criar testes para o método Start():
  - Teste: Start() deve alterar Status para Started
- [ ] Criar testes para o método GenerateQrCode():
  - Teste: GenerateQrCode() deve lançar ArgumentException quando qrCodeUrl é null
  - Teste: GenerateQrCode() deve lançar ArgumentException quando qrCodeUrl é vazio
  - Teste: GenerateQrCode() deve lançar ArgumentException quando qrCodeUrl é whitespace
  - Teste: GenerateQrCode() deve atribuir QrCodeUrl quando válido
  - Teste: GenerateQrCode() deve alterar Status para QrCodeGenerated quando válido
- [ ] Criar testes para o método Approve():
  - Teste: Approve() deve lançar ArgumentException quando transactionId é null
  - Teste: Approve() deve lançar ArgumentException quando transactionId é vazio
  - Teste: Approve() deve atribuir ExternalTransactionId quando válido
  - Teste: Approve() deve alterar Status para Approved quando válido
- [ ] Criar testes para o método Reject():
  - Teste: Reject() deve alterar Status para Rejected
- [ ] Criar testes para o método Cancel():
  - Teste: Cancel() deve alterar Status para Canceled
- [ ] Criar testes de fluxo completo:
  - Teste: Fluxo Start → GenerateQrCode → Approve deve funcionar corretamente
  - Teste: Fluxo Start → GenerateQrCode → Reject deve funcionar corretamente
  - Teste: Fluxo Start → Cancel deve funcionar corretamente

## Como testar
- Executar `dotnet test` no projeto de testes (todos os testes devem passar)
- Verificar cobertura de código (deve estar acima de 90% para a entidade Payment)
- Validar que todos os cenários de sucesso estão cobertos
- Validar que todos os cenários de erro (validações) estão cobertos
- Verificar que os testes são independentes e podem ser executados em qualquer ordem

## Critérios de aceite
- [ ] Arquivo `PaymentTests.cs` criado em `src/tests/FastFood.PayStream.Tests.Unit/Domain/Entities/`
- [ ] Mínimo de 15 testes unitários criados
- [ ] Todos os testes passam (`dotnet test`)
- [ ] Testes cobrem todos os métodos de domínio (Start, GenerateQrCode, Approve, Reject, Cancel)
- [ ] Testes validam regras de negócio (validações de parâmetros)
- [ ] Testes validam fluxos completos (Start → GenerateQrCode → Approve)
- [ ] Cobertura de código da entidade Payment acima de 90%
- [ ] Testes são independentes e podem ser executados em qualquer ordem
- [ ] Nomenclatura dos testes segue padrão: `MethodName_Scenario_ExpectedBehavior`
