# Subtask 02: Criar entidade de persistência PaymentEntity

## Status
- **Estado:** ✅ Concluída
- **Data de Conclusão:** 02/01/2025

## Descrição
Criar a entidade de persistência `PaymentEntity` que representa a estrutura da tabela Payments no banco de dados PostgreSQL, separada da entidade de domínio `Payment`.

## Passos de implementação
- [x] Criar diretório `src/Infra/FastFood.PayStream.Infra.Persistence/Entities/` se não existir
- [x] Criar arquivo `PaymentEntity.cs` no diretório Entities
- [x] Definir namespace `FastFood.PayStream.Infra.Persistence.Entities`
- [x] Criar classe pública `PaymentEntity` com as seguintes propriedades públicas (com getters e setters):
  - `Id` (Guid) - Identificador único
  - `OrderId` (Guid) - ID do pedido relacionado
  - `Status` (int) - Status do pagamento (representa EnumPaymentStatus)
  - `ExternalTransactionId` (string?) - ID da transação no gateway externo
  - `QrCodeUrl` (string?) - URL do QR Code gerado
  - `CreatedAt` (DateTime) - Data de criação
  - `TotalAmount` (decimal) - Valor total do pedido
  - `OrderSnapshot` (string) - JSON serializado com resumo do pedido (será mapeado como JSONB)
- [x] Adicionar comentários XML para documentação da classe e propriedades
- [x] Adicionar comentário explicando que esta é a entidade de persistência, separada da entidade de domínio

## Como testar
- Executar `dotnet build` no projeto Infra.Persistence (deve compilar sem erros)
- Verificar que todas as propriedades estão acessíveis
- Verificar que o namespace está correto
- Validar que a classe pode ser instanciada

## Critérios de aceite
- [x] Arquivo `PaymentEntity.cs` criado em `src/Infra/FastFood.PayStream.Infra.Persistence/Entities/`
- [x] Classe `PaymentEntity` criada com namespace `FastFood.PayStream.Infra.Persistence.Entities`
- [x] Todas as 8 propriedades definidas (Id, OrderId, Status, ExternalTransactionId, QrCodeUrl, CreatedAt, TotalAmount, OrderSnapshot)
- [x] Todas as propriedades têm getters e setters públicos
- [x] Tipo `Status` é `int` (não enum, para persistência)
- [x] Tipo `OrderSnapshot` é `string` (será mapeado como JSONB)
- [x] Comentários XML adicionados para documentação
- [x] Projeto Infra.Persistence compila sem erros
