# Subtask 09: Configurar dependências entre projetos

## Descrição
Verificar e ajustar todas as dependências entre os projetos criados, garantindo que seguem a Clean Architecture e que não há dependências circulares ou violações de camadas.

## Passos de implementação
- Revisar todas as referências de projetos criadas nas subtasks anteriores
- Verificar que Domain não referencia ninguém
- Verificar que Application referencia apenas Domain
- Verificar que Infra (em `src/Infra/`) referencia Application e Domain
- Verificar que Infra.Persistence (em `src/Infra/`) referencia apenas Domain
- Verificar que CrossCutting referencia Application e Domain
- Verificar que Api referencia Application, CrossCutting e Infra (não deve referenciar Domain diretamente)
- Verificar que Migrator referencia Infra.Persistence e CrossCutting
- Executar `dotnet build` na solução inteira para verificar se há erros de dependência

## Como testar
- Executar `dotnet build` na raiz do projeto (deve compilar todos os projetos sem erros)
- Verificar que não há dependências circulares
- Verificar que Domain não tem dependências de infraestrutura
- Verificar que Application não tem dependências de infraestrutura
- Verificar que Api não acessa Domain diretamente (apenas via Application)

## Critérios de aceite
- Todas as dependências entre projetos estão corretas conforme Clean Architecture
- Domain não referencia nenhum outro projeto
- Application referencia apenas Domain
- Infra (em `src/Infra/`) referencia Application e Domain
- Infra.Persistence (em `src/Infra/`) referencia apenas Domain
- CrossCutting referencia Application e Domain
- Api referencia Application, CrossCutting e Infra (não Domain diretamente)
- Migrator referencia Infra.Persistence e CrossCutting
- Solução compila sem erros (`dotnet build`)
- Não há dependências circulares


