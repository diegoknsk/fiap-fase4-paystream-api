# Subtask 05: Criar projeto Infra.Persistence

## Descrição
Criar o projeto `FastFood.PayStream.Infra.Persistence` na pasta `src/Core/`, que será a camada de persistência contendo repositórios, DbContext, configurações do Entity Framework Core e migrations para PostgreSQL.

## Passos de implementação
- Criar pasta `src/Core/FastFood.PayStream.Infra.Persistence/`
- Executar `dotnet new classlib -n FastFood.PayStream.Infra.Persistence -f net8.0` na pasta criada
- Adicionar referências:
  - `dotnet add reference ../FastFood.PayStream.Domain/FastFood.PayStream.Domain.csproj`
  - `dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL` (versão compatível com .NET 8)
  - `dotnet add package Microsoft.EntityFrameworkCore.Design` (para migrations)
- Criar estrutura de pastas:
  - `Repositories/` (para implementações de repositórios)
  - `Entities/` (para entidades de persistência, se necessário)
  - `Configurations/` (para configurações EF Core - Fluent API)
  - `Migrations/` (para migrations do Entity Framework)
- Criar arquivo `PayStreamDbContext.cs` na raiz do projeto (será implementado nas próximas stories)
- Remover o arquivo `Class1.cs` gerado automaticamente

## Como testar
- Executar `dotnet restore` no projeto (deve baixar os pacotes sem erros)
- Executar `dotnet build` no projeto Infra.Persistence (deve compilar sem erros)
- Verificar que os pacotes NuGet foram adicionados ao `.csproj`
- Verificar estrutura de pastas criada

## Critérios de aceite
- Projeto `FastFood.PayStream.Infra.Persistence` criado em `src/Core/FastFood.PayStream.Infra.Persistence/`
- Estrutura de pastas criada (Repositories, Entities, Configurations, Migrations)
- Pacote `Npgsql.EntityFrameworkCore.PostgreSQL` adicionado
- Pacote `Microsoft.EntityFrameworkCore.Design` adicionado
- Referência ao projeto Domain adicionada
- Arquivo `PayStreamDbContext.cs` criado (vazio por enquanto)
- Projeto compila sem erros (`dotnet build`)
- Arquivo `Class1.cs` removido

