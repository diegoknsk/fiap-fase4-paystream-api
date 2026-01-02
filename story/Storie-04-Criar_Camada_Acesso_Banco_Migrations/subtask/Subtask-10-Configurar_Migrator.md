# Subtask 10: Configurar Migrator para executar migrations

## Status
- **Estado:** ✅ Concluída
- **Data de Conclusão:** 02/01/2025

## Descrição
Configurar o projeto Migrator para executar migrations do Entity Framework Core automaticamente, seguindo o padrão do projeto auth-lambda, lendo a connection string do appsettings.json e aplicando migrations pendentes.

## Passos de implementação
- [x] Abrir arquivo `src/InterfacesExternas/FastFood.PayStream.Migrator/Program.cs`
- [x] Adicionar usings necessários:
  - `Microsoft.EntityFrameworkCore`
  - `Microsoft.Extensions.Configuration`
  - `FastFood.PayStream.Infra.Persistence`
- [x] Substituir conteúdo do método `Main` para:
  - Carregar configuração de `appsettings.json` e `appsettings.Development.json`
  - Priorizar variável de ambiente `ConnectionStrings__DefaultConnection` se existir
  - Obter connection string via `configuration.GetConnectionString("DefaultConnection")`
  - Validar que connection string não é nula/vazia (exibir erro e sair se for)
  - Criar `DbContextOptionsBuilder<PayStreamDbContext>` e configurar com `UseNpgsql(connectionString)`
  - Criar instância de `PayStreamDbContext` com as opções
  - Verificar migrations pendentes usando `GetPendingMigrationsAsync()`
  - Se houver migrations pendentes, aplicar usando `MigrateAsync()`
  - Exibir mensagens informativas sobre o processo
  - Tratar exceções e exibir erros apropriadamente
- [x] Adicionar referência ao projeto Infra.Persistence no .csproj do Migrator (se não existir)
- [x] Criar arquivo `appsettings.json` no projeto Migrator com connection string (ou copiar da API)
- [x] Adicionar comentários XML e comentários no código explicando o funcionamento

## Como testar
- Executar `dotnet build` no projeto Migrator (deve compilar sem erros)
- Executar `dotnet run` no projeto Migrator (deve executar e aplicar migrations)
- Verificar que as migrations são aplicadas ao banco de dados
- Verificar que mensagens informativas são exibidas
- Testar com connection string inválida (deve exibir erro apropriado)
- Verificar que o Migrator pode ser executado múltiplas vezes sem problemas (idempotente)

## Critérios de aceite
- [x] `Program.cs` do Migrator carrega configuração de appsettings.json
- [x] Connection string é obtida via `GetConnectionString("DefaultConnection")`
- [x] Variável de ambiente `ConnectionStrings__DefaultConnection` tem prioridade
- [x] Validação de connection string não nula/vazia implementada
- [x] `PayStreamDbContext` é criado com opções configuradas para PostgreSQL
- [x] Migrations pendentes são verificadas usando `GetPendingMigrationsAsync()`
- [x] Migrations são aplicadas usando `MigrateAsync()` quando há pendências
- [x] Mensagens informativas são exibidas durante o processo
- [x] Tratamento de exceções implementado
- [x] Arquivo `appsettings.json` criado no projeto Migrator
- [x] Referência ao projeto Infra.Persistence adicionada no .csproj do Migrator
- [x] Comentários adicionados no código
- [x] Projeto Migrator compila sem erros
- [x] Migrator executa migrations com sucesso quando executado
