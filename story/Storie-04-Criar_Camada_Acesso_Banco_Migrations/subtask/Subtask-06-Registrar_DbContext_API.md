# Subtask 06: Registrar DbContext no Program.cs da API

## Status
- **Estado:** ✅ Concluída
- **Data de Conclusão:** 02/01/2025

## Descrição
Registrar o `PayStreamDbContext` no container de injeção de dependência no `Program.cs` da API, configurando para usar PostgreSQL com a connection string do appsettings.json, seguindo o padrão do projeto auth-lambda.

## Passos de implementação
- [x] Abrir arquivo `src/InterfacesExternas/FastFood.PayStream.Api/Program.cs`
- [x] Adicionar using para `Microsoft.EntityFrameworkCore` e `FastFood.PayStream.Infra.Persistence`
- [x] Após `var builder = WebApplication.CreateBuilder(args);`, adicionar código para obter connection string:
  - `var dbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");`
- [x] Adicionar verificação se connection string não é nula/vazia
- [x] Registrar DbContext usando `builder.Services.AddDbContext<PayStreamDbContext>`:
  - Configurar opções com `options => options.UseNpgsql(dbConnectionString)`
- [x] Adicionar comentários explicativos no código

## Como testar
- Executar `dotnet build` no projeto Api (deve compilar sem erros)
- Verificar que não há erros de compilação relacionados ao DbContext
- Validar que o DbContext está registrado no container de DI
- Testar que a API inicia sem erros (se possível, executar `dotnet run`)

## Critérios de aceite
- [x] Using para `Microsoft.EntityFrameworkCore` adicionado
- [x] Using para `FastFood.PayStream.Infra.Persistence` adicionado
- [x] Connection string obtida via `builder.Configuration.GetConnectionString("DefaultConnection")`
- [x] Verificação de connection string não nula/vazia implementada
- [x] `PayStreamDbContext` registrado usando `AddDbContext<PayStreamDbContext>`
- [x] Configuração `UseNpgsql(dbConnectionString)` aplicada
- [x] Comentários adicionados no código
- [x] Projeto Api compila sem erros
- [x] DbContext está disponível para injeção de dependência
