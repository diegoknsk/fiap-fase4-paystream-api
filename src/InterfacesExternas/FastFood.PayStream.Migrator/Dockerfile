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

# Etapa 2: imagem final para execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /out ./

# Copiar arquivos de configuração
COPY src/InterfacesExternas/FastFood.PayStream.Migrator/appsettings.json ./appsettings.json

# Copiar migrações do projeto FastFood.PayStream.Infra.Persistence
COPY src/Infra/FastFood.PayStream.Infra.Persistence/Migrations ./Migrations

ENTRYPOINT ["dotnet", "FastFood.PayStream.Migrator.dll"]


