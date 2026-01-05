# Storie-03: Criar Entidades Domain

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Como desenvolvedor, quero criar as entidades de domínio do microserviço de pagamento seguindo os princípios de Clean Architecture, para que possamos representar corretamente o domínio de pagamentos com suas regras de negócio encapsuladas.

## Objetivo
Criar a entidade de domínio `Payment` com todas as propriedades necessárias (Id, OrderId, Status, ExternalTransactionId, QrCodeUrl, CreatedAt, TotalAmount, OrderSnapshot), o enum `EnumPaymentStatus` com todos os status possíveis, e implementar os métodos de domínio que encapsulam as regras de negócio (Start, GenerateQrCode, Approve, Reject, Cancel).

## Escopo Técnico
- Tecnologias: .NET 8, C#
- Arquivos afetados:
  - `src/Core/FastFood.PayStream.Domain/Entities/Payment.cs`
  - `src/Core/FastFood.PayStream.Domain/Common/Enums/EnumPaymentStatus.cs`
  - `src/Core/FastFood.PayStream.Domain/Common/Exceptions/DomainValidation.cs` (se necessário)
- Estrutura da entidade Payment:
  - Id (Guid) - Primary Key
  - OrderId (Guid) - Foreign Key para o pedido
  - Status (EnumPaymentStatus) - Status atual do pagamento
  - ExternalTransactionId (string?) - ID da transação no gateway externo
  - QrCodeUrl (string?) - URL do QR Code gerado
  - CreatedAt (DateTime) - Data de criação
  - TotalAmount (decimal) - Valor total do pedido replicado
  - OrderSnapshot (string) - JSONB com resumo do pedido (serializado como JSON string)
- Métodos de domínio:
  - Construtor padrão (protected para EF Core)
  - Construtor com OrderId e TotalAmount
  - Start() - Inicia o processo de pagamento
  - GenerateQrCode(string qrCodeUrl) - Gera QR Code e atualiza status
  - Approve(string transactionId) - Aprova pagamento
  - Reject() - Rejeita pagamento
  - Cancel() - Cancela pagamento

## Subtasks

- [ ] [Subtask 01: Criar enum EnumPaymentStatus](./subtask/Subtask-01-Criar_enum_EnumPaymentStatus.md)
- [ ] [Subtask 02: Criar classe DomainValidation para validações](./subtask/Subtask-02-Criar_DomainValidation.md)
- [ ] [Subtask 03: Criar entidade Payment com propriedades básicas](./subtask/Subtask-03-Criar_entidade_Payment_basica.md)
- [ ] [Subtask 04: Implementar métodos de domínio na entidade Payment](./subtask/Subtask-04-Implementar_metodos_dominio.md)
- [ ] [Subtask 05: Criar testes unitários para entidade Payment](./subtask/Subtask-05-Criar_testes_unitarios.md)

## Critérios de Aceite da História

- [ ] Enum `EnumPaymentStatus` criado em `src/Core/FastFood.PayStream.Domain/Common/Enums/EnumPaymentStatus.cs` com valores: NotStarted (0), Started (1), QrCodeGenerated (2), Approved (3), Rejected (4), Canceled (5)
- [ ] Classe `DomainValidation` criada em `src/Core/FastFood.PayStream.Domain/Common/Exceptions/DomainValidation.cs` com método `ThrowIfNullOrWhiteSpace`
- [ ] Entidade `Payment` criada em `src/Core/FastFood.PayStream.Domain/Entities/Payment.cs` com todas as propriedades: Id, OrderId, Status, ExternalTransactionId, QrCodeUrl, CreatedAt, TotalAmount, OrderSnapshot
- [ ] Construtor protegido sem parâmetros implementado para suporte ao EF Core
- [ ] Construtor público com OrderId e TotalAmount implementado, inicializando Status como NotStarted e CreatedAt como DateTime.UtcNow
- [ ] Método `Start()` implementado, alterando Status para Started
- [ ] Método `GenerateQrCode(string qrCodeUrl)` implementado, validando qrCodeUrl e alterando Status para QrCodeGenerated
- [ ] Método `Approve(string transactionId)` implementado, validando transactionId e alterando Status para Approved
- [ ] Método `Reject()` implementado, alterando Status para Rejected
- [ ] Método `Cancel()` implementado, alterando Status para Canceled
- [ ] Todas as propriedades têm getters privados (encapsulamento)
- [ ] Testes unitários criados cobrindo todos os métodos de domínio
- [ ] Testes unitários validando regras de negócio (ex: não pode aprovar sem transactionId)
- [ ] Código compila sem erros
- [ ] Nenhuma violação de regras de arquitetura (Domain não depende de outras camadas)
