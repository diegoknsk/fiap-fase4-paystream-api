# Subtask 05: Modificar GetReceiptUseCase para chamar serviço de cozinha

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Modificar o GetReceiptUseCase para chamar o serviço de cozinha antes de finalizar o use case, garantindo que o pedido seja enviado para preparação após obter o recibo do gateway.

## Passos de implementação
- [ ] Abrir arquivo `src/Core/FastFood.PayStream.Application/UseCases/GetReceiptUseCase.cs`
- [ ] Adicionar campo privado readonly `IKitchenService _kitchenService` na classe
- [ ] Modificar construtor para receber `IKitchenService kitchenService` como parâmetro
- [ ] Armazenar `kitchenService` no campo privado
- [ ] No método `ExecuteAsync`, após obter o recibo do gateway e antes de retornar o response:
  - Chamar `await _kitchenService.SendToPreparationAsync(payment.OrderId, payment.OrderSnapshot)`
  - Esta chamada deve ser feita ANTES de chamar o presenter e retornar
  - Se ocorrer erro, a exceção será propagada automaticamente (não precisa tratar aqui, o controller tratará)
- [ ] Manter toda a lógica existente do use case
- [ ] Adicionar comentário explicando que o pedido é enviado para a cozinha antes de finalizar

## Fluxo esperado:
1. Validações
2. Buscar Payment por OrderId
3. Validar ExternalTransactionId
4. Obter recibo do gateway
5. Ajustar TotalPaidAmount se fakeCheckout
6. Mapear para OutputModel
7. **ENVIAR PARA COZINHA** ← Nova etapa
8. Retornar Response via Presenter

## Como testar
- Executar `dotnet build` no projeto Application (deve compilar sem erros)
- Verificar que o construtor do UseCase recebe IKitchenService
- Validar que a chamada é feita antes de retornar o response

## Critérios de aceite
- [ ] Campo `_kitchenService` adicionado na classe GetReceiptUseCase
- [ ] Construtor modificado para receber `IKitchenService`
- [ ] Método `ExecuteAsync` chama `_kitchenService.SendToPreparationAsync` antes de retornar
- [ ] Chamada usa `payment.OrderId` e `payment.OrderSnapshot`
- [ ] Chamada é feita após obter recibo e antes de retornar response
- [ ] Exceções são propagadas (não são tratadas no use case)
- [ ] Projeto Application compila sem erros
