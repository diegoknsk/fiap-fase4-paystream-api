# Subtask 06: Registrar DbContext no Program.cs da API

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Registrar o `PayStreamDbContext` no container de injeção de dependência no `Program.cs` da API, configurando para usar PostgreSQL com a connection string do appsettings.json, seguindo o padrão do projeto auth-lambda.

## Passos de implementação
- [ ] Abrir arquivo `src/InterfacesExternas/FastFood.PayStream.Api/Program.cs`
- [ ] Adicionar using para `Microsoft.EntityFrameworkCore` e `FastFood.PayStream.Infra.Persistence`
- [ ] Após `var builder = WebApplication.CreateBuilder(args);`, adicionar código para obter connection string:
  - `var dbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");`
- [ ] Adicionar verificação se connection string não é nula/vazia
- [ ] Registrar DbContext usando `builder.Services.AddDbContext<PayStreamDbContext>`:
  - Configurar opções com `options => options.UseNpgsql(dbConnectionString)`
- [ ] Adicionar comentários explicativos no código

## Como testar
- Executar `dotnet build` no projeto Api (deve compilar sem erros)
- Verificar que não há erros de compilação relacionados ao DbContext
- Validar que o DbContext está registrado no container de DI
- Testar que a API inicia sem erros (se possível, executar `dotnet run`)

## Critérios de aceite
- [ ] Using para `Microsoft.EntityFrameworkCore` adicionado
- [ ] Using para `FastFood.PayStream.Infra.Persistence` adicionado
- [ ] Connection string obtida via `builder.Configuration.GetConnectionString("DefaultConnection")`
- [ ] Verificação de connection string não nula/vazia implementada
- [ ] `PayStreamDbContext` registrado usando `AddDbContext<PayStreamDbContext>`
- [ ] Configuração `UseNpgsql(dbConnectionString)` aplicada
- [ ] Comentários adicionados no código
- [ ] Projeto Api compila sem erros
- [ ] DbContext está disponível para injeção de dependência
