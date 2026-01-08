# Subtask 02: Criar modelo KitchenPreparationRequest

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Criar o modelo KitchenPreparationRequest que representa o payload da requisição para a API de preparação da cozinha. Este modelo será usado na implementação do KitchenService.

## Passos de implementação
- [ ] Criar diretório `src/Core/FastFood.PayStream.Application/Ports/Parameters/` se não existir
- [ ] Criar arquivo `src/Core/FastFood.PayStream.Application/Ports/Parameters/KitchenPreparationRequest.cs`
- [ ] Definir namespace `FastFood.PayStream.Application.Ports.Parameters`
- [ ] Criar classe pública `KitchenPreparationRequest` com propriedades:
  - `Guid OrderId` - ID do pedido
  - `string OrderSnapshot` - Snapshot do pedido serializado como JSON string
- [ ] Adicionar atributos `[JsonPropertyName]` se necessário para serialização JSON
- [ ] Adicionar comentários XML para documentação

## Como testar
- Executar `dotnet build` no projeto Application (deve compilar sem erros)
- Verificar que a classe pode ser serializada para JSON

## Critérios de aceite
- [ ] Arquivo `KitchenPreparationRequest.cs` criado em `src/Core/FastFood.PayStream.Application/Ports/Parameters/`
- [ ] Classe `KitchenPreparationRequest` criada com propriedades `OrderId` (Guid) e `OrderSnapshot` (string)
- [ ] Propriedades são públicas e podem ser serializadas
- [ ] Comentários XML adicionados
- [ ] Projeto Application compila sem erros
