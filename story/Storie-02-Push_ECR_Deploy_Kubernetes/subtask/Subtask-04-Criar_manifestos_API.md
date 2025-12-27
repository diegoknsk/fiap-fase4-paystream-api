# Subtask 04: Criar manifestos Kubernetes para API (Deployment e Service)

## Descrição
Criar os manifestos Kubernetes (Deployment e Service) para a API, configurando recursos, health checks, variáveis de ambiente e estratégia de rollout seguro.

## Passos de implementação
- Criar arquivo `k8s/app/api/api-deployment.yaml`:
  - Definir `kind: Deployment`
  - Configurar `metadata.namespace: paystream`
  - Configurar labels e selectors apropriados
  - Definir `strategy.type: RollingUpdate` com `maxUnavailable: 0` e `maxSurge: 1`
  - Configurar container da API:
    - Imagem do ECR (usar placeholder para tag)
    - Porta 80 nomeada como `http`
    - Resources (requests e limits) para CPU e memória
    - Liveness probe (HTTP GET em `/health` ou similar)
    - Readiness probe (HTTP GET em `/health` ou similar)
    - Variáveis de ambiente via `envFrom` (ConfigMap e Secrets)
  - Configurar `replicas: 2` (ou valor apropriado)
- Criar arquivo `k8s/app/api/api-service.yaml`:
  - Definir `kind: Service`
  - Configurar `metadata.namespace: paystream`
  - Configurar `type: LoadBalancer` ou `ClusterIP` conforme necessário
  - Configurar selector para casar com labels do Deployment
  - Configurar porta 80 mapeada para porta `http` do container
  - Se LoadBalancer, adicionar annotations para NLB:
    - `service.beta.kubernetes.io/aws-load-balancer-scheme: internet-facing`
    - `service.beta.kubernetes.io/aws-load-balancer-type: nlb`

## Como testar
- Validar sintaxe YAML com `kubectl apply --dry-run=client -f k8s/app/api/api-deployment.yaml`
- Validar sintaxe YAML com `kubectl apply --dry-run=client -f k8s/app/api/api-service.yaml`
- Verificar que os manifestos seguem o padrão definido em `infrarules.mdc`
- Verificar que namespace `paystream` está referenciado
- Verificar que health checks estão configurados

## Critérios de aceite
- Arquivo `k8s/app/api/api-deployment.yaml` criado
- Arquivo `k8s/app/api/api-service.yaml` criado
- Deployment configurado com:
  - Namespace `paystream`
  - Estratégia RollingUpdate segura
  - Health checks (liveness e readiness)
  - Resources definidos
  - Variáveis de ambiente via ConfigMap/Secrets
- Service configurado com:
  - Selector correto para o Deployment
  - Porta 80 exposta
  - Tipo apropriado (LoadBalancer ou ClusterIP)
- Manifestos validam sem erros com `kubectl apply --dry-run`
- Seguem padrões definidos em `infrarules.mdc`


