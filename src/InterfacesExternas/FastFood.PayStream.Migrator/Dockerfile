# Etapa 1: build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copiar arquivos .csproj e fazer restore (otimização de cache)
COPY src/Core/FastFood.PayStream.Domain/FastFood.PayStream.Domain.csproj src/Core/FastFood.PayStream.Domain/
COPY src/Core/FastFood.PayStream.Application/FastFood.PayStream.Application.csproj src/Core/FastFood.PayStream.Application/
COPY src/Core/FastFood.PayStream.CrossCutting/FastFood.PayStream.CrossCutting.csproj src/Core/FastFood.PayStream.CrossCutting/
COPY src/Infra/FastFood.PayStream.Infra/FastFood.PayStream.Infra.csproj src/Infra/FastFood.PayStream.Infra/
COPY src/Infra/FastFood.PayStream.Infra.Persistence/FastFood.PayStream.Infra.Persistence.csproj src/Infra/FastFood.PayStream.Infra.Persistence/
COPY src/InterfacesExternas/FastFood.PayStream.Migrator/FastFood.PayStream.Migrator.csproj src/InterfacesExternas/FastFood.PayStream.Migrator/

RUN dotnet restore src/InterfacesExternas/FastFood.PayStream.Migrator/FastFood.PayStream.Migrator.csproj

# Copiar todo o código
COPY . .

# Publicar o Migrator
RUN dotnet publish src/InterfacesExternas/FastFood.PayStream.Migrator/FastFood.PayStream.Migrator.csproj -c Release -o /out \
    /p:CopyOutputSymbolsToPublishDirectory=false \
    /p:CopyOutputXmlDocumentationToPublishDirectory=false

# Preparar migrações: copiar para pasta temporária, garantindo que sempre exista
RUN mkdir -p /migrations && \
    if [ -d "src/Infra/FastFood.PayStream.Infra.Persistence/Migrations" ] && [ "$(find src/Infra/FastFood.PayStream.Infra.Persistence/Migrations -type f 2>/dev/null | wc -l)" -gt 0 ]; then \
        cp -r src/Infra/FastFood.PayStream.Infra.Persistence/Migrations/* /migrations/ 2>/dev/null || true; \
    fi && \
    # Garantir que a pasta não esteja vazia (evita erro no COPY)
    if [ ! "$(ls -A /migrations 2>/dev/null)" ]; then \
        touch /migrations/.keep; \
    fi

# Etapa 2: imagem final para execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /out ./

# Copiar arquivos de configuração
COPY src/InterfacesExternas/FastFood.PayStream.Migrator/appsettings.json ./appsettings.json

# Copiar migrações do build stage (sempre terá pelo menos o arquivo .keep)
COPY --from=build /migrations ./Migrations

ENTRYPOINT ["dotnet", "FastFood.PayStream.Migrator.dll"]
