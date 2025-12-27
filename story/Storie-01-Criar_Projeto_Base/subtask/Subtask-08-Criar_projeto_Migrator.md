# Subtask 08: Criar projeto Migrator

## Descrição
Criar o projeto `FastFood.PayStream.Migrator` na pasta `src/InterfacesExternas/`, que será um projeto console para executar migrations do Entity Framework Core como um Kubernetes Job.

## Passos de implementação
- Criar pasta `src/InterfacesExternas/FastFood.PayStream.Migrator/`
- Executar `dotnet new console -n FastFood.PayStream.Migrator -f net8.0` na pasta criada
- Adicionar referências:
  - `dotnet add reference ../../Infra/FastFood.PayStream.Infra.Persistence/FastFood.PayStream.Infra.Persistence.csproj`
  - `dotnet add reference ../../Core/FastFood.PayStream.CrossCutting/FastFood.PayStream.CrossCutting.csproj`
- Criar arquivo `Program.cs` básico (será implementado nas próximas stories quando tivermos migrations)
- O Program.cs inicial pode apenas ter um `Console.WriteLine("Migrator - To be implemented")`

## Como testar
- Executar `dotnet build` no projeto Migrator (deve compilar sem erros)
- Executar `dotnet run` no projeto Migrator (deve executar sem erros, mesmo que apenas mostre uma mensagem)
- Verificar que o arquivo `.csproj` contém referências aos projetos necessários

## Critérios de aceite
- Projeto `FastFood.PayStream.Migrator` criado em `src/InterfacesExternas/FastFood.PayStream.Migrator/`
- Referências aos projetos Infra.Persistence e CrossCutting adicionadas
- Arquivo `Program.cs` criado (pode ser básico por enquanto)
- Projeto compila sem erros (`dotnet build`)
- Projeto executa sem erros (`dotnet run`)


