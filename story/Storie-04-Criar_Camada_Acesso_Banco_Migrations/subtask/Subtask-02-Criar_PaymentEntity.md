# Subtask 02: Criar entidade de persistência PaymentEntity

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Criar a entidade de persistência `PaymentEntity` que representa a estrutura da tabela Payments no banco de dados PostgreSQL, separada da entidade de domínio `Payment`.

## Passos de implementação
- [ ] Criar diretório `src/Infra/FastFood.PayStream.Infra.Persistence/Entities/` se não existir
- [ ] Criar arquivo `PaymentEntity.cs` no diretório Entities
- [ ] Definir namespace `FastFood.PayStream.Infra.Persistence.Entities`
- [ ] Criar classe pública `PaymentEntity` com as seguintes propriedades públicas (com getters e setters):
  - `Id` (Guid) - Identificador único
  - `OrderId` (Guid) - ID do pedido relacionado
  - `Status` (int) - Status do pagamento (representa EnumPaymentStatus)
  - `ExternalTransactionId` (string?) - ID da transação no gateway externo
  - `QrCodeUrl` (string?) - URL do QR Code gerado
  - `CreatedAt` (DateTime) - Data de criação
  - `TotalAmount` (decimal) - Valor total do pedido
  - `OrderSnapshot` (string) - JSON serializado com resumo do pedido (será mapeado como JSONB)
- [ ] Adicionar comentários XML para documentação da classe e propriedades
- [ ] Adicionar comentário explicando que esta é a entidade de persistência, separada da entidade de domínio

## Como testar
- Executar `dotnet build` no projeto Infra.Persistence (deve compilar sem erros)
- Verificar que todas as propriedades estão acessíveis
- Verificar que o namespace está correto
- Validar que a classe pode ser instanciada

## Critérios de aceite
- [ ] Arquivo `PaymentEntity.cs` criado em `src/Infra/FastFood.PayStream.Infra.Persistence/Entities/`
- [ ] Classe `PaymentEntity` criada com namespace `FastFood.PayStream.Infra.Persistence.Entities`
- [ ] Todas as 8 propriedades definidas (Id, OrderId, Status, ExternalTransactionId, QrCodeUrl, CreatedAt, TotalAmount, OrderSnapshot)
- [ ] Todas as propriedades têm getters e setters públicos
- [ ] Tipo `Status` é `int` (não enum, para persistência)
- [ ] Tipo `OrderSnapshot` é `string` (será mapeado como JSONB)
- [ ] Comentários XML adicionados para documentação
- [ ] Projeto Infra.Persistence compila sem erros
