# Revisão de Configuração - Deploy PayStream

## ✅ Verificações Realizadas

### 1. Namespace
**Localização**: Projeto de infra (`fiap-fase4-infra`)

```yaml
apiVersion: v1
kind: Namespace
metadata:
  name: paystream
  labels:
    app: paystream
    env: dev
    project: fiap-fase4-infra
```

**Status**: ✅ Correto - O workflow usa `KUBERNETES_NAMESPACE: paystream`

---

### 2. ECR (Elastic Container Registry)

**Workflows configurados:**
- `ECR_REPOSITORY: fiap-fase4-infra-paystream-api` (repositório único)
- `AWS_REGION: us-east-1`

**Registry calculado dinamicamente:**
```bash
ECR_REGISTRY="${ACCOUNT_ID}.dkr.ecr.${AWS_REGION}.amazonaws.com"
```

**Imagens criadas no mesmo repositório:**
- API: `${ECR_REGISTRY}/fiap-fase4-infra-paystream-api:api-${IMAGE_TAG}`
- API: `${ECR_REGISTRY}/fiap-fase4-infra-paystream-api:api-latest`
- Migrator: `${ECR_REGISTRY}/fiap-fase4-infra-paystream-api:migrator-${IMAGE_TAG}`
- Migrator: `${ECR_REGISTRY}/fiap-fase4-infra-paystream-api:migrator-latest`

**Status**: ✅ Correto - Usa um único repositório ECR com tags diferentes para diferenciar as imagens:
- Repositório: `fiap-fase4-infra-paystream-api`
- Tags: `api-*` para API e `migrator-*` para Migrator

---

### 3. Deployment da API

**Localização**: `C:\Projetos\Fiap\fiap-fase4-infra\k8s\app\paystream\deployment.yaml`

**Configuração:**
- **Nome**: `paystream-api` ✅
- **Namespace**: `paystream` ✅
- **Container name**: `api` ✅
- **Image placeholder**: `<ECR_REGISTRY>/paystream-api:<TAG>` ✅
- **Labels**: `app: paystream-api`, `service: paystream` ✅

**Workflow de deploy:**
- Atualiza a imagem usando: `kubectl set image deployment/paystream-api api=${FULL_IMAGE} -n paystream`
- Verifica pods usando: `kubectl get pods -l app=paystream-api -n paystream`

**Status**: ✅ Correto - Alinhado com o workflow

---

### 4. Job do Migrator

**Localização**: `C:\Projetos\Fiap\fiap-fase4-infra\k8s\app\paystream\migrator-job.yaml`

**Configuração do template:**
- **Nome no template**: `paystream-migrator` (fixo)
- **Namespace**: `paystream` ✅
- **Container name**: `migrator` ✅
- **Image placeholder**: `<ECR_REGISTRY>/paystream-migrator:<TAG>` ✅
- **Labels**: `app: paystream-migrator`, `service: paystream` ✅
- **restartPolicy**: `Never` ✅

**Workflow de deploy:**
- **Comportamento**: Jobs não podem ser atualizados no Kubernetes, então o workflow cria um novo job com nome único usando timestamp: `paystream-migrator-$(date +%s)`
- **Imagem**: Substitui o placeholder pela imagem completa do ECR
- **Configuração**: Mantém a mesma estrutura do template (restartPolicy: Never, resources, envFrom, etc.)

**Status**: ✅ Correto - O workflow cria novos jobs com nomes únicos, seguindo o padrão do template

---

## 🔧 Ajustes Realizados

### 1. Correção no label do pod
**Problema**: O workflow estava usando `app=${DEPLOYMENT_NAME}` para buscar pods, mas o label correto é `app=paystream-api`

**Correção**: Ajustado para usar `app=paystream-api` diretamente

### 2. Alinhamento do Job do Migrator
**Ajuste**: O job criado pelo workflow agora segue exatamente a estrutura do template do projeto de infra:
- `restartPolicy: Never` (ao invés de `OnFailure`)
- Labels: `app: paystream-migrator`, `service: paystream`
- Mesmos resources e configurações

---

## ✅ Checklist Final

- [x] Namespace `paystream` configurado corretamente
- [x] Nomes dos repositórios ECR alinhados (`paystream-api`, `paystream-migrator`)
- [x] Deployment `paystream-api` com container `api` configurado
- [x] Workflow atualiza imagem do deployment corretamente
- [x] Job do Migrator criado com nome único e imagem atualizada
- [x] Labels e selectors alinhados entre manifestos e workflows
- [x] ConfigMaps e Secrets referenciados corretamente (`paystream-config`, `paystream-secrets`)

---

## 📝 Observações Importantes

1. **ECR Repository**: Certifique-se de que o repositório ECR foi criado no Terraform com o nome exato:
   - `fiap-fase4-infra-paystream-api`
   
   **Nota**: Ambas as imagens (API e Migrator) são armazenadas no mesmo repositório, diferenciadas por tags:
   - API: tags `api-*` (ex: `api-latest`, `api-abc123`)
   - Migrator: tags `migrator-*` (ex: `migrator-latest`, `migrator-abc123`)

2. **Secrets e ConfigMaps**: Devem ser criados no projeto de infra ou manualmente no cluster antes do primeiro deploy:
   - `paystream-config` (ConfigMap)
   - `paystream-secrets` (Secret)

3. **Jobs do Migrator**: Cada execução cria um novo job com timestamp. Jobs antigos são mantidos até serem limpos manualmente ou pelo TTL (se configurado).

4. **Deployment**: O deployment deve ser criado no projeto de infra antes do primeiro deploy. O workflow apenas atualiza a imagem.

---

## 🚀 Próximos Passos

1. Verificar se os repositórios ECR existem no AWS (criados via Terraform)
2. Verificar se o deployment `paystream-api` existe no cluster (criado via projeto de infra)
3. Verificar se os secrets e configmaps existem no namespace `paystream`
4. Executar workflow "Push to ECR" para publicar as imagens
5. Executar workflow "Deploy to EKS" para atualizar o deployment e executar o migrator

