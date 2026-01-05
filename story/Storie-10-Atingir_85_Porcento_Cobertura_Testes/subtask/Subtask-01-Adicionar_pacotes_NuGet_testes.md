# Subtask 01: Adicionar pacotes NuGet necessários para testes

## Status
- **Estado:** 🔄 Pendente
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Adicionar os pacotes NuGet necessários para testes unitários e BDD no projeto de testes, seguindo as especificações do documento de lições aprendidas. Os pacotes incluem FluentAssertions, coverlet.msbuild, e configuração do SpecFlow para testes BDD.

## Passos de implementação

### 1. Atualizar projeto de testes unitários
- [ ] Adicionar `FluentAssertions` versão 6.12.0 ao projeto `FastFood.PayStream.Tests.Unit`
- [ ] Adicionar `coverlet.msbuild` versão 6.0.0 ao projeto de testes unitários
- [ ] Verificar que `coverlet.collector` versão 6.0.0 já está presente (adicionar se não estiver)
- [ ] Verificar versões dos pacotes existentes (xunit, Moq) e atualizar se necessário

### 2. Configurar projeto de testes BDD
- [ ] Verificar se projeto `FastFood.PayStream.Tests.Bdd` existe
- [ ] Adicionar `SpecFlow` versão 3.9.74
- [ ] Adicionar `SpecFlow.xUnit` versão 3.9.74
- [ ] Adicionar `FluentAssertions` versão 6.12.0
- [ ] Adicionar `coverlet.collector` versão 6.0.0
- [ ] Adicionar `coverlet.msbuild` versão 6.0.0
- [ ] Verificar que `xunit` versão 2.6.2 está presente

### 3. Verificar estrutura de referências
- [ ] Verificar que projeto de testes unitários referencia:
  - `FastFood.PayStream.Domain`
  - `FastFood.PayStream.Application`
  - `FastFood.PayStream.Api` (para testes de controllers)
- [ ] Verificar que projeto de testes BDD referencia os projetos necessários

## Comandos a executar

```bash
# No projeto de testes unitários
cd src/tests/FastFood.PayStream.Tests.Unit
dotnet add package FluentAssertions --version 6.12.0
dotnet add package coverlet.msbuild --version 6.0.0

# No projeto de testes BDD
cd src/tests/FastFood.PayStream.Tests.Bdd
dotnet add package SpecFlow --version 3.9.74
dotnet add package SpecFlow.xUnit --version 3.9.74
dotnet add package FluentAssertions --version 6.12.0
dotnet add package coverlet.msbuild --version 6.0.0
dotnet add package coverlet.collector --version 6.0.0
```

## Como testar
- [ ] Executar `dotnet restore` na solução
- [ ] Executar `dotnet build` na solução (deve compilar sem erros)
- [ ] Verificar que os pacotes aparecem nos arquivos `.csproj`
- [ ] Executar `dotnet test` (deve executar sem erros, mesmo que não haja testes ainda)

## Critérios de aceite
- [ ] `FluentAssertions` versão 6.12.0 adicionado ao projeto de testes unitários
- [ ] `coverlet.msbuild` versão 6.0.0 adicionado ao projeto de testes unitários
- [ ] `coverlet.collector` versão 6.0.0 presente no projeto de testes unitários
- [ ] Projeto de testes BDD tem todos os pacotes SpecFlow configurados
- [ ] Projeto de testes BDD tem FluentAssertions e coverlet configurados
- [ ] Solução compila sem erros após adicionar pacotes
- [ ] `dotnet test` executa sem erros

## Referências
- [Documento de Lições Aprendidas - Pacotes NuGet](./docs/PROMPT_MICROSERVICOS_TESTES_DEPLOY.md#pacotes-nuget-obrigatórios)
