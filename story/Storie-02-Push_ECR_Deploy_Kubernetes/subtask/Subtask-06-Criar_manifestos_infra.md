# Subtask 06: Criar manifestos de infraestrutura (Namespace, ConfigMap)

## Descrição
Criar os manifestos de infraestrutura compartilhada (Namespace e ConfigMap) necessários para o funcionamento da API e do Migrator no Kubernetes.

## Passos de implementação
- Criar arquivo `k8s/infra/namespace.yaml`:
  - Definir `kind: Namespace`
  - Configurar `metadata.name: paystream`
  - Adicionar labels apropriados (ex: `app: paystream`, `environment: production`)
- Criar arquivo `k8s/infra/configmap.yaml`:
  - Definir `kind: ConfigMap`
  - Configurar `metadata.namespace: paystream`
  - Configurar `metadata.name: paystream-config`
  - Adicionar variáveis de ambiente não sensíveis:
    - `ASPNETCORE_ENVIRONMENT` (ex: `Production`)
    - `ASPNETCORE_URLS` (ex: `http://+:80`)
    - `MERCADO_PAGO_MODE` (ex: `MOCK` ou `INTEGRATED`)
    - Outras variáveis de configuração não sensíveis
- Criar arquivo `k8s/infra/secrets-template.yaml` (template/documentação):
  - Documentar quais secrets são necessários
  - Listar variáveis sensíveis que devem ser criadas manualmente:
    - `ConnectionStrings__PostgreSQL` (connection string do RDS)
    - `AWS_ACCESS_KEY_ID`
    - `AWS_SECRET_ACCESS_KEY`
    - `AWS_SESSION_TOKEN`
    - `MERCADO_PAGO_ACCESS_TOKEN` (se modo integrado)
    - `MERCADO_PAGO_PUBLIC_KEY` (se modo integrado)
  - Incluir instruções de como criar os secrets via `kubectl create secret`

## Como testar
- Validar sintaxe YAML com `kubectl apply --dry-run=client -f k8s/infra/namespace.yaml`
- Validar sintaxe YAML com `kubectl apply --dry-run=client -f k8s/infra/configmap.yaml`
- Verificar que o namespace `paystream` está definido corretamente
- Verificar que o ConfigMap contém variáveis não sensíveis apropriadas
- Verificar que a documentação de secrets está completa

## Critérios de aceite
- Arquivo `k8s/infra/namespace.yaml` criado
- Arquivo `k8s/infra/configmap.yaml` criado
- Arquivo `k8s/infra/secrets-template.yaml` criado (documentação)
- Namespace `paystream` definido corretamente
- ConfigMap contém variáveis de ambiente não sensíveis
- Documentação de secrets lista todas as variáveis sensíveis necessárias
- Manifestos validam sem erros com `kubectl apply --dry-run`
- Seguem padrões definidos em `infrarules.mdc`

