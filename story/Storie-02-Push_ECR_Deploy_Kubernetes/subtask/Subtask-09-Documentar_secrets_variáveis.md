# Subtask 09: Configurar documentação de secrets e variáveis necessárias

## Descrição
Criar documentação completa sobre as secrets do GitHub e variáveis de ambiente necessárias para os workflows de CI/CD funcionarem corretamente.

## Passos de implementação
- Criar arquivo `docs/CI_CD_SETUP.md` (ou atualizar README.md se existir)
- Documentar secrets necessárias no GitHub:
  - `AWS_ACCESS_KEY_ID` - Chave de acesso AWS
  - `AWS_SECRET_ACCESS_KEY` - Chave secreta AWS
  - `AWS_SESSION_TOKEN` - Token de sessão AWS (para credenciais temporárias)
- Documentar como obter/configurar cada secret
- Documentar variáveis de ambiente usadas nos workflows:
  - `AWS_REGION`
  - `ECR_REPOSITORY_API`
  - `ECR_REPOSITORY_MIGRATOR`
  - `ECR_REGISTRY`
  - `EKS_CLUSTER_NAME`
  - `KUBERNETES_NAMESPACE`
- Documentar como executar os workflows manualmente
- Documentar como testar localmente os Dockerfiles
- Documentar como criar secrets no Kubernetes (se necessário)
- Incluir seção de troubleshooting com problemas comuns

## Como testar
- Verificar que a documentação está completa e clara
- Verificar que todas as secrets necessárias estão documentadas
- Verificar que todas as variáveis de ambiente estão documentadas
- Verificar que há instruções de como configurar cada secret
- Verificar que há instruções de como executar os workflows
- Verificar que há seção de troubleshooting

## Critérios de aceite
- Documentação criada em `docs/CI_CD_SETUP.md` (ou README.md atualizado)
- Todas as secrets necessárias estão documentadas:
  - `AWS_ACCESS_KEY_ID`
  - `AWS_SECRET_ACCESS_KEY`
  - `AWS_SESSION_TOKEN`
- Instruções de como obter/configurar cada secret estão incluídas
- Todas as variáveis de ambiente usadas nos workflows estão documentadas
- Instruções de como executar os workflows manualmente estão incluídas
- Instruções de como testar localmente os Dockerfiles estão incluídas
- Seção de troubleshooting com problemas comuns está incluída
- Documentação está clara e fácil de seguir


