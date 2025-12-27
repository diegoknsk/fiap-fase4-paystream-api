# Subtask 01: Criar Dockerfile para API

## Descrição
Criar o Dockerfile para a API FastFood.PayStream.Api usando multi-stage build para otimizar o tamanho da imagem final e garantir que apenas os arquivos necessários sejam incluídos.

## Passos de implementação
- Criar arquivo `Dockerfile` em `src/InterfacesExternas/FastFood.PayStream.Api/`
- Usar multi-stage build:
  - Stage 1 (build): Usar `mcr.microsoft.com/dotnet/sdk:8.0` para compilar a aplicação
  - Stage 2 (runtime): Usar `mcr.microsoft.com/dotnet/aspnet:8.0` para executar a aplicação
- Configurar WORKDIR apropriado
- Copiar arquivos `.csproj` e fazer restore antes de copiar todo o código (otimização de cache)
- Publicar a aplicação com configurações otimizadas:
  - `-c Release`
  - `/p:CopyOutputSymbolsToPublishDirectory=false`
  - `/p:CopyOutputXmlDocumentationToPublishDirectory=false`
- Configurar variável de ambiente `ASPNETCORE_URLS=http://+:80`
- Expor porta 80
- Definir ENTRYPOINT para executar `FastFood.PayStream.Api.dll`

## Como testar
- Executar `docker build -t paystream-api:test -f src/InterfacesExternas/FastFood.PayStream.Api/Dockerfile .` na raiz do projeto
- Verificar que a imagem é criada sem erros
- Executar `docker run -p 8080:80 paystream-api:test` e verificar que a API inicia
- Acessar `http://localhost:8080/api/hello` e verificar que retorna "Olá Mundo"
- Verificar tamanho da imagem com `docker images paystream-api:test`

## Critérios de aceite
- Dockerfile criado em `src/InterfacesExternas/FastFood.PayStream.Api/Dockerfile`
- Usa multi-stage build (build stage + runtime stage)
- Imagem pode ser construída sem erros
- Imagem pode ser executada e a API responde corretamente
- Tamanho da imagem final é otimizado (não inclui SDK completo)


