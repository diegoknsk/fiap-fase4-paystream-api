# Subtask 10: Verificar e ajustar cobertura para atingir 85%

## Status
- **Estado:** 🔄 Pendente
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Executar análise de cobertura completa, identificar áreas com baixa cobertura, e criar testes adicionais para garantir que a meta de 85% de cobertura seja atingida e mantida.

## Passos de implementação

### 1. Executar análise de cobertura local
- [ ] Executar `dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover`
- [ ] Gerar relatório de cobertura
- [ ] Analisar relatório para identificar áreas com baixa cobertura
- [ ] Listar classes/métodos com cobertura < 85%

### 2. Analisar relatório do SonarCloud
- [ ] Acessar dashboard do SonarCloud
- [ ] Verificar cobertura geral do projeto
- [ ] Identificar arquivos com baixa cobertura
- [ ] Verificar cobertura por camada (Domain, Application, API)
- [ ] Identificar métodos não cobertos

### 3. Priorizar áreas para cobertura
- [ ] **Alta prioridade**: UseCases, Domain, Controllers principais
- [ ] **Média prioridade**: Presenters, Helpers, Utils
- [ ] **Baixa prioridade**: DTOs, Configurations (se excluídos)

### 4. Criar testes adicionais
- [ ] Para cada área com baixa cobertura, criar testes específicos
- [ ] Focar em caminhos não cobertos (edge cases, validações, erros)
- [ ] Garantir que testes seguem padrão AAA
- [ ] Usar FluentAssertions

### 5. Validar cobertura incremental
- [ ] Executar testes após cada adição
- [ ] Verificar aumento de cobertura
- [ ] Continuar até atingir 85% mínimo
- [ ] Preferir atingir >85% para ter margem de segurança

### 6. Verificar exclusões
- [ ] Verificar que exclusões estão corretas (Program.cs, Migrations, DTOs)
- [ ] Ajustar exclusões se necessário
- [ ] Garantir que código importante não está sendo excluído incorretamente

### 7. Validar no CI/CD
- [ ] Criar PR com todas as mudanças
- [ ] Verificar que workflow executa
- [ ] Verificar que SonarCloud mostra cobertura >= 85%
- [ ] Verificar que Quality Gate passa
- [ ] Verificar que threshold de 85% é validado no workflow

## Ferramentas para análise

### Local (dotnet)
```bash
# Gerar relatório de cobertura
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

# Verificar arquivo gerado
# Arquivo será gerado em: TestResults/coverage/coverage.opencover.xml
```

### SonarCloud
- Dashboard mostra cobertura geral
- Pode filtrar por arquivo/classe
- Mostra linhas não cobertas
- Gera relatórios detalhados

## Estratégias para aumentar cobertura

### 1. Testar edge cases
- Valores limite (min, max, zero, negativo)
- Valores null/empty
- Estados inválidos

### 2. Testar tratamento de erros
- Exceções esperadas
- Validações que falham
- Falhas de dependências (repositórios, gateways)

### 3. Testar todos os caminhos
- If/else statements
- Switch cases
- Loops
- Early returns

### 4. Testar métodos privados indiretamente
- Testar através de métodos públicos
- Não criar testes diretos para métodos privados

## Checklist de verificação

- [ ] Cobertura geral >= 85%
- [ ] Cobertura de Domain >= 95%
- [ ] Cobertura de UseCases >= 90%
- [ ] Cobertura de Controllers >= 80%
- [ ] Cobertura de Presenters >= 90%
- [ ] Todos os testes passam
- [ ] Quality Gate passa no SonarCloud
- [ ] Workflow valida threshold de 85%
- [ ] Relatório de cobertura está completo

## Critérios de aceite
- [ ] Cobertura geral do projeto >= 85% (verificado no SonarCloud)
- [ ] Cobertura de Domain >= 95%
- [ ] Cobertura de UseCases >= 90%
- [ ] Cobertura de Controllers >= 80%
- [ ] Todos os testes passam
- [ ] Quality Gate passa no SonarCloud
- [ ] Workflow bloqueia merges quando cobertura < 85%
- [ ] Relatório de cobertura aparece no SonarCloud
- [ ] Exclusões de cobertura estão corretas
- [ ] Documentação de cobertura atualizada (se necessário)

## Manutenção contínua
- [ ] Configurar alertas no SonarCloud para queda de cobertura
- [ ] Revisar cobertura em cada PR
- [ ] Adicionar testes quando novo código é adicionado
- [ ] Manter cobertura acima de 85% como padrão

## Referências
- [Documento de Lições Aprendidas - Cobertura](./docs/PROMPT_MICROSERVICOS_TESTES_DEPLOY.md#1-cobertura-de-testes)
- [SonarCloud Coverage Metrics](https://docs.sonarcloud.io/enriching/test-coverage/)
