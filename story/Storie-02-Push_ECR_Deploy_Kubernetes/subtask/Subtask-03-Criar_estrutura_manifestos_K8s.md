# Subtask 03: Criar estrutura de pastas para manifestos Kubernetes

## Descrição
Criar a estrutura de pastas organizada para armazenar os manifestos Kubernetes seguindo as melhores práticas e o padrão definido em `infrarules.mdc`.

## Passos de implementação
- Criar pasta `k8s/` na raiz do projeto
- Criar subpasta `k8s/app/` para manifestos de aplicação
- Criar subpasta `k8s/app/api/` para manifestos da API
- Criar subpasta `k8s/app/migrator/` para manifestos do Migrator
- Criar subpasta `k8s/infra/` para manifestos de infraestrutura compartilhada
- Criar arquivo `.gitkeep` em cada pasta para garantir que sejam versionadas (ou criar um README.md explicando a estrutura)

## Como testar
- Verificar que a estrutura de pastas foi criada:
  ```
  k8s/
  ├── app/
  │   ├── api/
  │   └── migrator/
  └── infra/
  ```
- Verificar que as pastas existem no sistema de arquivos
- Verificar que as pastas podem ser versionadas no Git

## Critérios de aceite
- Estrutura de pastas `k8s/` criada na raiz do projeto
- Subpastas organizadas conforme padrão:
  - `k8s/app/api/` - Manifestos da API
  - `k8s/app/migrator/` - Manifestos do Migrator
  - `k8s/infra/` - Manifestos de infraestrutura
- Estrutura segue padrão definido em `infrarules.mdc`



