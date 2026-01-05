# Subtask 02: Implementar Extensão AddCustomerJwtBearer

## Status
- **Estado:** 🔄 Pendente
- **Data de Início:** -
- **Data de Conclusão:** -

## Descrição
Copiar e adaptar a implementação de `JwtAuthenticationConfig.cs` do projeto orderhub, que contém a extensão `AddCustomerJwtBearer` para configurar autenticação JWT Bearer para tokens de Customer.

## Objetivo
Criar a classe `JwtAuthenticationConfig` com o método de extensão `AddCustomerJwtBearer` que configura o esquema de autenticação "CustomerBearer" para validar tokens JWT gerados pelo Lambda Customer.

## Arquivo a Criar

### `src/Infra/FastFood.PayStream.Infra.Auth/JwtAuthenticationConfig.cs`

Copiar do orderhub e adaptar namespace:

```csharp
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace FastFood.PayStream.Infra.Auth
{
    /// <summary>
    /// Configuração de autenticação JWT Bearer para Customer
    /// </summary>
    public static class JwtAuthenticationConfig
    {
        private const string CustomerSchemeName = "CustomerBearer";
        private const string ConfigurationSection = "JwtCustomer";

        /// <summary>
        /// Adiciona autenticação JWT Bearer para Customer
        /// </summary>
        public static AuthenticationBuilder AddCustomerJwtBearer(this AuthenticationBuilder authBuilder, IConfiguration configuration)
        {
            return authBuilder.AddJwtBearer(CustomerSchemeName, options =>
            {
                options.TokenValidationParameters = BuildTokenValidationParameters(configuration, ConfigurationSection);
            });
        }

        /// <summary>
        /// Constrói os parâmetros de validação do token JWT
        /// </summary>
        private static TokenValidationParameters BuildTokenValidationParameters(IConfiguration configuration, string section)
        {
            var issuer = configuration[$"{section}:Issuer"];
            var audience = configuration[$"{section}:Audience"];
            var secret = configuration[$"{section}:SecretKey"];

            if (string.IsNullOrWhiteSpace(issuer))
                throw new InvalidOperationException($"JWT Issuer não configurado na seção {section}:Issuer");
            
            if (string.IsNullOrWhiteSpace(audience))
                throw new InvalidOperationException($"JWT Audience não configurado na seção {section}:Audience");
            
            if (string.IsNullOrWhiteSpace(secret))
                throw new InvalidOperationException($"JWT SecretKey não configurado na seção {section}:SecretKey");

            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                ClockSkew = TimeSpan.FromSeconds(30),
                RoleClaimType = "role",
                NameClaimType = JwtRegisteredClaimNames.Sub
            };
        }

        /// <summary>
        /// Configura o JWT Security Token Handler para desabilitar mapeamento automático de claims
        /// </summary>
        public static void ConfigureJwtSecurityTokenHandler()
        {
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
        }
    }
}
```

## Passos de Implementação

1. [ ] Copiar `JwtAuthenticationConfig.cs` do orderhub
2. [ ] Adaptar namespace para `FastFood.PayStream.Infra.Auth`
3. [ ] Verificar que todas as dependências estão disponíveis
4. [ ] Adicionar pacote `System.IdentityModel.Tokens.Jwt` se necessário
5. [ ] Verificar que o código compila sem erros

## Características Importantes

- **Scheme Name:** "CustomerBearer" (deve ser exatamente este nome)
- **Configuration Section:** "JwtCustomer" (deve corresponder ao appsettings.json)
- **Validações:** Issuer, Audience, SecretKey, Lifetime
- **ClockSkew:** 30 segundos de tolerância
- **NameClaimType:** `JwtRegisteredClaimNames.Sub` (para extrair CustomerId)
- **DefaultMapInboundClaims:** Deve ser desabilitado (chamado no Program.cs)

## Como Testar

- Executar `dotnet build` no projeto Infra.Auth (deve compilar sem erros)
- Verificar que o método de extensão está acessível
- Validar que a estrutura é idêntica ao orderhub

## Critérios de Aceite

- [ ] `JwtAuthenticationConfig.cs` criado com estrutura idêntica ao orderhub
- [ ] Namespace adaptado para `FastFood.PayStream.Infra.Auth`
- [ ] Método `AddCustomerJwtBearer` implementado
- [ ] Método `ConfigureJwtSecurityTokenHandler` implementado
- [ ] Método `BuildTokenValidationParameters` implementado
- [ ] Validações de configuração implementadas (exceções se faltar Issuer, Audience, SecretKey)
- [ ] Código compila sem erros
- [ ] Estrutura é idêntica ao orderhub

## Referências

- **Arquivo de referência (OrderHub):** `C:\Projetos\Fiap\fiap-fase4-orderhub-api\src\Infra\FastFood.OrderHub.Infra.Auth\JwtAuthenticationConfig.cs`
