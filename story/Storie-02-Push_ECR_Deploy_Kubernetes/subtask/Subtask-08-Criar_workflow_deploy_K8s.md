# Subtask 08: Criar GitHub Action para deploy no Kubernetes

## Descrição
Criar o workflow do GitHub Actions para fazer deploy da API e executar o Job do Migrator no cluster Kubernetes (EKS).

## Passos de implementação
- Criar arquivo `.github/workflows/deploy-to-eks.yml`
- Configurar trigger `workflow_dispatch` para execução manual (ou após push para ECR)
- Configurar variáveis de ambiente:
  - `AWS_REGION` (ex: `us-east-1`)
  - `EKS_CLUSTER_NAME` (ex: `eks-paystream`)
  - `KUBERNETES_NAMESPACE` (ex: `paystream`)
  - `ECR_REGISTRY` (ex: `898384491704.dkr.ecr.us-east-1.amazonaws.com`)
  - `IMAGE_TAG` (ex: `${{ github.sha }}` ou `latest`)
- Configurar job `deploy`:
  - Usar `runs-on: ubuntu-latest`
  - Fazer checkout do código
  - Configurar credenciais AWS:
    - Usar `aws-actions/configure-aws-credentials@v4`
    - Configurar `AWS_ACCESS_KEY_ID`, `AWS_SECRET_ACCESS_KEY`, `AWS_SESSION_TOKEN` via secrets
  - Configurar kubectl:
    - Instalar kubectl
    - Configurar acesso ao cluster EKS usando `aws eks update-kubeconfig`
  - Aplicar manifestos Kubernetes na ordem correta:
    1. Namespace (`k8s/infra/namespace.yaml`)
    2. ConfigMap (`k8s/infra/configmap.yaml`)
    3. Deployment da API (`k8s/app/api/api-deployment.yaml`) - substituir tag da imagem
    4. Service da API (`k8s/app/api/api-service.yaml`)
    5. Job do Migrator (`k8s/app/migrator/migrator-job.yaml`) - substituir tag da imagem
  - Validar que o deploy foi bem-sucedido:
    - Verificar status do Deployment
    - Verificar status do Service
    - Verificar status do Job do Migrator
    - Aguardar rollout do Deployment completar
  - Implementar rollback em caso de falha (opcional, mas recomendado)

## Como testar
- Verificar sintaxe YAML do workflow
- Configurar secrets no GitHub (se ainda não configuradas)
- Executar workflow manualmente via GitHub Actions UI
- Verificar que os manifestos são aplicados no cluster
- Verificar que o Deployment da API está rodando
- Verificar que o Service está criado
- Verificar que o Job do Migrator executa e completa com sucesso
- Verificar logs do workflow para garantir que não há erros
- Testar rollback em caso de falha (opcional)

## Critérios de aceite
- Arquivo `.github/workflows/deploy-to-eks.yml` criado
- Workflow configurado com trigger apropriado
- Workflow configura kubectl com acesso ao cluster EKS
- Workflow aplica manifestos Kubernetes na ordem correta
- Workflow substitui tags de imagem nos manifestos antes de aplicar
- Workflow valida que o deploy foi bem-sucedido
- Workflow aguarda rollout do Deployment completar
- Workflow pode ser executado manualmente via GitHub Actions UI
- Deploy no Kubernetes é realizado com sucesso
- API fica disponível após o deploy
- Migrator executa as migrações do banco de dados corretamente
- Health checks da API funcionam corretamente



