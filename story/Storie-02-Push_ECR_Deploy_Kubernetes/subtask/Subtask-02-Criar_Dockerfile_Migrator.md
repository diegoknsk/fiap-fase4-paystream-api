# Subtask 02: Criar Dockerfile para Migrator

## Descrição
Criar o Dockerfile para o Migrator FastFood.PayStream.Migrator usando multi-stage build, garantindo que as migrações do Entity Framework Core sejam incluídas na imagem.

## Passos de implementação
- Criar arquivo `Dockerfile` em `src/InterfacesExternas/FastFood.PayStream.Migrator/`
- Usar multi-stage build:
  - Stage 1 (build): Usar `mcr.microsoft.com/dotnet/sdk:8.0` para compilar o Migrator
  - Stage 2 (runtime): Usar `mcr.microsoft.com/dotnet/aspnet:8.0` para executar o Migrator
- Configurar WORKDIR apropriado
- Copiar arquivos `.csproj` e fazer restore antes de copiar todo o código
- Publicar o Migrator com configurações otimizadas
- Copiar arquivo `appsettings.json` do Migrator para a imagem
- Copiar pasta `Migrations/` do projeto `FastFood.PayStream.Infra.Persistence` (em `src/Infra/`) para a imagem
- Definir ENTRYPOINT para executar `FastFood.PayStream.Migrator.dll`

## Como testar
- Executar `docker build -t paystream-migrator:test -f src/InterfacesExternas/FastFood.PayStream.Migrator/Dockerfile .` na raiz do projeto
- Verificar que a imagem é criada sem erros
- Verificar que a pasta `Migrations/` está presente na imagem:
  - `docker run --entrypoint ls paystream-migrator:test -la Migrations/`
- Verificar que o `appsettings.json` está presente na imagem

## Critérios de aceite
- Dockerfile criado em `src/InterfacesExternas/FastFood.PayStream.Migrator/Dockerfile`
- Usa multi-stage build (build stage + runtime stage)
- Imagem inclui as migrações do Entity Framework Core
- Imagem inclui o arquivo `appsettings.json` do Migrator
- Imagem pode ser construída sem erros
- Tamanho da imagem final é otimizado


