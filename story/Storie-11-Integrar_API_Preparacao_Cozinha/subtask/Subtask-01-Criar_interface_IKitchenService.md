# Subtask 01: Criar interface IKitchenService na Application

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Criar a interface IKitchenService na camada Application que define o contrato para integração com a API de preparação da cozinha, seguindo o padrão de Clean Architecture.

## Passos de implementação
- [ ] Criar arquivo `src/Core/FastFood.PayStream.Application/Ports/IKitchenService.cs`
- [ ] Definir namespace `FastFood.PayStream.Application.Ports`
- [ ] Criar interface pública `IKitchenService` com método assíncrono:
  - `Task SendToPreparationAsync(Guid orderId, string orderSnapshot)` - Envia pedido para preparação
- [ ] Adicionar comentários XML para documentação:
  - Descrição do método
  - Parâmetros (orderId, orderSnapshot)
  - Exceções que podem ser lançadas (HttpRequestException, etc.)
- [ ] O método deve lançar exceções em caso de erro HTTP (será tratado na implementação)

## Como testar
- Executar `dotnet build` no projeto Application (deve compilar sem erros)
- Verificar que a interface está acessível
- Validar que a interface segue o padrão das outras interfaces (IPaymentGateway, etc.)

## Critérios de aceite
- [ ] Arquivo `IKitchenService.cs` criado em `src/Core/FastFood.PayStream.Application/Ports/`
- [ ] Interface `IKitchenService` criada com método `SendToPreparationAsync`
- [ ] Método recebe `Guid orderId` e `string orderSnapshot`
- [ ] Método retorna `Task` (sem retorno)
- [ ] Comentários XML adicionados
- [ ] Projeto Application compila sem erros
