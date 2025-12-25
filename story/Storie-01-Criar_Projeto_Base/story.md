# Storie-01: Criar Projeto Base com Todas as Camadas e Rota Olá Mundo

## Descrição
Como desenvolvedor, quero criar a estrutura base do projeto FastFoodPayStream com todas as camadas da Clean Architecture implementadas (.NET 8), para que possamos começar a desenvolver as funcionalidades de pagamento sobre uma base sólida e bem organizada.

## Objetivo
Criar a solução .NET 8 com todas as camadas (Domain, Application, Infra, Infra.Persistence, CrossCutting, Api, Migrator) seguindo a arquitetura definida no `paystream-context.mdc`, configurar as dependências entre projetos, e implementar uma rota "Olá Mundo" na API para validar que a estrutura está funcionando corretamente.

## Escopo Técnico
- Tecnologias: .NET 8, ASP.NET Core, Entity Framework Core
- Estrutura de pastas conforme `paystream-context.mdc`:
  - `src/Core/FastFood.PayStream.Domain/`
  - `src/Core/FastFood.PayStream.Application/`
  - `src/Core/FastFood.PayStream.Infra/`
  - `src/Core/FastFood.PayStream.Infra.Persistence/`
  - `src/Core/FastFood.PayStream.CrossCutting/`
  - `src/InterfacesExternas/FastFood.PayStream.Api/`
  - `src/InterfacesExternas/FastFood.PayStream.Migrator/`
  - `src/tests/FastFood.PayStream.Tests.Unit/`
  - `src/tests/FastFood.PayStream.Tests.Bdd/`
- Solução: `FastFood.PayStream.sln`
- API deve ter pelo menos uma rota GET `/api/hello` que retorna "Olá Mundo"

## Regras Obrigatórias
- Respeitar Clean Architecture descrita no contexto base (`baseprojectcontext.mdc` e `paystream-context.mdc`)
- Seguir nomenclatura: `FastFood.PayStream.{Camada}`
- Namespaces seguindo o padrão dos nomes dos projetos
- Todos os projetos devem ser adicionados à solução (.sln)
- API deve ser ASP.NET Core HTTP (não Lambda)
- Configurar Swagger na API para facilitar testes

## Subtasks

- [Subtask 01: Criar solução .NET 8](./subtask/Subtask-01-Criar_solucao_NET8.md)
- [Subtask 02: Criar projeto Domain](./subtask/Subtask-02-Criar_projeto_Domain.md)
- [Subtask 03: Criar projeto Application](./subtask/Subtask-03-Criar_projeto_Application.md)
- [Subtask 04: Criar projeto Infra](./subtask/Subtask-04-Criar_projeto_Infra.md)
- [Subtask 05: Criar projeto Infra.Persistence](./subtask/Subtask-05-Criar_projeto_Infra_Persistence.md)
- [Subtask 06: Criar projeto CrossCutting](./subtask/Subtask-06-Criar_projeto_CrossCutting.md)
- [Subtask 07: Criar projeto Api](./subtask/Subtask-07-Criar_projeto_Api.md)
- [Subtask 08: Criar projeto Migrator](./subtask/Subtask-08-Criar_projeto_Migrator.md)
- [Subtask 09: Configurar dependências entre projetos](./subtask/Subtask-09-Configurar_dependencias_projetos.md)
- [Subtask 10: Implementar rota Olá Mundo na API](./subtask/Subtask-10-Implementar_rota_Ola_Mundo.md)
- [Subtask 11: Criar projetos de testes](./subtask/Subtask-11-Criar_projetos_testes.md)
- [Subtask 12: Adicionar todos os projetos à solução](./subtask/Subtask-12-Adicionar_projetos_solucao.md)

## Critérios de Aceite da História

- [x] Solução `FastFood.PayStream.sln` criada na raiz do projeto
- [x] Todos os projetos Core criados nas pastas corretas:
  - [x] `FastFood.PayStream.Domain`
  - [x] `FastFood.PayStream.Application`
  - [x] `FastFood.PayStream.Infra`
  - [x] `FastFood.PayStream.Infra.Persistence`
  - [x] `FastFood.PayStream.CrossCutting`
- [x] Todos os projetos InterfacesExternas criados:
  - [x] `FastFood.PayStream.Api`
  - [x] `FastFood.PayStream.Migrator`
- [x] Todos os projetos de testes criados:
  - [x] `FastFood.PayStream.Tests.Unit`
  - [x] `FastFood.PayStream.Tests.Bdd`
- [x] Dependências entre projetos configuradas corretamente:
  - [x] Application referencia Domain
  - [x] Infra referencia Application e Domain
  - [x] Infra.Persistence referencia Domain
  - [x] CrossCutting referencia Domain e Application
  - [x] Api referencia Application, CrossCutting e Infra
  - [x] Migrator referencia Infra.Persistence e CrossCutting
  - [x] Tests.Unit referencia todos os projetos necessários
  - [x] Tests.Bdd referencia todos os projetos necessários
- [x] Todos os projetos adicionados à solução (.sln)
- [x] Solução compila sem erros (`dotnet build`)
- [x] API configurada com Swagger
- [x] Rota GET `/api/hello` implementada retornando "Olá Mundo"
- [x] API inicia sem erros (`dotnet run` no projeto Api)
- [x] Swagger acessível e mostrando a rota `/api/hello`
- [x] Teste manual da rota retorna "Olá Mundo" com status 200
- [x] Estrutura de pastas segue o padrão definido em `paystream-context.mdc`
- [x] Nomenclatura de projetos e namespaces seguem o padrão `FastFood.PayStream.*`
- [x] Sem violações críticas de Sonar (se análise já estiver configurada)


