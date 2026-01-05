# Subtask 09: Configurar SonarCloud e validar integração

## Status
- **Estado:** 🔄 Pendente
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Configurar o projeto no SonarCloud, desabilitar análise automática, configurar Quality Gate, e validar que a integração está funcionando corretamente com o workflow do GitHub Actions.

## Passos de implementação

### 1. Criar projeto no SonarCloud
- [ ] Acessar https://sonarcloud.io
- [ ] Fazer login com conta GitHub
- [ ] Criar novo projeto
- [ ] Selecionar organização (ex: `diegoknsk`)
- [ ] Configurar nome do projeto (ex: `fiap-fase4-paystream-api`)
- [ ] Anotar Organization Key e Project Key

### 2. Desabilitar Análise Automática
- [ ] ⚠️ **CRÍTICO**: Navegar até Administration → Analysis Method
- [ ] Desabilitar **Automatic Analysis**
- [ ] Salvar alterações
- [ ] **Por quê?**: Análise automática e CI/CD são mutuamente exclusivas

### 3. Configurar Quality Gate
- [ ] Acessar Quality Gates no SonarCloud
- [ ] Criar ou editar Quality Gate customizado
- [ ] Configurar cobertura mínima de **85%**
- [ ] Configurar bloqueio para:
  - Security Hotspots críticos
  - Reliability issues bloqueantes
  - Maintainability issues bloqueantes
- [ ] Aplicar Quality Gate ao projeto

### 4. Gerar Token do SonarCloud
- [ ] Acessar My Account → Security
- [ ] Gerar novo token
- [ ] Copiar token gerado
- [ ] ⚠️ **IMPORTANTE**: Token não será exibido novamente, salvar com segurança

### 5. Configurar Secret no GitHub
- [ ] Acessar repositório no GitHub
- [ ] Ir em Settings → Secrets and variables → Actions
- [ ] Clicar em "New repository secret"
- [ ] Nome: `SONAR_TOKEN`
- [ ] Valor: Token gerado no SonarCloud
- [ ] Salvar secret

### 6. Atualizar workflow quality.yml
- [ ] Verificar que workflow tem Organization Key e Project Key corretos
- [ ] Verificar que workflow usa `SONAR_TOKEN` do secrets
- [ ] Verificar configurações de exclusão de cobertura
- [ ] Verificar que threshold está configurado para 85%

### 7. Validar integração
- [ ] Criar PR de teste
- [ ] Verificar que workflow executa
- [ ] Verificar que SonarCloud recebe dados
- [ ] Verificar que Quality Gate é avaliado
- [ ] Verificar que relatório aparece no SonarCloud
- [ ] Verificar que cobertura aparece no dashboard

## Configurações do SonarCloud

### Exclusões de Cobertura
Garantir que as seguintes exclusões estão configuradas:
- `**/*Program.cs`
- `**/*Startup.cs`
- `**/Migrations/**`
- `**/*Dto.cs`

### Quality Gate Customizado
Configurar Quality Gate com:
- Coverage: >= 85%
- Duplicated Lines: <= 3%
- Security Hotspots: 0 (ou justificados)
- Reliability Rating: A
- Maintainability Rating: A

## Como testar
- [ ] Criar PR com mudanças pequenas
- [ ] Verificar que workflow executa
- [ ] Verificar que análise aparece no SonarCloud
- [ ] Verificar que Quality Gate é avaliado
- [ ] Verificar que comentário aparece no PR (se configurado)
- [ ] Verificar que merge é bloqueado se Quality Gate falhar

## Critérios de aceite
- [ ] Projeto criado no SonarCloud
- [ ] Análise Automática desabilitada
- [ ] Quality Gate configurado com cobertura mínima de 85%
- [ ] Token gerado e configurado como secret no GitHub
- [ ] Workflow quality.yml configurado corretamente
- [ ] Integração funcionando (workflow envia dados para SonarCloud)
- [ ] Quality Gate bloqueia merges quando necessário
- [ ] Relatório de cobertura aparece no SonarCloud
- [ ] Dashboard do SonarCloud mostra métricas corretas

## Troubleshooting

### Erro: "Automatic Analysis is enabled"
- **Solução**: Desabilitar Análise Automática no SonarCloud (Administration → Analysis Method)

### Erro: "No coverage files found"
- **Solução**: Verificar que build tem símbolos de debug (`/p:DebugType=portable /p:DebugSymbols=true`)
- **Solução**: Verificar que formato é OpenCover
- **Solução**: Verificar consolidação de arquivos de cobertura

### Erro: "Quality Gate failed"
- **Solução**: Verificar cobertura atual do projeto
- **Solução**: Adicionar mais testes para aumentar cobertura
- **Solução**: Verificar outras métricas (duplicação, code smells)

## Referências
- [Documento de Lições Aprendidas - SonarCloud](./docs/PROMPT_MICROSERVICOS_TESTES_DEPLOY.md#23-configuração-do-sonarcloud)
- [SonarCloud Documentation](https://docs.sonarcloud.io/)
