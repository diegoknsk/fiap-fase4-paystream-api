# Subtask 01: Adicionar pacotes NuGet necessários

## Status
- **Estado:** ✅ Concluída
- **Data de Conclusão:** 02/01/2025

## Descrição
Adicionar os pacotes NuGet necessários para Entity Framework Core e PostgreSQL no projeto Infra.Persistence, seguindo o padrão do projeto auth-lambda.

## Passos de implementação
- [x] Abrir arquivo `src/Infra/FastFood.PayStream.Infra.Persistence/FastFood.PayStream.Infra.Persistence.csproj`
- [x] Adicionar referência ao projeto Domain: `<ProjectReference Include="..\..\Core\FastFood.PayStream.Domain\FastFood.PayStream.Domain.csproj" />`
- [x] Adicionar pacote `Microsoft.EntityFrameworkCore` versão 8.0.0
- [x] Adicionar pacote `Microsoft.EntityFrameworkCore.Design` versão 8.0.0 com PrivateAssets e IncludeAssets configurados
- [x] Adicionar pacote `Npgsql.EntityFrameworkCore.PostgreSQL` versão 8.0.0
- [x] Executar `dotnet restore` para restaurar os pacotes

## Como testar
- Executar `dotnet restore` no projeto Infra.Persistence (deve restaurar sem erros)
- Executar `dotnet build` no projeto Infra.Persistence (deve compilar sem erros)
- Verificar que os pacotes foram adicionados corretamente no arquivo .csproj
- Verificar que a referência ao projeto Domain foi adicionada

## Critérios de aceite
- [x] Pacote `Microsoft.EntityFrameworkCore` versão 8.0.0 adicionado
- [x] Pacote `Microsoft.EntityFrameworkCore.Design` versão 8.0.0 adicionado com configurações corretas
- [x] Pacote `Npgsql.EntityFrameworkCore.PostgreSQL` versão 8.0.0 adicionado
- [x] Referência ao projeto Domain adicionada
- [x] `dotnet restore` executa sem erros
- [x] `dotnet build` executa sem erros
- [x] Arquivo .csproj está formatado corretamente
