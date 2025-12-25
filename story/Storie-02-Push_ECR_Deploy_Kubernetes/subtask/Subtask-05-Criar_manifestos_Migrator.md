# Subtask 05: Criar manifestos Kubernetes para Migrator (Job)

## Descrição
Criar o manifesto Kubernetes Job para o Migrator, garantindo que seja idempotente e possa ser executado múltiplas vezes sem problemas.

## Passos de implementação
- Criar arquivo `k8s/app/migrator/migrator-job.yaml`:
  - Definir `kind: Job`
  - Configurar `metadata.namespace: paystream`
  - Configurar `spec.completions: 1` e `spec.parallelism: 1`
  - Configurar `spec.backoffLimit: 3` para permitir retries em caso de falha
  - Configurar `spec.ttlSecondsAfterFinished: 3600` para limpeza automática após 1 hora
  - Definir container do Migrator:
    - Imagem do ECR (usar placeholder para tag)
    - Resources (requests e limits) para CPU e memória
    - Variáveis de ambiente via `envFrom` (ConfigMap e Secrets)
    - Command apropriado para executar migrações (ex: `dotnet FastFood.PayStream.Migrator.dll`)
  - Garantir que o Job seja idempotente (migrações do EF Core são idempotentes por padrão)

## Como testar
- Validar sintaxe YAML com `kubectl apply --dry-run=client -f k8s/app/migrator/migrator-job.yaml`
- Verificar que o Job está configurado para ser executado uma vez
- Verificar que há limite de retries configurado
- Verificar que variáveis de ambiente estão configuradas via ConfigMap/Secrets
- Verificar que o Job pode ser executado múltiplas vezes sem problemas (idempotência)

## Critérios de aceite
- Arquivo `k8s/app/migrator/migrator-job.yaml` criado
- Job configurado com:
  - Namespace `paystream`
  - Completions e parallelism definidos
  - Backoff limit para retries
  - TTL para limpeza automática
  - Resources definidos
  - Variáveis de ambiente via ConfigMap/Secrets
- Job é idempotente (pode ser executado múltiplas vezes)
- Manifesto valida sem erros com `kubectl apply --dry-run`
- Segue padrões definidos em `infrarules.mdc`

