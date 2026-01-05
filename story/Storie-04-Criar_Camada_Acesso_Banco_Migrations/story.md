# Storie-04: Criar Camada de Acesso ao Banco e Migrations

## Status
- **Estado:** ✅ Concluída
- **Data de Conclusão:** 02/01/2025

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

- [x] [Subtask 01: Adicionar pacotes NuGet necessários](./subtask/Subtask-01-Adicionar_pacotes_NuGet.md)
- [x] [Subtask 02: Criar entidade de persistência PaymentEntity](./subtask/Subtask-02-Criar_PaymentEntity.md)
- [x] [Subtask 03: Criar configuração PaymentConfiguration com JSONB](./subtask/Subtask-03-Criar_PaymentConfiguration.md)
- [x] [Subtask 04: Criar DbContext PayStreamDbContext](./subtask/Subtask-04-Criar_PayStreamDbContext.md)
- [x] [Subtask 05: Configurar connection string no appsettings.json](./subtask/Subtask-05-Configurar_connection_string.md)
- [x] [Subtask 06: Registrar DbContext no Program.cs da API](./subtask/Subtask-06-Registrar_DbContext_API.md)
- [x] [Subtask 07: Criar interface IPaymentRepository na Application](./subtask/Subtask-07-Criar_interface_IPaymentRepository.md)
- [x] [Subtask 08: Implementar PaymentRepository na Infra.Persistence](./subtask/Subtask-08-Implementar_PaymentRepository.md)
- [x] [Subtask 09: Criar migration inicial](./subtask/Subtask-09-Criar_migration_inicial.md)
- [x] [Subtask 10: Configurar Migrator para executar migrations](./subtask/Subtask-10-Configurar_Migrator.md)

## Critérios de Aceite da História

- [x] Pacotes NuGet adicionados: Microsoft.EntityFrameworkCore (8.0.0), Microsoft.EntityFrameworkCore.Design (8.0.0), Npgsql.EntityFrameworkCore.PostgreSQL (8.0.0)
- [x] Entidade `PaymentEntity` criada em `src/Infra/FastFood.PayStream.Infra.Persistence/Entities/PaymentEntity.cs` com todas as propriedades
- [x] Configuração `PaymentConfiguration` criada com mapeamento para tabela Payments
- [x] Campo `OrderSnapshot` configurado como JSONB no PostgreSQL
- [x] `PayStreamDbContext` criado herdando de `DbContext`
- [x] `PayStreamDbContext` possui `DbSet<PaymentEntity> Payments`
- [x] Connection string configurada em `appsettings.json` com valores: localhost, porta 5433, database dbPaymentLocal, user postgres, password postgres
- [x] `PayStreamDbContext` registrado no `Program.cs` da API usando `AddDbContext` com `UseNpgsql`
- [x] Interface `IPaymentRepository` criada na camada Application com métodos: GetByIdAsync, GetByOrderIdAsync, AddAsync, UpdateAsync, ExistsAsync
- [x] `PaymentRepository` implementa `IPaymentRepository` e faz mapeamento entre `Payment` (Domain) e `PaymentEntity` (Infra)
- [x] Migration inicial criada e pode ser aplicada ao banco de dados
- [x] Migrator configurado para executar migrations automaticamente (seguindo padrão do projeto auth-lambda)
- [x] Tabela Payments criada no PostgreSQL com estrutura correta (todas as colunas, tipos corretos, JSONB para OrderSnapshot)
- [x] Projeto Infra.Persistence compila sem erros
- [x] Projeto Api compila sem erros
- [x] Projeto Migrator compila sem erros
- [x] Migrator executa migrations com sucesso quando executado
