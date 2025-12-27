# Configuração de CI/CD - PayStream API

Este documento descreve como configurar os secrets e variáveis necessárias para os workflows de CI/CD do projeto PayStream API.

## Índice

- [Secrets do GitHub](#secrets-do-github)
- [Variáveis de Ambiente dos Workflows](#variáveis-de-ambiente-dos-workflows)
- [Secrets do Kubernetes](#secrets-do-kubernetes)
- [Como Executar os Workflows](#como-executar-os-workflows)
- [Testando Localmente](#testando-localmente)
- [Troubleshooting](#troubleshooting)

## Secrets do GitHub

As secrets do GitHub são configuradas no repositório em **Settings > Secrets and variables > Actions**.

### Secrets Obrigatórias

#### AWS_ACCESS_KEY_ID
- **Descrição**: Chave de acesso AWS para autenticação
- **Como obter**: 
  1. Acesse o AWS Console
  2. Vá em IAM > Users > Seu usuário > Security credentials
  3. Clique em "Create access key"
  4. Copie o Access Key ID
- **Uso**: Usado pelos workflows para autenticar na AWS e fazer push no ECR e deploy no EKS

#### AWS_SECRET_ACCESS_KEY
- **Descrição**: Chave secreta AWS para autenticação
- **Como obter**: 
  1. Ao criar a Access Key (passo acima), copie o Secret Access Key
  2. **IMPORTANTE**: Esta chave só é exibida uma vez, salve-a com segurança
- **Uso**: Usado junto com AWS_ACCESS_KEY_ID para autenticação AWS

#### AWS_SESSION_TOKEN
- **Descrição**: Token de sessão AWS (necessário para credenciais temporárias do AWS Academy)
- **Como obter**: 
  1. Se estiver usando AWS Academy, o token é gerado automaticamente ao fazer login
  2. Execute: `aws sts get-session-token` ou obtenha via console do AWS Academy
  3. Copie o valor do campo `SessionToken`
- **Uso**: Usado para autenticação com credenciais temporárias
- **Nota**: Se estiver usando credenciais permanentes, este campo pode ser deixado vazio (mas o workflow ainda espera o secret)

### Como Configurar Secrets no GitHub

1. Acesse o repositório no GitHub
2. Vá em **Settings** > **Secrets and variables** > **Actions**
3. Clique em **New repository secret**
4. Preencha o nome da secret (ex: `AWS_ACCESS_KEY_ID`)
5. Cole o valor da secret
6. Clique em **Add secret**
7. Repita para todas as secrets necessárias

## Variáveis de Ambiente dos Workflows

As variáveis de ambiente são definidas diretamente nos arquivos de workflow (`.github/workflows/*.yml`). 

### Workflow: push-to-ecr.yml

```yaml
env:
  AWS_REGION: us-east-1
  ECR_REPOSITORY_API: paystream-api
  ECR_REPOSITORY_MIGRATOR: paystream-migrator
```

- **AWS_REGION**: Região AWS onde o ECR está configurado (padrão: `us-east-1`)
- **ECR_REPOSITORY_API**: Nome do repositório ECR para a imagem da API
- **ECR_REPOSITORY_MIGRATOR**: Nome do repositório ECR para a imagem do Migrator

### Workflow: deploy-to-eks.yml

```yaml
env:
  AWS_REGION: us-east-1
  EKS_CLUSTER_NAME: eks-paystream
  KUBERNETES_NAMESPACE: paystream
```

- **AWS_REGION**: Região AWS onde o EKS está configurado (padrão: `us-east-1`)
- **EKS_CLUSTER_NAME**: Nome do cluster EKS (padrão: `eks-paystream`)
- **KUBERNETES_NAMESPACE**: Namespace Kubernetes onde a aplicação será implantada (padrão: `paystream`)

**Nota**: Se precisar alterar esses valores, edite os arquivos de workflow diretamente ou configure-os como secrets/variáveis do GitHub para maior flexibilidade.

## Manifestos Kubernetes

**IMPORTANTE**: Os manifestos Kubernetes (Deployment, Service, Job, ConfigMap, Secrets) estão localizados no projeto de infraestrutura: `C:\Projetos\Fiap\fiap-fase4-infra\k8s\app\paystream\`

Este projeto (paystream-api) apenas:
- Faz push das imagens Docker para o ECR
- Atualiza a imagem do deployment existente no Kubernetes
- Cria e executa o Job do Migrator

### Secrets do Kubernetes

Os secrets do Kubernetes devem ser criados no projeto de infraestrutura ou manualmente no cluster. Consulte a documentação do projeto de infra para mais detalhes sobre como criar os secrets.

**Nota**: O workflow de deploy deste projeto assume que os secrets e configmaps já existem no cluster Kubernetes.

## Como Executar os Workflows

### Workflow: Push to ECR

1. Acesse o repositório no GitHub
2. Vá em **Actions** > **Push to ECR**
3. Clique em **Run workflow**
4. (Opcional) Informe uma tag customizada para a imagem
5. Clique em **Run workflow**
6. Aguarde a conclusão do workflow
7. Verifique as imagens no ECR: AWS Console > ECR > Repositories

### Workflow: Deploy to EKS

Este workflow:
1. Valida que as imagens existem no ECR
2. Atualiza a imagem do deployment `paystream-api` no Kubernetes
3. Aguarda o rollout do deployment completar
4. Cria e executa um Job do Migrator (se não marcado para pular)

**Passos para executar:**

1. Acesse o repositório no GitHub
2. Vá em **Actions** > **Deploy to EKS**
3. Clique em **Run workflow**
4. (Opcional) Informe uma tag customizada para a imagem
5. (Opcional) Marque "Skip migrator" se não quiser executar o Migrator
6. Clique em **Run workflow**
7. Aguarde a conclusão do workflow
8. Verifique o deploy: `kubectl get pods -n paystream`

**Nota**: 
- O workflow de deploy valida que as imagens existem no ECR antes de fazer o deploy. Certifique-se de executar o workflow "Push to ECR" primeiro.
- O deployment e outros manifestos Kubernetes devem estar criados no projeto de infraestrutura (`fiap-fase4-infra`). Este workflow apenas atualiza a imagem do deployment existente.

## Testando Localmente

### Testar Dockerfile da API

```bash
# Na raiz do projeto
docker build -t paystream-api:test -f src/InterfacesExternas/FastFood.PayStream.Api/Dockerfile .

# Executar a imagem
docker run -p 8080:80 paystream-api:test

# Testar a API
curl http://localhost:8080/api/hello
```

### Testar Dockerfile do Migrator

```bash
# Na raiz do projeto
docker build -t paystream-migrator:test -f src/InterfacesExternas/FastFood.PayStream.Migrator/Dockerfile .

# Verificar que as migrações estão presentes
docker run --entrypoint ls paystream-migrator:test -la Migrations/

# Verificar appsettings.json
docker run --entrypoint cat paystream-migrator:test appsettings.json
```

### Validar Manifestos Kubernetes

**Nota**: Os manifestos Kubernetes estão no projeto de infraestrutura (`C:\Projetos\Fiap\fiap-fase4-infra\k8s\app\paystream\`). 

Para validar os manifestos, execute no projeto de infra:

```bash
# No projeto de infraestrutura
cd C:\Projetos\Fiap\fiap-fase4-infra
kubectl apply --dry-run=client -f k8s/app/paystream/deployment.yaml
kubectl apply --dry-run=client -f k8s/app/paystream/service.yaml
```

## Troubleshooting

### Erro: "Unable to locate credentials"

**Causa**: Secrets do GitHub não configuradas ou incorretas.

**Solução**: 
1. Verifique se todas as secrets estão configuradas no GitHub
2. Verifique se os valores estão corretos (sem espaços extras)
3. Para AWS Academy, certifique-se de que o `AWS_SESSION_TOKEN` está atualizado (tokens expiram)

### Erro: "Repository does not exist in ECR"

**Causa**: Repositório ECR não foi criado.

**Solução**: 
1. Crie o repositório ECR manualmente ou via Terraform
2. Verifique se o nome do repositório está correto no workflow

### Erro: "Unable to connect to the server"

**Causa**: kubectl não consegue se conectar ao cluster EKS.

**Solução**: 
1. Verifique se o nome do cluster está correto (`EKS_CLUSTER_NAME`)
2. Verifique se as credenciais AWS têm permissão para acessar o EKS
3. Tente configurar o kubeconfig manualmente: `aws eks update-kubeconfig --name <cluster-name> --region <region>`

### Erro: "ImagePullBackOff" no Kubernetes

**Causa**: Pod não consegue fazer pull da imagem do ECR.

**Solução**: 
1. Verifique se a imagem existe no ECR com a tag especificada
2. Verifique se o Node Group do EKS tem permissão para fazer pull do ECR
3. Verifique se o IAM role do Node Group tem a policy `AmazonEC2ContainerRegistryReadOnly`

### Erro: "Job failed" no Migrator

**Causa**: Migrator não consegue se conectar ao banco ou executar migrações.

**Solução**: 
1. Verifique os logs do Job: `kubectl logs job/paystream-migrator -n paystream`
2. Verifique se o secret `paystream-secrets` está criado corretamente
3. Verifique se a connection string do PostgreSQL está correta
4. Verifique se o Security Group do RDS permite conexões do EKS

### Erro: "Health check failed"

**Causa**: API não está respondendo no endpoint `/health`.

**Solução**: 
1. Verifique se a API tem um endpoint `/health` implementado
2. Verifique os logs do pod: `kubectl logs <pod-name> -n paystream`
3. Ajuste os tempos de `initialDelaySeconds` nos probes se necessário

### Workflow trava em "Wait for deployment rollout"

**Causa**: Deployment não está ficando pronto.

**Solução**: 
1. Verifique o status do deployment: `kubectl get deployment paystream-api -n paystream`
2. Verifique os pods: `kubectl get pods -n paystream`
3. Verifique os eventos: `kubectl describe deployment paystream-api -n paystream`
4. Verifique os logs dos pods para identificar o problema

### Como Fazer Rollback Manual

Se o deploy falhar, você pode fazer rollback manual:

```bash
# Rollback do deployment
kubectl rollout undo deployment/paystream-api -n paystream

# Verificar status
kubectl rollout status deployment/paystream-api -n paystream
```

## Referências

- [AWS ECR Documentation](https://docs.aws.amazon.com/ecr/)
- [AWS EKS Documentation](https://docs.aws.amazon.com/eks/)
- [Kubernetes Documentation](https://kubernetes.io/docs/)
- [GitHub Actions Documentation](https://docs.github.com/en/actions)

