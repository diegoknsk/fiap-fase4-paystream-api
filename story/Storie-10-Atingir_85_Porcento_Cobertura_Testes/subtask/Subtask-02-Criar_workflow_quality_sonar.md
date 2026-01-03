# Subtask 02: Criar workflow quality.yml com SonarCloud

## Status
- **Estado:** 🔄 Pendente
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Criar o workflow GitHub Actions `quality.yml` para executar build, testes com cobertura, e análise no SonarCloud. O workflow deve validar que a cobertura mínima de 85% seja atingida antes de permitir merge.

## Passos de implementação

### 1. Criar estrutura de diretório
- [ ] Verificar se diretório `.github/workflows/` existe
- [ ] Criar diretório se não existir

### 2. Criar arquivo quality.yml
- [ ] Criar arquivo `.github/workflows/quality.yml`
- [ ] Configurar triggers:
  - Pull requests para branch `main` (opened, synchronize, reopened)
  - Push para branch `main`
- [ ] Configurar job `quality` com runner `ubuntu-latest`

### 3. Configurar steps do workflow
- [ ] Step: Checkout com `fetch-depth: 0`
- [ ] Step: Setup .NET 8.0.x
- [ ] Step: Cache do Sonar
- [ ] Step: Instalar SonarScanner for .NET
- [ ] Step: Sonar Begin (com configuração para PR e branch)
- [ ] Step: Restore da solução
- [ ] Step: Build com símbolos de debug (`/p:DebugType=portable /p:DebugSymbols=true`)
- [ ] Step: Test com cobertura (formato OpenCover)
- [ ] Step: Listar arquivos de cobertura (debug)
- [ ] Step: Consolidar relatórios de cobertura
- [ ] Step: Verificar arquivo de cobertura antes do Sonar End
- [ ] Step: Verificar threshold de 85% de cobertura (script Python)
- [ ] Step: Sonar End

### 4. Configurações críticas
- [ ] Build com símbolos de debug: `/p:DebugType=portable /p:DebugSymbols=true`
- [ ] Formato OpenCover: `/p:CoverletOutputFormat="opencover"`
- [ ] Exclusões de cobertura: `**/*Program.cs,**/*Startup.cs,**/Migrations/**,**/*Dto.cs`
- [ ] Quality Gate wait: `/d:sonar.qualitygate.wait=true`
- [ ] Threshold mínimo: 85% (ajustar script Python)

### 5. Substituir placeholders
- [ ] Substituir `{ORGANIZACAO}` pelo nome da organização no SonarCloud
- [ ] Substituir `{PROJETO}` pelo nome do projeto no SonarCloud (ex: `fiap-fase4-paystream-api`)
- [ ] Substituir `FastFood.{Servico}.sln` por `FastFood.PayStream.sln`

## Estrutura do workflow

O workflow deve seguir a estrutura completa do documento de lições aprendidas, incluindo:
- Configuração diferenciada para PRs vs branches
- Consolidação de múltiplos arquivos de cobertura
- Verificação de threshold antes do Sonar End
- Tratamento de erros adequado

## Como testar
- [ ] Criar PR de teste
- [ ] Verificar que workflow executa automaticamente
- [ ] Verificar que build e testes executam
- [ ] Verificar que cobertura é gerada
- [ ] Verificar que SonarCloud recebe os dados
- [ ] Verificar que Quality Gate é avaliado

## Critérios de aceite
- [ ] Arquivo `.github/workflows/quality.yml` criado
- [ ] Workflow executa em PRs para `main`
- [ ] Workflow executa em push para `main`
- [ ] Build executa com símbolos de debug
- [ ] Testes executam com cobertura em formato OpenCover
- [ ] Arquivos de cobertura são consolidados corretamente
- [ ] Threshold de 85% é verificado antes do Sonar End
- [ ] SonarCloud recebe dados de cobertura
- [ ] Quality Gate é avaliado e bloqueia merges quando necessário
- [ ] Placeholders substituídos corretamente

## Configuração do SonarCloud (pré-requisito)
- [ ] Projeto criado no SonarCloud
- [ ] Secret `SONAR_TOKEN` configurado no GitHub (Settings → Secrets and variables → Actions)
- [ ] Análise Automática desabilitada no SonarCloud (Administration → Analysis Method)

## Referências
- [Documento de Lições Aprendidas - Workflow de Qualidade](./docs/PROMPT_MICROSERVICOS_TESTES_DEPLOY.md#tarefa-2-criar-workflow-de-qualidade-qualityyml)
- [Estrutura Completa do Workflow](./docs/PROMPT_MICROSERVICOS_TESTES_DEPLOY.md#21-estrutura-completa-do-workflow)
