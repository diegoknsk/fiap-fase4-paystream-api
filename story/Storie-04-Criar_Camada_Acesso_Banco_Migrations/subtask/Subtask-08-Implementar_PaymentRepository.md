# Subtask 08: Implementar PaymentRepository na Infra.Persistence

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Implementar a classe `PaymentRepository` que implementa `IPaymentRepository`, fazendo o mapeamento entre a entidade de domínio `Payment` e a entidade de persistência `PaymentEntity`, seguindo o padrão do projeto auth-lambda.

## Passos de implementação
- [ ] Criar diretório `src/Infra/FastFood.PayStream.Infra.Persistence/Repositories/` se não existir
- [ ] Criar arquivo `PaymentRepository.cs` no diretório Repositories
- [ ] Definir namespace `FastFood.PayStream.Infra.Persistence.Repositories`
- [ ] Adicionar usings necessários:
  - `Microsoft.EntityFrameworkCore`
  - `FastFood.PayStream.Application.Ports`
  - `FastFood.PayStream.Domain.Entities`
  - `FastFood.PayStream.Domain.Common.Enums`
  - `FastFood.PayStream.Infra.Persistence`
  - `FastFood.PayStream.Infra.Persistence.Entities`
- [ ] Criar classe pública `PaymentRepository` implementando `IPaymentRepository`
- [ ] Adicionar campo privado readonly `PayStreamDbContext _context` no construtor
- [ ] Implementar método `GetByIdAsync(Guid id)`:
  - Buscar `PaymentEntity` no contexto
  - Mapear para `Payment` (domínio) usando método auxiliar
  - Retornar null se não encontrado
- [ ] Implementar método `GetByOrderIdAsync(Guid orderId)`:
  - Buscar `PaymentEntity` por OrderId usando `FirstOrDefaultAsync`
  - Mapear para `Payment` (domínio) usando método auxiliar
  - Retornar null se não encontrado
- [ ] Implementar método `AddAsync(Payment payment)`:
  - Mapear `Payment` (domínio) para `PaymentEntity` usando método auxiliar
  - Adicionar ao contexto
  - Salvar mudanças
  - Retornar `Payment` mapeado de volta
- [ ] Implementar método `UpdateAsync(Payment payment)`:
  - Buscar `PaymentEntity` existente no contexto
  - Atualizar propriedades da entidade
  - Salvar mudanças
- [ ] Implementar método `ExistsAsync(Guid id)`:
  - Verificar se existe usando `AnyAsync`
- [ ] Criar método privado estático `MapToDomain(PaymentEntity entity)`:
  - Converter `PaymentEntity` para `Payment` (domínio)
  - Mapear Status de int para EnumPaymentStatus
  - Usar construtor apropriado da entidade de domínio
- [ ] Criar método privado estático `MapToEntity(Payment payment)`:
  - Converter `Payment` (domínio) para `PaymentEntity`
  - Mapear Status de EnumPaymentStatus para int
  - Mapear todas as propriedades
- [ ] Adicionar comentários XML para documentação

## Como testar
- Executar `dotnet build` no projeto Infra.Persistence (deve compilar sem erros)
- Verificar que a classe implementa `IPaymentRepository`
- Validar que os métodos de mapeamento funcionam corretamente
- Testar que o repositório pode ser instanciado com DbContext
- Verificar que os mapeamentos preservam todos os dados

## Critérios de aceite
- [ ] Arquivo `PaymentRepository.cs` criado em `src/Infra/FastFood.PayStream.Infra.Persistence/Repositories/`
- [ ] Classe `PaymentRepository` implementa `IPaymentRepository`
- [ ] Construtor recebe `PayStreamDbContext` e armazena em campo privado
- [ ] Método `GetByIdAsync` implementado com mapeamento correto
- [ ] Método `GetByOrderIdAsync` implementado com mapeamento correto
- [ ] Método `AddAsync` implementado com mapeamento e SaveChanges
- [ ] Método `UpdateAsync` implementado com atualização e SaveChanges
- [ ] Método `ExistsAsync` implementado
- [ ] Método `MapToDomain` implementado convertendo PaymentEntity para Payment
- [ ] Método `MapToEntity` implementado convertendo Payment para PaymentEntity
- [ ] Mapeamento de Status (int ↔ EnumPaymentStatus) funciona corretamente
- [ ] Comentários XML adicionados
- [ ] Projeto Infra.Persistence compila sem erros
