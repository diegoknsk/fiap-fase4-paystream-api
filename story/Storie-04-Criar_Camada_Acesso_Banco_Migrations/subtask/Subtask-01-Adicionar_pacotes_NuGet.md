# Subtask 01: Adicionar pacotes NuGet necessários

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Adicionar os pacotes NuGet necessários para Entity Framework Core e PostgreSQL no projeto Infra.Persistence, seguindo o padrão do projeto auth-lambda.

## Passos de implementação
- [ ] Abrir arquivo `src/Infra/FastFood.PayStream.Infra.Persistence/FastFood.PayStream.Infra.Persistence.csproj`
- [ ] Adicionar referência ao projeto Domain: `<ProjectReference Include="..\..\Core\FastFood.PayStream.Domain\FastFood.PayStream.Domain.csproj" />`
- [ ] Adicionar pacote `Microsoft.EntityFrameworkCore` versão 8.0.0
- [ ] Adicionar pacote `Microsoft.EntityFrameworkCore.Design` versão 8.0.0 com PrivateAssets e IncludeAssets configurados
- [ ] Adicionar pacote `Npgsql.EntityFrameworkCore.PostgreSQL` versão 8.0.0
- [ ] Executar `dotnet restore` para restaurar os pacotes

## Como testar
- Executar `dotnet restore` no projeto Infra.Persistence (deve restaurar sem erros)
- Executar `dotnet build` no projeto Infra.Persistence (deve compilar sem erros)
- Verificar que os pacotes foram adicionados corretamente no arquivo .csproj
- Verificar que a referência ao projeto Domain foi adicionada

## Critérios de aceite
- [ ] Pacote `Microsoft.EntityFrameworkCore` versão 8.0.0 adicionado
- [ ] Pacote `Microsoft.EntityFrameworkCore.Design` versão 8.0.0 adicionado com configurações corretas
- [ ] Pacote `Npgsql.EntityFrameworkCore.PostgreSQL` versão 8.0.0 adicionado
- [ ] Referência ao projeto Domain adicionada
- [ ] `dotnet restore` executa sem erros
- [ ] `dotnet build` executa sem erros
- [ ] Arquivo .csproj está formatado corretamente
