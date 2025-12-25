# Subtask 11: Criar projetos de testes

## Descrição
Criar os projetos de testes unitários e BDD para o PayStream, seguindo a estrutura definida no contexto do projeto.

## Passos de implementação
- Criar pasta `src/tests/`
- Criar projeto de testes unitários:
  - Executar `dotnet new xunit -n FastFood.PayStream.Tests.Unit -f net8.0` em `src/tests/`
  - Adicionar referências aos projetos que serão testados (Domain, Application, etc.)
- Criar projeto de testes BDD:
  - Executar `dotnet new xunit -n FastFood.PayStream.Tests.Bdd -f net8.0` em `src/tests/`
  - Adicionar pacote SpecFlow (será configurado nas próximas stories): `dotnet add package SpecFlow`
  - Adicionar referências aos projetos necessários
- Criar uma classe de teste básica em cada projeto para validar que estão funcionando
- Remover arquivos de exemplo gerados automaticamente se necessário

## Como testar
- Executar `dotnet test` na solução (deve executar os testes, mesmo que vazios)
- Executar `dotnet test` em cada projeto de teste individualmente
- Verificar que os projetos compilam sem erros

## Critérios de aceite
- Projeto `FastFood.PayStream.Tests.Unit` criado em `src/tests/FastFood.PayStream.Tests.Unit/`
- Projeto `FastFood.PayStream.Tests.Bdd` criado em `src/tests/FastFood.PayStream.Tests.Bdd/`
- Projetos de testes compilam sem erros (`dotnet build`)
- `dotnet test` executa sem erros (mesmo que não haja testes ainda)
- Estrutura básica de testes criada

