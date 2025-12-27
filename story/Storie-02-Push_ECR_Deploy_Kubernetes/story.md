# Storie-02: Push para ECR e Deploy no Kubernetes

## Descrição
Como desenvolvedor DevOps, quero configurar o pipeline de CI/CD para fazer push das imagens Docker (API e Migrator) para o Amazon ECR e realizar o deploy automático no cluster Kubernetes (EKS), para que possamos automatizar o processo de publicação e atualização das aplicações em produção.

## Objetivo
Configurar o pipeline completo de CI/CD com GitHub Actions para:
1. Fazer build e push das imagens Docker da API e do Migrator para o Amazon ECR
2. Realizar deploy automático da API como Deployment no Kubernetes
3. Executar o Migrator como Kubernetes Job para aplicação de migrações do banco de dados
4. Garantir que todo o processo seja automatizado e confiável

## Escopo Técnico
- Tecnologias: Docker, Amazon ECR, Kubernetes (EKS), GitHub Actions
- Componentes:
  - Dockerfiles para API e Migrator
  - GitHub Actions workflow para push no ECR
  - GitHub Actions workflow para deploy no Kubernetes
  - Manifestos Kubernetes (Deployment, Service, Job, ConfigMap, Secrets)
- Estrutura:
  - `.github/workflows/push-to-ecr.yml` - Workflow para build e push das imagens
  - `.github/workflows/deploy-to-eks.yml` - Workflow para deploy no Kubernetes
  - `k8s/` - Pasta com manifestos Kubernetes
    - `k8s/app/api/` - Manifestos da API (deployment, service)
    - `k8s/app/migrator/` - Manifestos do Migrator (job)
    - `k8s/infra/` - Manifestos de infraestrutura (namespace, configmap, secrets)

## Regras Obrigatórias
- Respeitar padrões de Docker multi-stage build para otimização de imagens
- Usar tags baseadas em SHA do commit para versionamento de imagens
- Configurar autenticação AWS via Access Key e Secret Key (não usar IAM roles)
- Manifestos Kubernetes devem seguir padrões definidos em `infrarules.mdc`
- Usar namespace específico para o PayStream (não usar default)
- Configurar health checks (liveness e readiness probes) na API
- Job do Migrator deve ser idempotente (pode ser executado múltiplas vezes)
- Secrets e ConfigMaps devem ser configurados para variáveis de ambiente sensíveis
- Deploy deve validar que a imagem existe no ECR antes de tentar fazer deploy
- Implementar estratégia de rollout seguro (RollingUpdate) para a API

## Subtasks

- [Subtask 01: Criar Dockerfile para API](./subtask/Subtask-01-Criar_Dockerfile_API.md)
- [Subtask 02: Criar Dockerfile para Migrator](./subtask/Subtask-02-Criar_Dockerfile_Migrator.md)
- [Subtask 03: Criar estrutura de pastas para manifestos Kubernetes](./subtask/Subtask-03-Criar_estrutura_manifestos_K8s.md)
- [Subtask 04: Criar manifestos Kubernetes para API (Deployment e Service)](./subtask/Subtask-04-Criar_manifestos_API.md)
- [Subtask 05: Criar manifestos Kubernetes para Migrator (Job)](./subtask/Subtask-05-Criar_manifestos_Migrator.md)
- [Subtask 06: Criar manifestos de infraestrutura (Namespace, ConfigMap)](./subtask/Subtask-06-Criar_manifestos_infra.md)
- [Subtask 07: Criar GitHub Action para push no ECR](./subtask/Subtask-07-Criar_workflow_push_ECR.md)
- [Subtask 08: Criar GitHub Action para deploy no Kubernetes](./subtask/Subtask-08-Criar_workflow_deploy_K8s.md)
- [Subtask 09: Configurar documentação de secrets e variáveis necessárias](./subtask/Subtask-09-Documentar_secrets_variáveis.md)

## Critérios de Aceite da História

- [ ] Dockerfile da API criado em `src/InterfacesExternas/FastFood.PayStream.Api/Dockerfile`
- [ ] Dockerfile do Migrator criado em `src/InterfacesExternas/FastFood.PayStream.Migrator/Dockerfile`
- [ ] Dockerfiles usam multi-stage build para otimização
- [ ] Imagens Docker podem ser construídas localmente sem erros
- [ ] Estrutura de pastas `k8s/` criada com organização correta:
  - [ ] `k8s/app/api/` - Manifestos da API
  - [ ] `k8s/app/migrator/` - Manifestos do Migrator
  - [ ] `k8s/infra/` - Manifestos de infraestrutura
- [ ] Manifestos Kubernetes criados:
  - [ ] Namespace `paystream` criado
  - [ ] Deployment da API com configurações corretas (resources, health checks, env vars)
  - [ ] Service da API configurado (LoadBalancer ou ClusterIP conforme necessário)
  - [ ] Job do Migrator configurado como idempotente
  - [ ] ConfigMap para variáveis de ambiente não sensíveis
  - [ ] Documentação sobre Secrets necessários
- [ ] GitHub Action `push-to-ecr.yml` criada em `.github/workflows/`
- [ ] GitHub Action `deploy-to-eks.yml` criada em `.github/workflows/`
- [ ] Workflow de push para ECR:
  - [ ] Faz build das imagens da API e Migrator
  - [ ] Faz push para ECR com tags baseadas em SHA do commit
  - [ ] Usa autenticação AWS via secrets configuradas
  - [ ] Valida que as imagens foram criadas com sucesso
- [ ] Workflow de deploy no Kubernetes:
  - [ ] Configura kubectl com credenciais AWS
  - [ ] Aplica manifestos Kubernetes na ordem correta (namespace → configmap → deployment → service)
  - [ ] Executa Job do Migrator após deploy da API
  - [ ] Valida que o deploy foi bem-sucedido
  - [ ] Implementa rollback em caso de falha
- [ ] Documentação criada sobre:
  - [ ] Secrets necessários no GitHub (AWS_ACCESS_KEY_ID, AWS_SECRET_ACCESS_KEY, AWS_SESSION_TOKEN, etc.)
  - [ ] Variáveis de ambiente necessárias
  - [ ] Como executar os workflows manualmente
  - [ ] Como testar localmente os Dockerfiles
- [ ] Workflows podem ser executados manualmente via GitHub Actions UI
- [ ] Imagens são criadas e publicadas no ECR com sucesso
- [ ] Deploy no Kubernetes é realizado com sucesso
- [ ] API fica disponível após o deploy
- [ ] Migrator executa as migrações do banco de dados corretamente
- [ ] Health checks da API funcionam corretamente
- [ ] Estrutura segue padrões definidos em `infrarules.mdc` e `paystream-context.mdc`



