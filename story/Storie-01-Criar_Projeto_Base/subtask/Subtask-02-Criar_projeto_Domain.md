# Subtask 02: Criar projeto Domain

## Descrição
Criar o projeto `FastFood.PayStream.Domain` na pasta `src/Core/`, que será a camada de domínio contendo entidades, value objects e regras de negócio puras, sem dependências de infraestrutura.

## Passos de implementação
- Criar pasta `src/Core/FastFood.PayStream.Domain/`
- Executar `dotnet new classlib -n FastFood.PayStream.Domain -f net8.0` na pasta criada
- Criar estrutura de pastas:
  - `Entities/` (para entidades de domínio)
  - `Common/` (para exceções, value objects, etc.)
- Remover o arquivo `Class1.cs` gerado automaticamente
- Verificar que o projeto não referencia nenhuma biblioteca de infraestrutura

## Como testar
- Executar `dotnet build` no projeto Domain (deve compilar sem erros)
- Verificar que o arquivo `.csproj` não contém referências a EF Core, DynamoDB, SQS, etc.
- Verificar estrutura de pastas criada

## Critérios de aceite
- Projeto `FastFood.PayStream.Domain` criado em `src/Core/FastFood.PayStream.Domain/`
- Estrutura de pastas `Entities/` e `Common/` criada
- Projeto compila sem erros (`dotnet build`)
- Projeto não possui dependências de infraestrutura
- Arquivo `Class1.cs` removido



