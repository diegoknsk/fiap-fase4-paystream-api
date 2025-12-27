# Subtask 12: Adicionar todos os projetos à solução

## Descrição
Adicionar todos os projetos criados ao arquivo de solução (.sln) para que possam ser gerenciados e visualizados no IDE (Visual Studio, Rider, VS Code, etc.).

## Passos de implementação
- Adicionar todos os projetos Core à solução:
  - `dotnet sln add src/Core/FastFood.PayStream.Domain/FastFood.PayStream.Domain.csproj`
  - `dotnet sln add src/Core/FastFood.PayStream.Application/FastFood.PayStream.Application.csproj`
  - `dotnet sln add src/Core/FastFood.PayStream.Infra/FastFood.PayStream.Infra.csproj`
  - `dotnet sln add src/Core/FastFood.PayStream.Infra.Persistence/FastFood.PayStream.Infra.Persistence.csproj`
  - `dotnet sln add src/Core/FastFood.PayStream.CrossCutting/FastFood.PayStream.CrossCutting.csproj`
- Adicionar projetos InterfacesExternas:
  - `dotnet sln add src/InterfacesExternas/FastFood.PayStream.Api/FastFood.PayStream.Api.csproj`
  - `dotnet sln add src/InterfacesExternas/FastFood.PayStream.Migrator/FastFood.PayStream.Migrator.csproj`
- Adicionar projetos de testes:
  - `dotnet sln add src/tests/FastFood.PayStream.Tests.Unit/FastFood.PayStream.Tests.Unit.csproj`
  - `dotnet sln add src/tests/FastFood.PayStream.Tests.Bdd/FastFood.PayStream.Tests.Bdd.csproj`
- Verificar que todos os projetos aparecem na solução: `dotnet sln list`

## Como testar
- Executar `dotnet sln list` na raiz (deve listar todos os 9 projetos)
- Executar `dotnet build` na solução (deve compilar todos os projetos)
- Abrir a solução no IDE e verificar que todos os projetos aparecem no Solution Explorer
- Verificar que não há erros de referência

## Critérios de aceite
- Todos os 9 projetos adicionados à solução:
  - [ ] FastFood.PayStream.Domain
  - [ ] FastFood.PayStream.Application
  - [ ] FastFood.PayStream.Infra
  - [ ] FastFood.PayStream.Infra.Persistence
  - [ ] FastFood.PayStream.CrossCutting
  - [ ] FastFood.PayStream.Api
  - [ ] FastFood.PayStream.Migrator
  - [ ] FastFood.PayStream.Tests.Unit
  - [ ] FastFood.PayStream.Tests.Bdd
- `dotnet sln list` mostra todos os projetos
- `dotnet build` compila todos os projetos sem erros
- Solução pode ser aberta no IDE sem problemas


