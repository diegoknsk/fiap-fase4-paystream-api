# Subtask 03: Criar projeto Application

## Descrição
Criar o projeto `FastFood.PayStream.Application` na pasta `src/Core/`, que será a camada de aplicação contendo use cases, commands, responses, presenters e interfaces de gateways (ports).

## Passos de implementação
- Criar pasta `src/Core/FastFood.PayStream.Application/`
- Executar `dotnet new classlib -n FastFood.PayStream.Application -f net8.0` na pasta criada
- Adicionar referência ao projeto Domain: `dotnet add reference ../FastFood.PayStream.Domain/FastFood.PayStream.Domain.csproj`
- Criar estrutura de pastas:
  - `Commands/` (para commands de operações)
  - `UseCases/` (para casos de uso)
  - `InputModels/` (para modelos de entrada dos UseCases)
  - `OutputModels/` (para modelos de saída dos UseCases)
  - `Responses/` (para ResponseModels - Application Responses)
  - `Presenters/` (para presenters de transformação)
  - `Ports/` (para interfaces de Gateways)
- Remover o arquivo `Class1.cs` gerado automaticamente
- Verificar que o projeto referencia apenas Domain

## Como testar
- Executar `dotnet build` no projeto Application (deve compilar sem erros)
- Verificar que o arquivo `.csproj` contém referência ao projeto Domain
- Verificar que não há referências diretas a infraestrutura (EF Core, SQS, etc.)
- Verificar estrutura de pastas criada

## Critérios de aceite
- Projeto `FastFood.PayStream.Application` criado em `src/Core/FastFood.PayStream.Application/`
- Estrutura de pastas criada (Commands, UseCases, InputModels, OutputModels, Responses, Presenters, Ports)
- Referência ao projeto Domain adicionada
- Projeto compila sem erros (`dotnet build`)
- Projeto não possui dependências de infraestrutura (apenas Domain)
- Arquivo `Class1.cs` removido

