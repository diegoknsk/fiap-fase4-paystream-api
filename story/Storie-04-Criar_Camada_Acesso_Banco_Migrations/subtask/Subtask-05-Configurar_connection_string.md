# Subtask 05: Configurar connection string no appsettings.json

## Status
- **Estado:** ✅ Concluída
- **Data de Conclusão:** 02/01/2025

## Descrição
Configurar a connection string do PostgreSQL no arquivo appsettings.json da API, seguindo o padrão do projeto auth-lambda com os valores especificados: localhost, porta 5433, database dbPaymentLocal, user postgres, password postgres.

## Passos de implementação
- [x] Abrir arquivo `src/InterfacesExternas/FastFood.PayStream.Api/appsettings.json`
- [x] Adicionar seção `ConnectionStrings` se não existir
- [x] Adicionar chave `DefaultConnection` com valor: `Host=localhost;Port=5433;Database=dbPaymentLocal;Username=postgres;Password=postgres`
- [x] Verificar que a formatação JSON está correta (vírgulas, chaves, etc.)
- [ ] Criar arquivo `appsettings.Development.json` se não existir (opcional, para desenvolvimento local)
- [ ] Adicionar mesma connection string no `appsettings.Development.json` (se criado)

## Como testar
- Validar sintaxe JSON do arquivo appsettings.json (usar validador JSON ou `dotnet build`)
- Verificar que a connection string está no formato correto do PostgreSQL
- Executar `dotnet build` no projeto Api (deve compilar sem erros)
- Verificar que a connection string pode ser lida via `IConfiguration.GetConnectionString("DefaultConnection")`

## Critérios de aceite
- [x] Seção `ConnectionStrings` adicionada em `appsettings.json`
- [x] Chave `DefaultConnection` configurada com connection string completa
- [x] Connection string contém: Host=localhost, Port=5433, Database=dbPaymentLocal, Username=postgres, Password=postgres
- [x] Sintaxe JSON válida (arquivo pode ser parseado)
- [x] `dotnet build` no projeto Api executa sem erros
- [x] Connection string pode ser lida via `IConfiguration.GetConnectionString("DefaultConnection")`
