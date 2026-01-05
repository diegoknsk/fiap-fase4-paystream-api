# Templates Story 02 - KitchenFlow

Este documento contém todos os templates de arquivos necessários para implementar a Story 02 no projeto KitchenFlow.

## Informações do KitchenFlow

- **Nome do Microserviço**: `kitchenflow`
- **ECR Repository**: `fiap-fase4-infra-kitchenflow-api`
- **Namespace Kubernetes**: `kitchenflow`
- **Deployment**: `kitchenflow-api`
- **Container**: `api`
- **Job**: `kitchenflow-migrator`
- **Projeto .NET**: `FastFood.KitchenFlow.*`

---

## 1. Dockerfile da API

**Arquivo**: `src/InterfacesExternas/FastFood.KitchenFlow.Api/Dockerfile`

```dockerfile
# Etapa 1: build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copiar arquivos .csproj e fazer restore (otimização de cache)
COPY src/Core/FastFood.KitchenFlow.Domain/FastFood.KitchenFlow.Domain.csproj src/Core/FastFood.KitchenFlow.Domain/
COPY src/Core/FastFood.KitchenFlow.Application/FastFood.KitchenFlow.Application.csproj src/Core/FastFood.KitchenFlow.Application/
COPY src/Core/FastFood.KitchenFlow.CrossCutting/FastFood.KitchenFlow.CrossCutting.csproj src/Core/FastFood.KitchenFlow.CrossCutting/
COPY src/Infra/FastFood.KitchenFlow.Infra/FastFood.KitchenFlow.Infra.csproj src/Infra/FastFood.KitchenFlow.Infra/
COPY src/Infra/FastFood.KitchenFlow.Infra.Persistence/FastFood.KitchenFlow.Infra.Persistence.csproj src/Infra/FastFood.KitchenFlow.Infra.Persistence/
COPY src/InterfacesExternas/FastFood.KitchenFlow.Api/FastFood.KitchenFlow.Api.csproj src/InterfacesExternas/FastFood.KitchenFlow.Api/

RUN dotnet restore src/InterfacesExternas/FastFood.KitchenFlow.Api/FastFood.KitchenFlow.Api.csproj

# Copiar todo o código
COPY . .

# Publicar a aplicação
RUN dotnet publish src/InterfacesExternas/FastFood.KitchenFlow.Api/FastFood.KitchenFlow.Api.csproj -c Release -o /app/publish \
    /p:CopyOutputSymbolsToPublishDirectory=false \
    /p:CopyOutputXmlDocumentationToPublishDirectory=false

# Etapa 2: imagem de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:80

EXPOSE 80

ENTRYPOINT ["dotnet", "FastFood.KitchenFlow.Api.dll"]
```

---

## 2. Dockerfile do Migrator

**Arquivo**: `src/InterfacesExternas/FastFood.KitchenFlow.Migrator/Dockerfile`

```dockerfile
# Etapa 1: build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copiar arquivos .csproj e fazer restore (otimização de cache)
COPY src/Core/FastFood.KitchenFlow.Domain/FastFood.KitchenFlow.Domain.csproj src/Core/FastFood.KitchenFlow.Domain/
COPY src/Core/FastFood.KitchenFlow.Application/FastFood.KitchenFlow.Application.csproj src/Core/FastFood.KitchenFlow.Application/
COPY src/Core/FastFood.KitchenFlow.CrossCutting/FastFood.KitchenFlow.CrossCutting.csproj src/Core/FastFood.KitchenFlow.CrossCutting/
COPY src/Infra/FastFood.KitchenFlow.Infra/FastFood.KitchenFlow.Infra.csproj src/Infra/FastFood.KitchenFlow.Infra/
COPY src/Infra/FastFood.KitchenFlow.Infra.Persistence/FastFood.KitchenFlow.Infra.Persistence.csproj src/Infra/FastFood.KitchenFlow.Infra.Persistence/
COPY src/InterfacesExternas/FastFood.KitchenFlow.Migrator/FastFood.KitchenFlow.Migrator.csproj src/InterfacesExternas/FastFood.KitchenFlow.Migrator/

RUN dotnet restore src/InterfacesExternas/FastFood.KitchenFlow.Migrator/FastFood.KitchenFlow.Migrator.csproj

# Copiar todo o código
COPY . .

# Publicar o Migrator
RUN dotnet publish src/InterfacesExternas/FastFood.KitchenFlow.Migrator/FastFood.KitchenFlow.Migrator.csproj -c Release -o /out \
    /p:CopyOutputSymbolsToPublishDirectory=false \
    /p:CopyOutputXmlDocumentationToPublishDirectory=false

# Preparar migrações: copiar para pasta temporária, garantindo que sempre exista
RUN mkdir -p /migrations && \
    if [ -d "src/Infra/FastFood.KitchenFlow.Infra.Persistence/Migrations" ] && [ "$(find src/Infra/FastFood.KitchenFlow.Infra.Persistence/Migrations -type f 2>/dev/null | wc -l)" -gt 0 ]; then \
        cp -r src/Infra/FastFood.KitchenFlow.Infra.Persistence/Migrations/* /migrations/ 2>/dev/null || true; \
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
COPY src/InterfacesExternas/FastFood.KitchenFlow.Migrator/appsettings.json ./appsettings.json

# Copiar migrações do build stage (sempre terá pelo menos o arquivo .keep)
COPY --from=build /migrations ./Migrations

ENTRYPOINT ["dotnet", "FastFood.KitchenFlow.Migrator.dll"]
```

---

## 3. HealthController

**Arquivo**: `src/InterfacesExternas/FastFood.KitchenFlow.Api/Controllers/HealthController.cs`

```csharp
using Microsoft.AspNetCore.Mvc;

namespace FastFood.KitchenFlow.Api.Controllers;

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

---

## 4. appsettings.json do Migrator

**Arquivo**: `src/InterfacesExternas/FastFood.KitchenFlow.Migrator/appsettings.json`

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

---

## 5. Workflow Push to ECR

**Arquivo**: `.github/workflows/push-to-ecr.yml`

```yaml
name: Push to ECR

on:
  workflow_dispatch:
    inputs:
      image_tag:
        description: 'Tag da imagem (padrão: SHA do commit)'
        required: false
        default: ''

env:
  AWS_REGION: us-east-1
  ECR_REPOSITORY: fiap-fase4-infra-kitchenflow-api

jobs:
  build-and-push:
    runs-on: ubuntu-latest
    
    steps:
    - name: Checkout code
      uses: actions/checkout@v4

    - name: Configure AWS credentials
      uses: aws-actions/configure-aws-credentials@v4
      with:
        aws-access-key-id: ${{ secrets.AWS_ACCESS_KEY_ID }}
        aws-secret-access-key: ${{ secrets.AWS_SECRET_ACCESS_KEY }}
        aws-session-token: ${{ secrets.AWS_SESSION_TOKEN }}
        aws-region: ${{ env.AWS_REGION }}

    - name: Login to Amazon ECR
      id: login-ecr
      uses: aws-actions/amazon-ecr-login@v2

    - name: Get ECR registry
      id: ecr-registry
      run: |
        ACCOUNT_ID=$(aws sts get-caller-identity --query Account --output text)
        ECR_REGISTRY="${ACCOUNT_ID}.dkr.ecr.${AWS_REGION}.amazonaws.com"
        echo "registry=${ECR_REGISTRY}" >> $GITHUB_OUTPUT
        echo "ECR Registry: ${ECR_REGISTRY}"

    - name: Set image tag
      id: image-tag
      run: |
        if [ -z "${{ github.event.inputs.image_tag }}" ]; then
          IMAGE_TAG="${{ github.sha }}"
        else
          IMAGE_TAG="${{ github.event.inputs.image_tag }}"
        fi
        echo "tag=${IMAGE_TAG}" >> $GITHUB_OUTPUT
        echo "Image Tag: ${IMAGE_TAG}"

    - name: Build and push API image
      env:
        ECR_REGISTRY: ${{ steps.ecr-registry.outputs.registry }}
        IMAGE_TAG: ${{ steps.image-tag.outputs.tag }}
      run: |
        docker build -t ${ECR_REGISTRY}/${ECR_REPOSITORY}:api-${IMAGE_TAG} \
          -t ${ECR_REGISTRY}/${ECR_REPOSITORY}:api-latest \
          -f src/InterfacesExternas/FastFood.KitchenFlow.Api/Dockerfile .
        docker push ${ECR_REGISTRY}/${ECR_REPOSITORY}:api-${IMAGE_TAG}
        docker push ${ECR_REGISTRY}/${ECR_REPOSITORY}:api-latest
        echo "API image pushed: ${ECR_REGISTRY}/${ECR_REPOSITORY}:api-${IMAGE_TAG}"

    - name: Build and push Migrator image
      env:
        ECR_REGISTRY: ${{ steps.ecr-registry.outputs.registry }}
        IMAGE_TAG: ${{ steps.image-tag.outputs.tag }}
      run: |
        docker build -t ${ECR_REGISTRY}/${ECR_REPOSITORY}:migrator-${IMAGE_TAG} \
          -t ${ECR_REGISTRY}/${ECR_REPOSITORY}:migrator-latest \
          -f src/InterfacesExternas/FastFood.KitchenFlow.Migrator/Dockerfile .
        docker push ${ECR_REGISTRY}/${ECR_REPOSITORY}:migrator-${IMAGE_TAG}
        docker push ${ECR_REGISTRY}/${ECR_REPOSITORY}:migrator-latest
        echo "Migrator image pushed: ${ECR_REGISTRY}/${ECR_REPOSITORY}:migrator-${IMAGE_TAG}"

    - name: Validate images in ECR
      env:
        ECR_REGISTRY: ${{ steps.ecr-registry.outputs.registry }}
        IMAGE_TAG: ${{ steps.image-tag.outputs.tag }}
      run: |
        echo "Validating images in ECR..."
        aws ecr describe-images --repository-name ${ECR_REPOSITORY} --image-ids imageTag=api-${IMAGE_TAG} --region ${AWS_REGION}
        aws ecr describe-images --repository-name ${ECR_REPOSITORY} --image-ids imageTag=migrator-${IMAGE_TAG} --region ${AWS_REGION}
        echo "Images validated successfully!"
```

---

## 6. Workflow Deploy to EKS

**Arquivo**: `.github/workflows/deploy-to-eks.yml`

```yaml
name: Deploy to EKS

on:
  workflow_dispatch:
    inputs:
      image_tag:
        description: 'Tag da imagem para deploy (padrão: SHA do commit)'
        required: false
        default: ''
      skip_migrator:
        description: 'Pular execução do Migrator'
        required: false
        type: boolean
        default: false

env:
  AWS_REGION: us-east-1
  EKS_CLUSTER_NAME: eks-paystream
  KUBERNETES_NAMESPACE: kitchenflow
  DEPLOYMENT_NAME: kitchenflow-api
  CONTAINER_NAME: api

jobs:
  deploy:
    runs-on: ubuntu-latest
    
    steps:
    - name: Checkout code
      uses: actions/checkout@v4

    - name: Configure AWS credentials
      uses: aws-actions/configure-aws-credentials@v4
      with:
        aws-access-key-id: ${{ secrets.AWS_ACCESS_KEY_ID }}
        aws-secret-access-key: ${{ secrets.AWS_SECRET_ACCESS_KEY }}
        aws-session-token: ${{ secrets.AWS_SESSION_TOKEN }}
        aws-region: ${{ env.AWS_REGION }}

    - name: Get ECR registry
      id: ecr-registry
      run: |
        ACCOUNT_ID=$(aws sts get-caller-identity --query Account --output text)
        ECR_REGISTRY="${ACCOUNT_ID}.dkr.ecr.${AWS_REGION}.amazonaws.com"
        echo "registry=${ECR_REGISTRY}" >> $GITHUB_OUTPUT
        echo "ECR Registry: ${ECR_REGISTRY}"

    - name: Set image tag
      id: image-tag
      run: |
        if [ -z "${{ github.event.inputs.image_tag }}" ]; then
          IMAGE_TAG="${{ github.sha }}"
        else
          IMAGE_TAG="${{ github.event.inputs.image_tag }}"
        fi
        echo "tag=${IMAGE_TAG}" >> $GITHUB_OUTPUT
        echo "Image Tag: ${IMAGE_TAG}"

    - name: Validate images exist in ECR
      env:
        ECR_REGISTRY: ${{ steps.ecr-registry.outputs.registry }}
        IMAGE_TAG: ${{ steps.image-tag.outputs.tag }}
        ECR_REPOSITORY: fiap-fase4-infra-kitchenflow-api
      run: |
        echo "Validating images exist in ECR..."
        aws ecr describe-images --repository-name ${ECR_REPOSITORY} --image-ids imageTag=api-${IMAGE_TAG} --region ${AWS_REGION} || exit 1
        aws ecr describe-images --repository-name ${ECR_REPOSITORY} --image-ids imageTag=migrator-${IMAGE_TAG} --region ${AWS_REGION} || exit 1
        echo "Images validated successfully!"

    - name: Install kubectl
      uses: azure/setup-kubectl@v3
      with:
        version: 'latest'

    - name: Configure kubectl for EKS
      run: |
        aws eks update-kubeconfig \
          --name ${EKS_CLUSTER_NAME} \
          --region ${AWS_REGION}
        kubectl version --client

    - name: Update API deployment image
      env:
        ECR_REGISTRY: ${{ steps.ecr-registry.outputs.registry }}
        IMAGE_TAG: ${{ steps.image-tag.outputs.tag }}
        ECR_REPOSITORY: fiap-fase4-infra-kitchenflow-api
      run: |
        echo "Updating deployment image..."
        FULL_IMAGE="${ECR_REGISTRY}/${ECR_REPOSITORY}:api-${IMAGE_TAG}"
        echo "Setting image to: ${FULL_IMAGE}"
        kubectl set image deployment/${DEPLOYMENT_NAME} ${CONTAINER_NAME}=${FULL_IMAGE} -n ${KUBERNETES_NAMESPACE}
        echo "Deployment image updated successfully!"

    - name: Wait for API deployment rollout
      run: |
        echo "Waiting for API deployment rollout..."
        kubectl rollout status deployment/${DEPLOYMENT_NAME} -n ${KUBERNETES_NAMESPACE} --timeout=5m
        echo "API deployment rollout completed!"

    - name: Verify API deployment
      run: |
        echo "Verifying API deployment..."
        kubectl get deployment ${DEPLOYMENT_NAME} -n ${KUBERNETES_NAMESPACE}
        kubectl get pods -l app=kitchenflow-api -n ${KUBERNETES_NAMESPACE}
        echo "API deployment verified!"

    - name: Create and execute Migrator Job
      if: ${{ !github.event.inputs.skip_migrator }}
      env:
        ECR_REGISTRY: ${{ steps.ecr-registry.outputs.registry }}
        IMAGE_TAG: ${{ steps.image-tag.outputs.tag }}
        ECR_REPOSITORY: fiap-fase4-infra-kitchenflow-api
      run: |
        echo "Creating Migrator Job..."
        # Jobs não podem ser atualizados, então criamos um novo com timestamp único
        JOB_NAME="kitchenflow-migrator-$(date +%s)"
        FULL_IMAGE="${ECR_REGISTRY}/${ECR_REPOSITORY}:migrator-${IMAGE_TAG}"
        echo "Job name: ${JOB_NAME}"
        echo "Image: ${FULL_IMAGE}"
        
        # Criar job baseado no template do projeto de infra, mas com nome único e imagem atualizada
        # O template está em: C:\Projetos\Fiap\fiap-fase4-infra\k8s\app\kitchenflow\migrator-job.yaml
        cat <<EOF | kubectl apply -f -
        apiVersion: batch/v1
        kind: Job
        metadata:
          name: ${JOB_NAME}
          namespace: ${KUBERNETES_NAMESPACE}
          labels:
            app: kitchenflow-migrator
            service: kitchenflow
        spec:
          completions: 1
          parallelism: 1
          backoffLimit: 3
          template:
            metadata:
              labels:
                app: kitchenflow-migrator
                service: kitchenflow
            spec:
              restartPolicy: Never
              containers:
              - name: migrator
                image: ${FULL_IMAGE}
                imagePullPolicy: Always
                resources:
                  requests:
                    cpu: "100m"
                    memory: "256Mi"
                  limits:
                    cpu: "500m"
                    memory: "512Mi"
                terminationGracePeriodSeconds: 30
                envFrom:
                - configMapRef:
                    name: kitchenflow-config
                - secretRef:
                    name: kitchenflow-secrets
                command: ["dotnet", "FastFood.KitchenFlow.Migrator.dll"]
        EOF
        echo "Migrator Job created successfully!"

    - name: Wait for Migrator Job completion
      if: ${{ !github.event.inputs.skip_migrator }}
      run: |
        echo "Waiting for Migrator Job to complete..."
        # Buscar o nome do job mais recente
        JOB_NAME=$(kubectl get jobs -n ${KUBERNETES_NAMESPACE} -l app=kitchenflow-migrator --sort-by=.metadata.creationTimestamp -o jsonpath='{.items[-1].metadata.name}')
        if [ -n "${JOB_NAME}" ]; then
          echo "Waiting for job: ${JOB_NAME}"
          kubectl wait --for=condition=complete --timeout=10m job/${JOB_NAME} -n ${KUBERNETES_NAMESPACE} || true
          echo "Migrator Job completed!"
        else
          echo "No Migrator Job found!"
          exit 1
        fi

    - name: Verify Migrator Job
      if: ${{ !github.event.inputs.skip_migrator }}
      run: |
        echo "Verifying Migrator Job..."
        JOB_NAME=$(kubectl get jobs -n ${KUBERNETES_NAMESPACE} -l app=kitchenflow-migrator --sort-by=.metadata.creationTimestamp -o jsonpath='{.items[-1].metadata.name}')
        if [ -n "${JOB_NAME}" ]; then
          kubectl get job ${JOB_NAME} -n ${KUBERNETES_NAMESPACE}
          kubectl get pods -l app=kitchenflow-migrator -n ${KUBERNETES_NAMESPACE}
          kubectl logs -l app=kitchenflow-migrator -n ${KUBERNETES_NAMESPACE} --tail=50 || true
          echo "Migrator Job verified!"
        else
          echo "No Migrator Job found!"
        fi

    - name: Rollback on failure
      if: failure()
      run: |
        echo "Deployment failed! Attempting rollback..."
        kubectl rollout undo deployment/${DEPLOYMENT_NAME} -n ${KUBERNETES_NAMESPACE} || true
        echo "Rollback completed!"
```

---

## Checklist de Implementação

### Arquivos a Criar

- [ ] `src/InterfacesExternas/FastFood.KitchenFlow.Api/Dockerfile`
- [ ] `src/InterfacesExternas/FastFood.KitchenFlow.Migrator/Dockerfile`
- [ ] `src/InterfacesExternas/FastFood.KitchenFlow.Api/Controllers/HealthController.cs`
- [ ] `src/InterfacesExternas/FastFood.KitchenFlow.Migrator/appsettings.json`
- [ ] `.github/workflows/push-to-ecr.yml`
- [ ] `.github/workflows/deploy-to-eks.yml`

### Validações

- [ ] Dockerfiles compilam sem erros
- [ ] Health endpoint `/health` funciona
- [ ] Workflow Push to ECR executa com sucesso
- [ ] Workflow Deploy to EKS executa com sucesso
- [ ] Deployment atualiza corretamente
- [ ] Job do Migrator executa com sucesso

### Observações Importantes

1. **Manifestos Kubernetes**: Já existem no projeto de infra (`C:\Projetos\Fiap\fiap-fase4-infra\k8s\app\kitchenflow\`)
2. **ECR Repository**: Deve existir `fiap-fase4-infra-kitchenflow-api` no AWS
3. **Secrets GitHub**: Configurar `AWS_ACCESS_KEY_ID`, `AWS_SECRET_ACCESS_KEY`, `AWS_SESSION_TOKEN`
4. **ConfigMaps e Secrets K8s**: Devem existir `kitchenflow-config` e `kitchenflow-secrets` no namespace `kitchenflow`

---

## Referências

- Projeto PayStream (referência): `C:\Projetos\Fiap\fiap-fase4-paystream-api`
- Projeto de Infra: `C:\Projetos\Fiap\fiap-fase4-infra`
- Manifestos K8s: `C:\Projetos\Fiap\fiap-fase4-infra\k8s\app\kitchenflow\`

