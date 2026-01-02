# Subtask 07: Criar interface IPaymentRepository na Application

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Criar a interface `IPaymentRepository` na camada Application que define o contrato para acesso aos dados de pagamento, seguindo o padrão de Clean Architecture onde a Application define as interfaces e a Infra implementa.

## Passos de implementação
- [ ] Criar diretório `src/Core/FastFood.PayStream.Application/Ports/` se não existir
- [ ] Criar arquivo `IPaymentRepository.cs` no diretório Ports
- [ ] Definir namespace `FastFood.PayStream.Application.Ports`
- [ ] Adicionar using para `FastFood.PayStream.Domain.Entities`
- [ ] Criar interface pública `IPaymentRepository` com os seguintes métodos assíncronos:
  - `Task<Payment?> GetByIdAsync(Guid id)` - Busca pagamento por ID
  - `Task<Payment?> GetByOrderIdAsync(Guid orderId)` - Busca pagamento por OrderId
  - `Task<Payment> AddAsync(Payment payment)` - Adiciona novo pagamento
  - `Task UpdateAsync(Payment payment)` - Atualiza pagamento existente
  - `Task<bool> ExistsAsync(Guid id)` - Verifica se pagamento existe
- [ ] Adicionar comentários XML para documentação da interface e métodos
- [ ] Verificar que a interface trabalha com entidade de domínio `Payment`, não com `PaymentEntity`

## Como testar
- Executar `dotnet build` no projeto Application (deve compilar sem erros)
- Verificar que a interface está acessível no namespace correto
- Validar que todos os métodos estão definidos corretamente
- Verificar que os tipos de retorno e parâmetros usam a entidade de domínio `Payment`

## Critérios de aceite
- [ ] Arquivo `IPaymentRepository.cs` criado em `src/Core/FastFood.PayStream.Application/Ports/`
- [ ] Interface `IPaymentRepository` criada com namespace `FastFood.PayStream.Application.Ports`
- [ ] Método `GetByIdAsync(Guid id)` definido retornando `Task<Payment?>`
- [ ] Método `GetByOrderIdAsync(Guid orderId)` definido retornando `Task<Payment?>`
- [ ] Método `AddAsync(Payment payment)` definido retornando `Task<Payment>`
- [ ] Método `UpdateAsync(Payment payment)` definido retornando `Task`
- [ ] Método `ExistsAsync(Guid id)` definido retornando `Task<bool>`
- [ ] Todos os métodos usam entidade de domínio `Payment` (não PaymentEntity)
- [ ] Comentários XML adicionados para documentação
- [ ] Projeto Application compila sem erros
