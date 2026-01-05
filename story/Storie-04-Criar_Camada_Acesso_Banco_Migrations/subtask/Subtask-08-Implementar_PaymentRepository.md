# Subtask 08: Implementar PaymentRepository na Infra.Persistence

## Status
- **Estado:** ✅ Concluída
- **Data de Conclusão:** 02/01/2025

## Descrição
Implementar a classe `PaymentRepository` que implementa `IPaymentRepository`, fazendo o mapeamento entre a entidade de domínio `Payment` e a entidade de persistência `PaymentEntity`, seguindo o padrão do projeto auth-lambda.

## Passos de implementação
- [x] Criar diretório `src/Infra/FastFood.PayStream.Infra.Persistence/Repositories/` se não existir
- [x] Criar arquivo `PaymentRepository.cs` no diretório Repositories
- [x] Definir namespace `FastFood.PayStream.Infra.Persistence.Repositories`
- [x] Adicionar usings necessários:
  - `Microsoft.EntityFrameworkCore`
  - `FastFood.PayStream.Application.Ports`
  - `FastFood.PayStream.Domain.Entities`
  - `FastFood.PayStream.Domain.Common.Enums`
  - `FastFood.PayStream.Infra.Persistence`
  - `FastFood.PayStream.Infra.Persistence.Entities`
- [x] Criar classe pública `PaymentRepository` implementando `IPaymentRepository`
- [x] Adicionar campo privado readonly `PayStreamDbContext _context` no construtor
- [x] Implementar método `GetByIdAsync(Guid id)`:
  - Buscar `PaymentEntity` no contexto
  - Mapear para `Payment` (domínio) usando método auxiliar
  - Retornar null se não encontrado
- [x] Implementar método `GetByOrderIdAsync(Guid orderId)`:
  - Buscar `PaymentEntity` por OrderId usando `FirstOrDefaultAsync`
  - Mapear para `Payment` (domínio) usando método auxiliar
  - Retornar null se não encontrado
- [x] Implementar método `AddAsync(Payment payment)`:
  - Mapear `Payment` (domínio) para `PaymentEntity` usando método auxiliar
  - Adicionar ao contexto
  - Salvar mudanças
  - Retornar `Payment` mapeado de volta
- [x] Implementar método `UpdateAsync(Payment payment)`:
  - Buscar `PaymentEntity` existente no contexto
  - Atualizar propriedades da entidade
  - Salvar mudanças
- [x] Implementar método `ExistsAsync(Guid id)`:
  - Verificar se existe usando `AnyAsync`
- [x] Criar método privado estático `MapToDomain(PaymentEntity entity)`:
  - Converter `PaymentEntity` para `Payment` (domínio)
  - Mapear Status de int para EnumPaymentStatus
  - Usar construtor apropriado da entidade de domínio
- [x] Criar método privado estático `MapToEntity(Payment payment)`:
  - Converter `Payment` (domínio) para `PaymentEntity`
  - Mapear Status de EnumPaymentStatus para int
  - Mapear todas as propriedades
- [x] Adicionar comentários XML para documentação

## Como testar
- Executar `dotnet build` no projeto Infra.Persistence (deve compilar sem erros)
- Verificar que a classe implementa `IPaymentRepository`
- Validar que os métodos de mapeamento funcionam corretamente
- Testar que o repositório pode ser instanciado com DbContext
- Verificar que os mapeamentos preservam todos os dados

## Critérios de aceite
- [x] Arquivo `PaymentRepository.cs` criado em `src/Infra/FastFood.PayStream.Infra.Persistence/Repositories/`
- [x] Classe `PaymentRepository` implementa `IPaymentRepository`
- [x] Construtor recebe `PayStreamDbContext` e armazena em campo privado
- [x] Método `GetByIdAsync` implementado com mapeamento correto
- [x] Método `GetByOrderIdAsync` implementado com mapeamento correto
- [x] Método `AddAsync` implementado com mapeamento e SaveChanges
- [x] Método `UpdateAsync` implementado com atualização e SaveChanges
- [x] Método `ExistsAsync` implementado
- [x] Método `MapToDomain` implementado convertendo PaymentEntity para Payment
- [x] Método `MapToEntity` implementado convertendo Payment para PaymentEntity
- [x] Mapeamento de Status (int ↔ EnumPaymentStatus) funciona corretamente
- [x] Comentários XML adicionados
- [x] Projeto Infra.Persistence compila sem erros
