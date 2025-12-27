# Etapa 1: build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copiar arquivos .csproj e fazer restore (otimização de cache)
COPY src/Core/FastFood.PayStream.Domain/FastFood.PayStream.Domain.csproj src/Core/FastFood.PayStream.Domain/
COPY src/Core/FastFood.PayStream.Application/FastFood.PayStream.Application.csproj src/Core/FastFood.PayStream.Application/
COPY src/Core/FastFood.PayStream.CrossCutting/FastFood.PayStream.CrossCutting.csproj src/Core/FastFood.PayStream.CrossCutting/
COPY src/Infra/FastFood.PayStream.Infra/FastFood.PayStream.Infra.csproj src/Infra/FastFood.PayStream.Infra/
COPY src/Infra/FastFood.PayStream.Infra.Persistence/FastFood.PayStream.Infra.Persistence.csproj src/Infra/FastFood.PayStream.Infra.Persistence/
COPY src/InterfacesExternas/FastFood.PayStream.Api/FastFood.PayStream.Api.csproj src/InterfacesExternas/FastFood.PayStream.Api/

RUN dotnet restore src/InterfacesExternas/FastFood.PayStream.Api/FastFood.PayStream.Api.csproj

# Copiar todo o código
COPY . .

# Publicar a aplicação
RUN dotnet publish src/InterfacesExternas/FastFood.PayStream.Api/FastFood.PayStream.Api.csproj -c Release -o /app/publish \
    /p:CopyOutputSymbolsToPublishDirectory=false \
    /p:CopyOutputXmlDocumentationToPublishDirectory=false

# Etapa 2: imagem de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:80

EXPOSE 80

ENTRYPOINT ["dotnet", "FastFood.PayStream.Api.dll"]


