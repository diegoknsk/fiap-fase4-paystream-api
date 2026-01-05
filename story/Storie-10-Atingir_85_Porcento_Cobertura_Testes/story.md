# Storie-10: Atingir 85% de Cobertura de Testes no SonarCloud

## Status
- **Estado:** 🔄 Pendente
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Como desenvolvedor, quero garantir que o projeto PayStream atinja pelo menos 85% de cobertura de testes no SonarCloud, seguindo as lições aprendidas do projeto OrderHub, para que possamos manter alta qualidade de código e confiabilidade do sistema de pagamento.

## Objetivo
Implementar testes unitários abrangentes para todas as camadas do projeto (Domain, Application, API), configurar o workflow de qualidade com SonarCloud, e garantir que a cobertura mínima de 85% seja atingida e validada no CI/CD. Esta story também inclui a criação de pelo menos um teste BDD para validar um fluxo crítico do sistema.

## Escopo Técnico
- Tecnologias: .NET 8, xUnit, Moq, FluentAssertions, Coverlet, SpecFlow, SonarCloud
- Meta de cobertura: **85% mínimo** (conforme definido no contexto do PayStream)
- Camadas a cobrir:
  - **Domain**: Entidades, Value Objects, DomainValidation, Enums
  - **Application**: UseCases, Presenters, InputModels, OutputModels
  - **API**: Controllers (PaymentController, WebhookPaymentController, HealthController, HelloController)
  - **Infra**: Repositories (se aplicável)
- Configurações necessárias:
  - Workflow GitHub Actions para qualidade (quality.yml)
  - Integração com SonarCloud
  - Configuração de cobertura com Coverlet (formato OpenCover)
  - Validação de threshold de 85% no CI/CD
  - Teste BDD mínimo (SpecFlow)

## Subtasks

- [ ] [Subtask 01: Adicionar pacotes NuGet necessários para testes](./subtask/Subtask-01-Adicionar_pacotes_NuGet_testes.md)
- [ ] [Subtask 02: Criar workflow quality.yml com SonarCloud](./subtask/Subtask-02-Criar_workflow_quality_sonar.md)
- [ ] [Subtask 03: Configurar projeto de testes com Coverlet e FluentAssertions](./subtask/Subtask-03-Configurar_projeto_testes.md)
- [ ] [Subtask 04: Criar testes para UseCases faltantes](./subtask/Subtask-04-Criar_testes_UseCases_faltantes.md)
- [ ] [Subtask 05: Criar testes para Controllers](./subtask/Subtask-05-Criar_testes_Controllers.md)
- [ ] [Subtask 06: Completar testes de Domain (DomainValidation, Payment)](./subtask/Subtask-06-Completar_testes_Domain.md)
- [ ] [Subtask 07: Criar testes para Presenters](./subtask/Subtask-07-Criar_testes_Presenters.md)
- [ ] [Subtask 08: Criar teste BDD básico com SpecFlow](./subtask/Subtask-08-Criar_teste_BDD_SpecFlow.md)
- [ ] [Subtask 09: Configurar SonarCloud e validar integração](./subtask/Subtask-09-Configurar_SonarCloud.md)
- [ ] [Subtask 10: Verificar e ajustar cobertura para atingir 85%](./subtask/Subtask-10-Verificar_ajustar_cobertura.md)

## Critérios de Aceite da História

### Configuração e Infraestrutura
- [ ] Pacotes NuGet necessários adicionados (FluentAssertions, coverlet.msbuild, coverlet.collector)
- [ ] Workflow `quality.yml` criado em `.github/workflows/`
- [ ] Workflow configurado com:
  - Build com símbolos de debug (`/p:DebugType=portable /p:DebugSymbols=true`)
  - Testes com cobertura em formato OpenCover
  - Consolidação de arquivos de cobertura
  - Verificação de threshold de 85% antes do Sonar End
  - Integração com SonarCloud (begin/end)
- [ ] Projeto de testes BDD configurado com SpecFlow
- [ ] SonarCloud configurado com:
  - Projeto criado
  - Análise Automática desabilitada
  - Quality Gate configurado com cobertura mínima de 85%
  - Secret `SONAR_TOKEN` configurado no GitHub

### Cobertura de Testes
- [ ] **Domain**:
  - [ ] Testes para classe `Payment` (construtor, métodos de domínio, validações)
  - [ ] Testes para `DomainValidation` (todos os métodos)
  - [ ] Testes para `EnumPaymentStatus` (se aplicável)
- [ ] **Application**:
  - [ ] Testes para `CreatePaymentUseCase` (já existe, verificar completude)
  - [ ] Testes para `GenerateQrCodeUseCase` (sucesso, falhas, validações)
  - [ ] Testes para `GetReceiptUseCase` (sucesso, falhas, validações)
  - [ ] Testes para `PaymentNotificationUseCase` (sucesso, falhas, validações)
  - [ ] Testes para todos os Presenters (transformações corretas)
- [ ] **API**:
  - [ ] Testes para `PaymentController` (endpoints principais)
  - [ ] Testes para `WebhookPaymentController` (endpoint de webhook)
  - [ ] Testes para `HealthController` (endpoint de health check)
  - [ ] Testes para `HelloController` (endpoint hello)
- [ ] **BDD**:
  - [ ] Pelo menos 1 teste BDD implementado (fluxo crítico)
  - [ ] Teste BDD executável e passando

### Validação e Qualidade
- [ ] `dotnet test` executa todos os testes sem erros
- [ ] Cobertura de código medida localmente (via `dotnet test /p:CollectCoverage=true`)
- [ ] Cobertura mínima de **85%** atingida (verificado no SonarCloud)
- [ ] Workflow de qualidade executa em PRs
- [ ] Quality Gate do SonarCloud passa
- [ ] Workflow bloqueia merges quando cobertura está abaixo de 85%
- [ ] Relatório de cobertura aparece no SonarCloud
- [ ] Exclusões de cobertura configuradas corretamente (`**/*Program.cs,**/*Startup.cs,**/Migrations/**,**/*Dto.cs`)

### Documentação
- [ ] Estrutura de testes documentada (README ou comentários)
- [ ] Padrão AAA (Arrange, Act, Assert) seguido em todos os testes
- [ ] Nomenclatura descritiva nos testes (`[Classe]_[Cenário]_[ResultadoEsperado]`)
- [ ] Testes independentes (podem executar isoladamente)

## Referências

- [Documento de Lições Aprendidas - Testes e Deploy](./docs/PROMPT_MICROSERVICOS_TESTES_DEPLOY.md)
- [Regras de Arquitetura - Testes](./rules/ARCHITECTURE_RULES.md)
- [Contexto PayStream - Critérios de Aceite Testes](./rules/paystream-context.mdc)

## Notas Importantes

1. **Build com símbolos de debug é CRÍTICO**: Sem `/p:DebugType=portable /p:DebugSymbols=true`, a cobertura não será processada corretamente pelo SonarCloud
2. **Formato OpenCover**: Único formato suportado pelo SonarCloud para .NET
3. **Desabilitar Análise Automática no SonarCloud**: OBRIGATÓRIO para evitar conflitos com CI/CD
4. **Consolidação de arquivos**: Coverlet gera arquivos em múltiplos locais, é necessário consolidar em um único arquivo antes do Sonar End
5. **Verificação antes do Sonar End**: Sempre verificar se o arquivo de cobertura existe e é válido
6. **Estrutura de testes deve espelhar código de produção**: Facilita manutenção e localização de testes
7. **SEMPRE executar testes após criá-los**: Execute `dotnet test` para verificar compilação e execução
