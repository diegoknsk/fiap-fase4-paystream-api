# Subtask 07: Criar GitHub Action para push no ECR

## Descrição
Criar o workflow do GitHub Actions para fazer build e push das imagens Docker (API e Migrator) para o Amazon ECR.

## Passos de implementação
- Criar arquivo `.github/workflows/push-to-ecr.yml`
- Configurar trigger `workflow_dispatch` para execução manual
- Configurar variáveis de ambiente:
  - `AWS_REGION` (ex: `us-east-1`)
  - `ECR_REPOSITORY_API` (ex: `paystream-api`)
  - `ECR_REPOSITORY_MIGRATOR` (ex: `paystream-migrator`)
  - `ECR_REGISTRY` (ex: `898384491704.dkr.ecr.us-east-1.amazonaws.com`)
- Configurar job `build-and-push`:
  - Usar `runs-on: ubuntu-latest`
  - Fazer checkout do código
  - Configurar credenciais AWS:
    - Usar `aws-actions/configure-aws-credentials@v4`
    - Configurar `AWS_ACCESS_KEY_ID`, `AWS_SECRET_ACCESS_KEY`, `AWS_SESSION_TOKEN` via secrets
  - Fazer login no ECR:
    - Usar `aws-actions/amazon-ecr-login@v2`
  - Build e push da imagem da API:
    - Gerar tag baseada em `${{ github.sha }}` (ex: `latest` e `${{ github.sha }}`)
    - Build usando Dockerfile da API
    - Push para ECR com tags
  - Build e push da imagem do Migrator:
    - Gerar tag baseada em `${{ github.sha }}`
    - Build usando Dockerfile do Migrator
    - Push para ECR com tags
  - Validar que as imagens foram criadas com sucesso

## Como testar
- Verificar sintaxe YAML do workflow
- Configurar secrets no GitHub:
  - `AWS_ACCESS_KEY_ID`
  - `AWS_SECRET_ACCESS_KEY`
  - `AWS_SESSION_TOKEN`
- Executar workflow manualmente via GitHub Actions UI
- Verificar que as imagens são criadas no ECR
- Verificar que as tags estão corretas
- Verificar logs do workflow para garantir que não há erros

## Critérios de aceite
- Arquivo `.github/workflows/push-to-ecr.yml` criado
- Workflow configurado com trigger `workflow_dispatch`
- Workflow faz build das imagens da API e Migrator
- Workflow faz push para ECR com tags baseadas em SHA do commit
- Workflow usa autenticação AWS via secrets configuradas
- Workflow valida que as imagens foram criadas com sucesso
- Workflow pode ser executado manualmente via GitHub Actions UI
- Imagens são criadas e publicadas no ECR com sucesso
- Documentação sobre secrets necessárias está clara

