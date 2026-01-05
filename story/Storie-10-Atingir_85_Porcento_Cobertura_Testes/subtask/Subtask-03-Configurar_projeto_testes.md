# Subtask 03: Configurar projeto de testes com Coverlet e FluentAssertions

## Status
- **Estado:** 🔄 Pendente
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Configurar o projeto de testes unitários para usar FluentAssertions e garantir que a estrutura de pastas espelhe a estrutura do código de produção, facilitando a localização e manutenção dos testes.

## Passos de implementação

### 1. Verificar estrutura de pastas
- [ ] Verificar que estrutura de testes espelha código de produção:
  ```
  FastFood.PayStream.Tests.Unit/
  ├── Domain/
  │   ├── Entities/
  │   └── Common/
  │       ├── Exceptions/
  │       └── Enums/
  ├── Application/
  │   ├── UseCases/
  │   ├── Presenters/
  │   ├── InputModels/
  │   └── OutputModels/
  └── InterfacesExternas/
      └── Controllers/
  ```
- [ ] Criar pastas faltantes se necessário

### 2. Configurar FluentAssertions
- [ ] Adicionar `using FluentAssertions;` nos arquivos de teste
- [ ] Verificar que pacote FluentAssertions está instalado (Subtask 01)
- [ ] Criar exemplo de teste usando FluentAssertions para referência

### 3. Configurar Coverlet no .csproj
- [ ] Verificar que `coverlet.collector` está configurado
- [ ] Verificar que `coverlet.msbuild` está configurado
- [ ] Adicionar configuração de cobertura no `.csproj` se necessário:
  ```xml
  <PropertyGroup>
    <CollectCoverage>true</CollectCoverage>
    <CoverletOutputFormat>opencover</CoverletOutputFormat>
  </PropertyGroup>
  ```

### 4. Criar classe base ou helpers (opcional)
- [ ] Considerar criar classe base para testes comuns (se necessário)
- [ ] Criar helpers para criação de mocks (se necessário)
- [ ] Documentar padrões de teste do projeto

### 5. Remover arquivos de exemplo
- [ ] Remover `UnitTest1.cs` se existir e não for necessário
- [ ] Limpar código de exemplo não utilizado

## Como testar
- [ ] Executar `dotnet test` com cobertura localmente:
  ```bash
  dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
  ```
- [ ] Verificar que arquivo de cobertura é gerado
- [ ] Verificar que estrutura de pastas está organizada
- [ ] Executar testes existentes para garantir que FluentAssertions funciona

## Critérios de aceite
- [ ] Estrutura de pastas espelha código de produção
- [ ] FluentAssertions configurado e funcionando
- [ ] Coverlet configurado corretamente
- [ ] Testes existentes continuam funcionando
- [ ] Cobertura pode ser gerada localmente
- [ ] Estrutura está organizada e fácil de navegar

## Referências
- [Documento de Lições Aprendidas - Estrutura de Pastas](./docs/PROMPT_MICROSERVICOS_TESTES_DEPLOY.md#estrutura-de-pastas)
- [Padrão AAA](./docs/PROMPT_MICROSERVICOS_TESTES_DEPLOY.md#padrão-aaa-arrange-act-assert)
