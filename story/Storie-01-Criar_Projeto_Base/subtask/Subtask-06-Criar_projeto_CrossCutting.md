# Subtask 06: Criar projeto CrossCutting

## Descrição
Criar o projeto `FastFood.PayStream.CrossCutting` na pasta `src/Core/`, que será a camada de cross-cutting concerns contendo extensões, configurações compartilhadas e utilitários.

## Passos de implementação
- Criar pasta `src/Core/FastFood.PayStream.CrossCutting/`
- Executar `dotnet new classlib -n FastFood.PayStream.CrossCutting -f net8.0` na pasta criada
- Adicionar referências:
  - `dotnet add reference ../FastFood.PayStream.Application/FastFood.PayStream.Application.csproj`
  - `dotnet add reference ../FastFood.PayStream.Domain/FastFood.PayStream.Domain.csproj`
- Criar estrutura de pastas:
  - `Extensions/` (para extensões como ServiceCollectionExtensions, etc.)
- Remover o arquivo `Class1.cs` gerado automaticamente

## Como testar
- Executar `dotnet build` no projeto CrossCutting (deve compilar sem erros)
- Verificar que o arquivo `.csproj` contém referências aos projetos Application e Domain
- Verificar estrutura de pastas criada

## Critérios de aceite
- Projeto `FastFood.PayStream.CrossCutting` criado em `src/Core/FastFood.PayStream.CrossCutting/`
- Estrutura de pastas `Extensions/` criada
- Referências aos projetos Application e Domain adicionadas
- Projeto compila sem erros (`dotnet build`)
- Arquivo `Class1.cs` removido


