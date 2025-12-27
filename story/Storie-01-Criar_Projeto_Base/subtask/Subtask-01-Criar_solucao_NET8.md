# Subtask 01: Criar solução .NET 8

## Descrição
Criar a solução .NET 8 `FastFood.PayStream.sln` na raiz do projeto para organizar todos os projetos que serão criados nas próximas subtasks.

## Passos de implementação
- Executar `dotnet new sln -n FastFood.PayStream` na raiz do projeto
- Verificar que o arquivo `FastFood.PayStream.sln` foi criado na raiz
- Confirmar que a solução está vazia (sem projetos ainda)

## Como testar
- Executar `dotnet sln list` na raiz (deve retornar vazio ou apenas a solução)
- Verificar que o arquivo `FastFood.PayStream.sln` existe na raiz do projeto
- Executar `dotnet build` na solução (deve compilar sem erros, mesmo vazia)

## Critérios de aceite
- Arquivo `FastFood.PayStream.sln` criado na raiz do projeto
- Solução compila sem erros (`dotnet build`)
- Solução está vazia (sem projetos ainda)


