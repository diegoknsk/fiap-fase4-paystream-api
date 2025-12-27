# Prompt para Criação/Alteração da Story 02 - Push ECR e Deploy Kubernetes

## Contexto

Este prompt deve ser usado para criar ou atualizar a **Story 02: Push para ECR e Deploy no Kubernetes** para outros microserviços (OrderHub, KitchenFlow, etc.), baseado nas lições aprendidas durante a implementação do PayStream.

## Informações do Microserviço

**IMPORTANTE**: Substitua os valores abaixo pelos dados do microserviço atual:

- **Nome do Microserviço**: `[NOME]` (ex: `orderhub`, `kitchenflow`)
- **Nome do ECR Repository**: `fiap-fase4-infra-[NOME]-api` (ex: `fiap-fase4-infra-orderhub-api`)
- **Namespace Kubernetes**: `[NOME]` (ex: `orderhub`, `kitchenflow`)
- **Nome do Deployment**: `[NOME]-api` (ex: `orderhub-api`, `kitchenflow-api`)
- **Nome do Container**: `api`
- **Nome do Job**: `[NOME]-migrator` (ex: `orderhub-migrator`)

## Lições Aprendidas e Padrões Obrigatórios

### 1. Estrutura de Repositório ECR

**CRÍTICO**: Usar **UM ÚNICO repositório ECR** para ambas as imagens (API e Migrator) do mesmo microserviço, diferenciando por **tags**:

- **Repositório único**: `fiap-fase4-infra-[NOME]-api`
- **Tags da API**: `api-${TAG}` e `api-latest`
- **Tags do Migrator**: `migrator-${TAG}` e `migrator-latest`

**Exemplo**:
- Repositório: `fiap-fase4-infra-orderhub-api`
- API: `058264347413.dkr.ecr.us-east-1.amazonaws.com/fiap-fase4-infra-orderhub-api:api-abc123`
- Migrator: `058264347413.dkr.ecr.us-east-1.amazonaws.com/fiap-fase4-infra-orderhub-api:migrator-abc123`

### 2. Manifestos Kubernetes

**IMPORTANTE**: Os manifestos Kubernetes (Deployment, Service, Job, ConfigMap, Secrets) **NÃO ficam no projeto da aplicação**. Eles ficam no projeto de infraestrutura: `C:\Projetos\Fiap\fiap-fase4-infra\k8s\app\[NOME]\`

O projeto da aplicação apenas:
- Faz push das imagens Docker para o ECR
- Atualiza a imagem do deployment existente no Kubernetes usando `kubectl set image`
- Cria e executa o Job do Migrator com nome único (timestamp)

### 3. Dockerfile do Migrator - Tratamento de Migrações Vazias

**PROBLEMA COMUM**: A pasta `Migrations/` pode estar vazia inicialmente, causando erro no build.

**SOLUÇÃO**: O Dockerfile do Migrator deve:
1. No build stage, copiar migrações para `/migrations`
2. Se a pasta estiver vazia, criar um arquivo `.keep` para garantir que a pasta não esteja vazia
3. No runtime stage, copiar do build stage (sempre terá pelo menos o `.keep`)

**Template do Dockerfile do Migrator**:
```dockerfile
# Etapa 1: build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copiar arquivos .csproj e fazer restore (otimização de cache)
COPY src/Core/[PROJETO].Domain/[PROJETO].Domain.csproj src/Core/[PROJETO].Domain/
COPY src/Core/[PROJETO].Application/[PROJETO].Application.csproj src/Core/[PROJETO].Application/
COPY src/Core/[PROJETO].CrossCutting/[PROJETO].CrossCutting.csproj src/Core/[PROJETO].CrossCutting/
COPY src/Infra/[PROJETO].Infra/[PROJETO].Infra.csproj src/Infra/[PROJETO].Infra/
COPY src/Infra/[PROJETO].Infra.Persistence/[PROJETO].Infra.Persistence.csproj src/Infra/[PROJETO].Infra.Persistence/
COPY src/InterfacesExternas/[PROJETO].Migrator/[PROJETO].Migrator.csproj src/InterfacesExternas/[PROJETO].Migrator/

RUN dotnet restore src/InterfacesExternas/[PROJETO].Migrator/[PROJETO].Migrator.csproj

# Copiar todo o código
COPY . .

# Publicar o Migrator
RUN dotnet publish src/InterfacesExternas/[PROJETO].Migrator/[PROJETO].Migrator.csproj -c Release -o /out \
    /p:CopyOutputSymbolsToPublishDirectory=false \
    /p:CopyOutputXmlDocumentationToPublishDirectory=false

# Preparar migrações: copiar para pasta temporária, garantindo que sempre exista
RUN mkdir -p /migrations && \
    if [ -d "src/Infra/[PROJETO].Infra.Persistence/Migrations" ] && [ "$(find src/Infra/[PROJETO].Infra.Persistence/Migrations -type f 2>/dev/null | wc -l)" -gt 0 ]; then \
        cp -r src/Infra/[PROJETO].Infra.Persistence/Migrations/* /migrations/ 2>/dev/null || true; \
    fi && \
    # Garantir que a pasta não esteja vazia (evita erro no COPY)
    if [ ! "$(ls -A /migrations 2>/dev/null)" ]; then \
        touch /migrations/.keep; \
    fi

# Etapa 2: imagem final para execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /out ./

# Copiar arquivos de configuração
COPY src/InterfacesExternas/[PROJETO].Migrator/appsettings.json ./appsettings.json

# Copiar migrações do build stage (sempre terá pelo menos o arquivo .keep)
COPY --from=build /migrations ./Migrations

ENTRYPOINT ["dotnet", "[PROJETO].Migrator.dll"]
```

### 4. Workflow Push to ECR

**Variáveis de ambiente**:
```yaml
env:
  AWS_REGION: us-east-1
  ECR_REPOSITORY: fiap-fase4-infra-[NOME]-api
```

**Build e Push**:
- API: `${ECR_REGISTRY}/${ECR_REPOSITORY}:api-${IMAGE_TAG}` e `:api-latest`
- Migrator: `${ECR_REGISTRY}/${ECR_REPOSITORY}:migrator-${IMAGE_TAG}` e `:migrator-latest`

**Validação**:
- Verificar tags `api-${IMAGE_TAG}` e `migrator-${IMAGE_TAG}` no mesmo repositório

### 5. Workflow Deploy to EKS

**Variáveis de ambiente**:
```yaml
env:
  AWS_REGION: us-east-1
  EKS_CLUSTER_NAME: eks-paystream  # Verificar se é o mesmo cluster ou diferente
  KUBERNETES_NAMESPACE: [NOME]
  DEPLOYMENT_NAME: [NOME]-api
  CONTAINER_NAME: api
```

**Validação de imagens**:
- Verificar `api-${IMAGE_TAG}` e `migrator-${IMAGE_TAG}` no repositório `fiap-fase4-infra-[NOME]-api`

**Atualização do Deployment**:
```bash
FULL_IMAGE="${ECR_REGISTRY}/${ECR_REPOSITORY}:api-${IMAGE_TAG}"
kubectl set image deployment/${DEPLOYMENT_NAME} ${CONTAINER_NAME}=${FULL_IMAGE} -n ${KUBERNETES_NAMESPACE}
```

**Criação do Job do Migrator**:
- Criar job com nome único: `[NOME]-migrator-$(date +%s)`
- Usar imagem: `${ECR_REGISTRY}/${ECR_REPOSITORY}:migrator-${IMAGE_TAG}`
- Seguir estrutura do template do projeto de infra (restartPolicy: Never, labels, etc.)

### 6. Health Check Endpoint

**OBRIGATÓRIO**: A API deve ter um endpoint `/health` para os health checks do Kubernetes.

**Controller exemplo**:
```csharp
using Microsoft.AspNetCore.Mvc;

namespace [PROJETO].Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { status = "healthy", timestamp = DateTime.UtcNow });
    }
}
```

### 7. appsettings.json do Migrator

**OBRIGATÓRIO**: O projeto Migrator deve ter um arquivo `appsettings.json` mesmo que básico:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

## Estrutura de Arquivos a Criar

### No Projeto da Aplicação

1. **Dockerfiles**:
   - `src/InterfacesExternas/[PROJETO].Api/Dockerfile`
   - `src/InterfacesExternas/[PROJETO].Migrator/Dockerfile`

2. **GitHub Actions**:
   - `.github/workflows/push-to-ecr.yml`
   - `.github/workflows/deploy-to-eks.yml`

3. **Controllers**:
   - `src/InterfacesExternas/[PROJETO].Api/Controllers/HealthController.cs`

4. **Configuração**:
   - `src/InterfacesExternas/[PROJETO].Migrator/appsettings.json`

5. **Documentação**:
   - `docs/CI_CD_SETUP.md`
   - `docs/REVISAO_DEPLOY.md` (opcional)

### NO PROJETO DE INFRAESTRUTURA (já deve existir)

Os manifestos Kubernetes já devem estar criados em:
- `C:\Projetos\Fiap\fiap-fase4-infra\k8s\app\[NOME]\deployment.yaml`
- `C:\Projetos\Fiap\fiap-fase4-infra\k8s\app\[NOME]\migrator-job.yaml`
- `C:\Projetos\Fiap\fiap-fase4-infra\k8s\app\[NOME]\namespace.yaml`

**NÃO criar manifestos no projeto da aplicação!**

## Checklist de Implementação

### Subtask 01: Dockerfile da API
- [ ] Criar `src/InterfacesExternas/[PROJETO].Api/Dockerfile`
- [ ] Usar multi-stage build (build + runtime)
- [ ] Otimizar cache copiando .csproj primeiro
- [ ] Configurar `ASPNETCORE_URLS=http://+:80`
- [ ] Expor porta 80
- [ ] Testar build local: `docker build -t test -f src/InterfacesExternas/[PROJETO].Api/Dockerfile .`

### Subtask 02: Dockerfile do Migrator
- [ ] Criar `src/InterfacesExternas/[PROJETO].Migrator/Dockerfile`
- [ ] Usar multi-stage build
- [ ] Implementar tratamento de pasta Migrations vazia (criar .keep se necessário)
- [ ] Copiar appsettings.json
- [ ] Testar build local mesmo sem migrações

### Subtask 03: Health Check Endpoint
- [ ] Criar `HealthController.cs` com endpoint `/health`
- [ ] Retornar status JSON com timestamp
- [ ] Testar localmente: `curl http://localhost:5000/health`

### Subtask 04: appsettings.json do Migrator
- [ ] Criar `src/InterfacesExternas/[PROJETO].Migrator/appsettings.json`
- [ ] Configurar logging básico

### Subtask 05: Workflow Push to ECR
- [ ] Criar `.github/workflows/push-to-ecr.yml`
- [ ] Usar repositório único: `fiap-fase4-infra-[NOME]-api`
- [ ] Tags: `api-${TAG}` e `migrator-${TAG}`
- [ ] Validar imagens após push
- [ ] Configurar secrets: `AWS_ACCESS_KEY_ID`, `AWS_SECRET_ACCESS_KEY`, `AWS_SESSION_TOKEN`

### Subtask 06: Workflow Deploy to EKS
- [ ] Criar `.github/workflows/deploy-to-eks.yml`
- [ ] Validar imagens existem no ECR antes de deploy
- [ ] Atualizar deployment: `kubectl set image deployment/[NOME]-api api=${IMAGE}`
- [ ] Criar job do migrator com nome único (timestamp)
- [ ] Usar imagem: `${ECR_REGISTRY}/${ECR_REPOSITORY}:migrator-${TAG}`
- [ ] Implementar rollback em caso de falha

### Subtask 07: Documentação
- [ ] Criar `docs/CI_CD_SETUP.md`
- [ ] Documentar secrets do GitHub
- [ ] Documentar variáveis de ambiente
- [ ] Documentar como executar workflows
- [ ] Documentar troubleshooting

## Validações Finais

Antes de considerar completo, verificar:

1. **ECR**:
   - [ ] Repositório `fiap-fase4-infra-[NOME]-api` existe no AWS
   - [ ] Workflow consegue fazer push das imagens
   - [ ] Imagens aparecem no ECR com tags corretas (`api-*` e `migrator-*`)

2. **Kubernetes**:
   - [ ] Deployment `[NOME]-api` existe no namespace `[NOME]`
   - [ ] Workflow consegue atualizar a imagem do deployment
   - [ ] Job do migrator é criado e executa com sucesso
   - [ ] Health check `/health` funciona

3. **Workflows**:
   - [ ] Push to ECR executa sem erros
   - [ ] Deploy to EKS executa sem erros
   - [ ] Rollback funciona em caso de falha

## Referências

- Projeto PayStream (implementação de referência): `C:\Projetos\Fiap\fiap-fase4-paystream-api`
- Projeto de Infraestrutura: `C:\Projetos\Fiap\fiap-fase4-infra`
- Manifestos Kubernetes: `C:\Projetos\Fiap\fiap-fase4-infra\k8s\app\[NOME]\`

## Exemplo de Uso

Para criar a Story 02 para o OrderHub:

1. Substituir `[NOME]` por `orderhub`
2. Substituir `[PROJETO]` por `FastFood.OrderHub` (ou nome do projeto)
3. Verificar nomes dos repositórios ECR no Terraform
4. Verificar nomes dos deployments no projeto de infra
5. Seguir o checklist acima
6. Testar cada subtask antes de prosseguir

## Erros Comuns a Evitar

1. ❌ **Criar manifestos Kubernetes no projeto da aplicação** - Eles devem estar no projeto de infra
2. ❌ **Usar dois repositórios ECR separados** - Usar um único repositório com tags diferentes
3. ❌ **Esquecer tratamento de pasta Migrations vazia** - Sempre criar .keep se vazia
4. ❌ **Esquecer endpoint /health** - Obrigatório para health checks do Kubernetes
5. ❌ **Usar nomes de repositório incorretos** - Verificar no Terraform antes de configurar
6. ❌ **Esquecer appsettings.json do Migrator** - Necessário para o Dockerfile funcionar

## Notas Finais

- Sempre testar localmente antes de commitar
- Validar sintaxe YAML dos workflows
- Verificar que os nomes estão consistentes em todos os arquivos
- Documentar qualquer diferença específica do microserviço
- Manter padrão consistente entre todos os microserviços

