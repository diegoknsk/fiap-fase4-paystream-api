# Subtask 03: Criar entidade Payment com propriedades básicas

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Criar a entidade de domínio `Payment` com todas as propriedades necessárias, incluindo os novos campos TotalAmount e OrderSnapshot, seguindo os princípios de encapsulamento (getters privados).

## Passos de implementação
- [ ] Criar diretório `src/Core/FastFood.PayStream.Domain/Entities/` se não existir
- [ ] Criar arquivo `Payment.cs` no diretório Entities
- [ ] Definir namespace `FastFood.PayStream.Domain.Entities`
- [ ] Criar classe pública `Payment` com as seguintes propriedades (todas com getters privados):
  - `Id` (Guid) - Identificador único
  - `OrderId` (Guid) - ID do pedido relacionado
  - `Status` (EnumPaymentStatus) - Status atual do pagamento
  - `ExternalTransactionId` (string?) - ID da transação no gateway externo
  - `QrCodeUrl` (string?) - URL do QR Code gerado
  - `CreatedAt` (DateTime) - Data de criação
  - `TotalAmount` (decimal) - Valor total do pedido
  - `OrderSnapshot` (string) - JSON serializado com resumo do pedido
- [ ] Criar construtor protegido sem parâmetros (para EF Core)
- [ ] Criar construtor público com parâmetros: `Payment(Guid orderId, decimal totalAmount, string orderSnapshot)`
- [ ] No construtor público, inicializar:
  - `Id = Guid.NewGuid()`
  - `OrderId = orderId`
  - `Status = EnumPaymentStatus.NotStarted`
  - `CreatedAt = DateTime.UtcNow`
  - `TotalAmount = totalAmount`
  - `OrderSnapshot = orderSnapshot`
  - `ExternalTransactionId = null`
  - `QrCodeUrl = null`
- [ ] Adicionar comentários XML para documentação da classe e propriedades

## Como testar
- Executar `dotnet build` no projeto Domain (deve compilar sem erros)
- Verificar que todas as propriedades têm getters privados
- Validar que o construtor protegido existe (necessário para EF Core)
- Validar que o construtor público inicializa todas as propriedades corretamente
- Verificar que o namespace está correto
- Testar criação de instância com o construtor público

## Critérios de aceite
- [ ] Arquivo `Payment.cs` criado em `src/Core/FastFood.PayStream.Domain/Entities/`
- [ ] Classe `Payment` criada com namespace `FastFood.PayStream.Domain.Entities`
- [ ] Todas as 8 propriedades definidas (Id, OrderId, Status, ExternalTransactionId, QrCodeUrl, CreatedAt, TotalAmount, OrderSnapshot)
- [ ] Todas as propriedades têm getters privados (encapsulamento)
- [ ] Construtor protegido sem parâmetros implementado
- [ ] Construtor público com OrderId, TotalAmount e OrderSnapshot implementado
- [ ] Construtor público inicializa Id como Guid.NewGuid()
- [ ] Construtor público inicializa Status como EnumPaymentStatus.NotStarted
- [ ] Construtor público inicializa CreatedAt como DateTime.UtcNow
- [ ] Comentários XML adicionados para documentação
- [ ] Projeto Domain compila sem erros
