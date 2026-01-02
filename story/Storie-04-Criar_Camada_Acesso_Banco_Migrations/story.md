# Storie-04: Criar Camada de Acesso ao Banco e Migrations

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Como desenvolvedor, quero criar a camada de persistência completa com Entity Framework Core e PostgreSQL, incluindo entidade de persistência, configurações, DbContext, migrations e repositório, para que possamos armazenar e recuperar dados de pagamentos no banco de dados PostgreSQL.

## Objetivo
Configurar Entity Framework Core com PostgreSQL, criar a entidade de persistência `PaymentEntity`, configurações de mapeamento (incluindo suporte a JSONB para OrderSnapshot), DbContext `PayStreamDbContext`, interface e implementação do repositório, connection string no appsettings.json, registro do DbContext no Program.cs, migration inicial e configuração do Migrator para executar migrations automaticamente.

## Escopo Técnico
- Tecnologias: .NET 8, Entity Framework Core 8.0, Npgsql.EntityFrameworkCore.PostgreSQL 8.0
- Arquivos afetados:
  - `src/Infra/FastFood.PayStream.Infra.Persistence/Entities/PaymentEntity.cs`
  - `src/Infra/FastFood.PayStream.Infra.Persistence/Configurations/PaymentConfiguration.cs`
  - `src/Infra/FastFood.PayStream.Infra.Persistence/PayStreamDbContext.cs`
  - `src/Infra/FastFood.PayStream.Infra.Persistence/Repositories/PaymentRepository.cs`
  - `src/Core/FastFood.PayStream.Application/Ports/IPaymentRepository.cs`
  - `src/InterfacesExternas/FastFood.PayStream.Api/appsettings.json`
  - `src/InterfacesExternas/FastFood.PayStream.Api/Program.cs`
  - `src/InterfacesExternas/FastFood.PayStream.Migrator/Program.cs`
- Estrutura da tabela Payments:
  - Id (Guid, PK)
  - OrderId (Guid, não nulo)
  - Status (int, não nulo) - representa EnumPaymentStatus
  - ExternalTransactionId (string, nullable)
  - QrCodeUrl (string, nullable)
  - CreatedAt (DateTime, não nulo)
  - TotalAmount (decimal, não nulo)
  - OrderSnapshot (jsonb, não nulo) - JSON com resumo do pedido
- Connection String: `Host=localhost;Port=5433;Database=dbPaymentLocal;Username=postgres;Password=postgres`

## Subtasks

- [ ] [Subtask 01: Adicionar pacotes NuGet necessários](./subtask/Subtask-01-Adicionar_pacotes_NuGet.md)
- [ ] [Subtask 02: Criar entidade de persistência PaymentEntity](./subtask/Subtask-02-Criar_PaymentEntity.md)
- [ ] [Subtask 03: Criar configuração PaymentConfiguration com JSONB](./subtask/Subtask-03-Criar_PaymentConfiguration.md)
- [ ] [Subtask 04: Criar DbContext PayStreamDbContext](./subtask/Subtask-04-Criar_PayStreamDbContext.md)
- [ ] [Subtask 05: Configurar connection string no appsettings.json](./subtask/Subtask-05-Configurar_connection_string.md)
- [ ] [Subtask 06: Registrar DbContext no Program.cs da API](./subtask/Subtask-06-Registrar_DbContext_API.md)
- [ ] [Subtask 07: Criar interface IPaymentRepository na Application](./subtask/Subtask-07-Criar_interface_IPaymentRepository.md)
- [ ] [Subtask 08: Implementar PaymentRepository na Infra.Persistence](./subtask/Subtask-08-Implementar_PaymentRepository.md)
- [ ] [Subtask 09: Criar migration inicial](./subtask/Subtask-09-Criar_migration_inicial.md)
- [ ] [Subtask 10: Configurar Migrator para executar migrations](./subtask/Subtask-10-Configurar_Migrator.md)

## Critérios de Aceite da História

- [ ] Pacotes NuGet adicionados: Microsoft.EntityFrameworkCore (8.0.0), Microsoft.EntityFrameworkCore.Design (8.0.0), Npgsql.EntityFrameworkCore.PostgreSQL (8.0.0)
- [ ] Entidade `PaymentEntity` criada em `src/Infra/FastFood.PayStream.Infra.Persistence/Entities/PaymentEntity.cs` com todas as propriedades
- [ ] Configuração `PaymentConfiguration` criada com mapeamento para tabela Payments
- [ ] Campo `OrderSnapshot` configurado como JSONB no PostgreSQL
- [ ] `PayStreamDbContext` criado herdando de `DbContext`
- [ ] `PayStreamDbContext` possui `DbSet<PaymentEntity> Payments`
- [ ] Connection string configurada em `appsettings.json` com valores: localhost, porta 5433, database dbPaymentLocal, user postgres, password postgres
- [ ] `PayStreamDbContext` registrado no `Program.cs` da API usando `AddDbContext` com `UseNpgsql`
- [ ] Interface `IPaymentRepository` criada na camada Application com métodos: GetByIdAsync, GetByOrderIdAsync, AddAsync, UpdateAsync, ExistsAsync
- [ ] `PaymentRepository` implementa `IPaymentRepository` e faz mapeamento entre `Payment` (Domain) e `PaymentEntity` (Infra)
- [ ] Migration inicial criada e pode ser aplicada ao banco de dados
- [ ] Migrator configurado para executar migrations automaticamente (seguindo padrão do projeto auth-lambda)
- [ ] Tabela Payments criada no PostgreSQL com estrutura correta (todas as colunas, tipos corretos, JSONB para OrderSnapshot)
- [ ] Projeto Infra.Persistence compila sem erros
- [ ] Projeto Api compila sem erros
- [ ] Projeto Migrator compila sem erros
- [ ] Migrator executa migrations com sucesso quando executado
