# Subtask 04: Criar projeto Infra

## Descrição
Criar o projeto `FastFood.PayStream.Infra` na pasta `src/Core/`, que será a camada de infraestrutura contendo implementações de serviços externos (SQS, Mercado Pago, etc.).

## Passos de implementação
- Criar pasta `src/Core/FastFood.PayStream.Infra/`
- Executar `dotnet new classlib -n FastFood.PayStream.Infra -f net8.0` na pasta criada
- Adicionar referências:
  - `dotnet add reference ../FastFood.PayStream.Application/FastFood.PayStream.Application.csproj`
  - `dotnet add reference ../FastFood.PayStream.Domain/FastFood.PayStream.Domain.csproj`
- Criar estrutura de pastas:
  - `Services/` (para implementações de serviços externos)
- Remover o arquivo `Class1.cs` gerado automaticamente

## Como testar
- Executar `dotnet build` no projeto Infra (deve compilar sem erros)
- Verificar que o arquivo `.csproj` contém referências aos projetos Application e Domain
- Verificar estrutura de pastas criada

## Critérios de aceite
- Projeto `FastFood.PayStream.Infra` criado em `src/Core/FastFood.PayStream.Infra/`
- Estrutura de pastas `Services/` criada
- Referências aos projetos Application e Domain adicionadas
- Projeto compila sem erros (`dotnet build`)
- Arquivo `Class1.cs` removido


