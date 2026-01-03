# Subtask 01: Criar Classes de Configuração (JwtOptions, CognitoOptions)

## Status
- **Estado:** 🔄 Pendente
- **Data de Início:** -
- **Data de Conclusão:** -

## Descrição
Criar ou verificar se existe o projeto `FastFood.PayStream.Infra.Auth` e copiar as classes de configuração do projeto orderhub (`JwtOptions.cs` e `CognitoOptions.cs`), adaptando os namespaces para o projeto paystream.

## Objetivo
Criar classes de configuração para JWT (CustomerBearer) e Cognito, copiando exatamente a implementação do projeto orderhub e adaptando apenas os namespaces.

## Arquivos a Criar/Copiar

### 1. Verificar/Criar projeto Infra.Auth
- Verificar se existe `src/Infra/FastFood.PayStream.Infra.Auth/`
- Se não existir, criar o projeto seguindo o padrão dos outros projetos Infra
- Adicionar referências necessárias:
  - `Microsoft.AspNetCore.Authentication.JwtBearer`
  - `Microsoft.Extensions.Configuration`
  - `Microsoft.Extensions.DependencyInjection`

### 2. `src/Infra/FastFood.PayStream.Infra.Auth/JwtOptions.cs`

Copiar do orderhub e adaptar namespace:

```csharp
namespace FastFood.PayStream.Infra.Auth;

public sealed record JwtOptions(string Issuer, string Audience, string SecretKey, int ExpiresInMinutes);
```

**Características:**
- Record type (imutável)
- Propriedades: Issuer, Audience, SecretKey, ExpiresInMinutes
- Namespace: `FastFood.PayStream.Infra.Auth`

### 3. `src/Infra/FastFood.PayStream.Infra.Auth/CognitoOptions.cs`

Copiar do orderhub e adaptar namespace:

```csharp
namespace FastFood.PayStream.Infra.Auth;

public sealed class CognitoOptions
{
    public const string SectionName = "Authentication:Cognito";
    public string UserPoolId { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string Region { get; set; } = "us-east-1";
    public int? ClockSkewMinutes { get; set; } = 5;
    public string Authority => $"https://cognito-idp.{Region}.amazonaws.com/{UserPoolId}";
}
```

**Características:**
- Classe sealed
- Constante `SectionName` para referência na configuração
- Propriedade calculada `Authority` baseada em Region e UserPoolId
- Valores padrão: Region = "us-east-1", ClockSkewMinutes = 5

## Passos de Implementação

1. [ ] Verificar se existe o projeto `FastFood.PayStream.Infra.Auth`
2. [ ] Se não existir, criar o projeto .csproj seguindo o padrão
3. [ ] Adicionar pacotes NuGet necessários:
   - `Microsoft.AspNetCore.Authentication.JwtBearer`
   - `Microsoft.Extensions.Configuration`
   - `Microsoft.Extensions.DependencyInjection`
   - `Microsoft.Extensions.Options`
4. [ ] Copiar `JwtOptions.cs` do orderhub e adaptar namespace
5. [ ] Copiar `CognitoOptions.cs` do orderhub e adaptar namespace
6. [ ] Verificar que o código compila sem erros

## Como Testar

- Executar `dotnet build` no projeto Infra.Auth (deve compilar sem erros)
- Verificar que os namespaces estão corretos
- Validar que as classes têm a mesma estrutura do orderhub

## Critérios de Aceite

- [ ] Projeto `FastFood.PayStream.Infra.Auth` existe ou foi criado
- [ ] Pacotes NuGet necessários adicionados
- [ ] `JwtOptions.cs` criado com estrutura idêntica ao orderhub
- [ ] `CognitoOptions.cs` criado com estrutura idêntica ao orderhub
- [ ] Namespaces adaptados para `FastFood.PayStream.Infra.Auth`
- [ ] Código compila sem erros
- [ ] Estrutura das classes é idêntica ao orderhub

## Referências

- **Arquivo de referência (OrderHub):** `C:\Projetos\Fiap\fiap-fase4-orderhub-api\src\Infra\FastFood.OrderHub.Infra.Auth\JwtOptions.cs`
- **Arquivo de referência (OrderHub):** `C:\Projetos\Fiap\fiap-fase4-orderhub-api\src\Infra\FastFood.OrderHub.Infra.Auth\CognitoOptions.cs`
