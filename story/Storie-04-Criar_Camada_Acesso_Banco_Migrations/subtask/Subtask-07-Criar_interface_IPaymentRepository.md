# Subtask 07: Criar interface IPaymentRepository na Application

## Status
- **Estado:** ✅ Concluída
- **Data de Conclusão:** 02/01/2025

## Descrição
Criar a interface `IPaymentRepository` na camada Application que define o contrato para acesso aos dados de pagamento, seguindo o padrão de Clean Architecture onde a Application define as interfaces e a Infra implementa.

## Passos de implementação
- [x] Criar diretório `src/Core/FastFood.PayStream.Application/Ports/` se não existir
- [x] Criar arquivo `IPaymentRepository.cs` no diretório Ports
- [x] Definir namespace `FastFood.PayStream.Application.Ports`
- [x] Adicionar using para `FastFood.PayStream.Domain.Entities`
- [x] Criar interface pública `IPaymentRepository` com os seguintes métodos assíncronos:
  - `Task<Payment?> GetByIdAsync(Guid id)` - Busca pagamento por ID
  - `Task<Payment?> GetByOrderIdAsync(Guid orderId)` - Busca pagamento por OrderId
  - `Task<Payment> AddAsync(Payment payment)` - Adiciona novo pagamento
  - `Task UpdateAsync(Payment payment)` - Atualiza pagamento existente
  - `Task<bool> ExistsAsync(Guid id)` - Verifica se pagamento existe
- [x] Adicionar comentários XML para documentação da interface e métodos
- [x] Verificar que a interface trabalha com entidade de domínio `Payment`, não com `PaymentEntity`

## Como testar
- Executar `dotnet build` no projeto Application (deve compilar sem erros)
- Verificar que a interface está acessível no namespace correto
- Validar que todos os métodos estão definidos corretamente
- Verificar que os tipos de retorno e parâmetros usam a entidade de domínio `Payment`

## Critérios de aceite
- [x] Arquivo `IPaymentRepository.cs` criado em `src/Core/FastFood.PayStream.Application/Ports/`
- [x] Interface `IPaymentRepository` criada com namespace `FastFood.PayStream.Application.Ports`
- [x] Método `GetByIdAsync(Guid id)` definido retornando `Task<Payment?>`
- [x] Método `GetByOrderIdAsync(Guid orderId)` definido retornando `Task<Payment?>`
- [x] Método `AddAsync(Payment payment)` definido retornando `Task<Payment>`
- [x] Método `UpdateAsync(Payment payment)` definido retornando `Task`
- [x] Método `ExistsAsync(Guid id)` definido retornando `Task<bool>`
- [x] Todos os métodos usam entidade de domínio `Payment` (não PaymentEntity)
- [x] Comentários XML adicionados para documentação
- [x] Projeto Application compila sem erros
