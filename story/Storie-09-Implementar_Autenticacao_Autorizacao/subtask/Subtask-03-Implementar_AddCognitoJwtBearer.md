# Subtask 03: Implementar Extensão AddCognitoJwtBearer

## Status
- **Estado:** 🔄 Pendente
- **Data de Início:** -
- **Data de Conclusão:** -

## Descrição
Copiar e adaptar a implementação de `CognitoAuthenticationConfig.cs` do projeto orderhub, que contém a extensão `AddCognitoJwtBearer` para configurar autenticação JWT Bearer para tokens do AWS Cognito.

## Objetivo
Criar a classe `CognitoAuthenticationConfig` com o método de extensão `AddCognitoJwtBearer` que configura o esquema de autenticação "Cognito" para validar tokens Access Token do AWS Cognito gerados pelo Lambda Admin.

## Arquivo a Criar

### `src/Infra/FastFood.PayStream.Infra.Auth/CognitoAuthenticationConfig.cs`

Copiar do orderhub e adaptar namespace:

```csharp
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FastFood.PayStream.Infra.Auth
{
    /// <summary>
    /// Configuração de autenticação JWT Bearer para Cognito
    /// </summary>
    public static class CognitoAuthenticationConfig
    {
        /// <summary>
        /// Adiciona autenticação JWT Bearer para Cognito
        /// </summary>
        public static AuthenticationBuilder AddCognitoJwtBearer(this AuthenticationBuilder authBuilder, IConfiguration configuration)
        {
            // Configurar CognitoOptions
            var services = authBuilder.Services;
            services.Configure<CognitoOptions>(
                configuration.GetSection(CognitoOptions.SectionName));

            var cognito = new CognitoOptions();
            configuration.GetSection(CognitoOptions.SectionName).Bind(cognito);

            // Adicionar JWT Bearer para Cognito
            return authBuilder.AddJwtBearer("Cognito", options =>
                {
                    options.Authority = cognito.Authority;
                    options.RequireHttpsMetadata = false; // Para desenvolvimento local
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = cognito.Authority,
                        ValidateAudience = false, // Cognito Access Token não tem 'aud'
                        ValidAudience = cognito.ClientId,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.FromMinutes(cognito.ClockSkewMinutes ?? 5),
                        NameClaimType = "username"
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = ctx =>
                        {
                            Console.WriteLine($"Authentication failed: {ctx.Exception?.Message}");
                            Console.WriteLine($"Exception type: {ctx.Exception?.GetType().Name}");
                            if (ctx.Exception?.InnerException != null)
                            {
                                Console.WriteLine($"Inner exception: {ctx.Exception.InnerException.Message}");
                            }
                            return Task.CompletedTask;
                        },
                        OnChallenge = ctx =>
                        {
                            Console.WriteLine($"Challenge: {ctx.Error} - {ctx.ErrorDescription}");
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = ctx =>
                        {
                            var claims = ctx.Principal?.Claims?.ToDictionary(c => c.Type, c => c.Value);
                            Console.WriteLine($"Token validated. Claims count: {claims?.Count ?? 0}");

                            if (claims is null)
                            {
                                Console.WriteLine("Token sem claims.");
                                ctx.Fail("Token sem claims.");
                                return Task.CompletedTask;
                            }

                            if (!claims.TryGetValue("token_use", out var tokenUse) || tokenUse != "access")
                            {
                                Console.WriteLine($"Token use inválido: {tokenUse}");
                                ctx.Fail("Token não é Access Token.");
                                return Task.CompletedTask;
                            }

                            if (!claims.TryGetValue("client_id", out var clientId) || clientId != cognito.ClientId)
                            {
                                Console.WriteLine($"Client ID inválido. Esperado: {cognito.ClientId}, Recebido: {clientId}");
                                ctx.Fail("client_id inválido para esta API.");
                                return Task.CompletedTask;
                            }

                            Console.WriteLine("Token validado com sucesso!");
                            return Task.CompletedTask;
                        }
                    };
                });

            return authBuilder;
        }
    }
}
```

## Passos de Implementação

1. [ ] Copiar `CognitoAuthenticationConfig.cs` do orderhub
2. [ ] Adaptar namespace para `FastFood.PayStream.Infra.Auth`
3. [ ] Verificar que todas as dependências estão disponíveis
4. [ ] Verificar que o código compila sem erros

## Características Importantes

- **Scheme Name:** "Cognito" (deve ser exatamente este nome)
- **Configuration Section:** "Authentication:Cognito" (via `CognitoOptions.SectionName`)
- **Authority:** Construído automaticamente a partir de Region e UserPoolId
- **Validações no OnTokenValidated:**
  - Verifica que `token_use == "access"`
  - Verifica que `client_id` corresponde ao configurado
- **RequireHttpsMetadata:** false (para desenvolvimento local)
- **NameClaimType:** "username"

## Como Testar

- Executar `dotnet build` no projeto Infra.Auth (deve compilar sem erros)
- Verificar que o método de extensão está acessível
- Validar que a estrutura é idêntica ao orderhub

## Critérios de Aceite

- [ ] `CognitoAuthenticationConfig.cs` criado com estrutura idêntica ao orderhub
- [ ] Namespace adaptado para `FastFood.PayStream.Infra.Auth`
- [ ] Método `AddCognitoJwtBearer` implementado
- [ ] Configuração de `CognitoOptions` via DI implementada
- [ ] Validações no `OnTokenValidated` implementadas:
  - Validação de `token_use == "access"`
  - Validação de `client_id`
- [ ] Eventos de autenticação configurados (OnAuthenticationFailed, OnChallenge, OnTokenValidated)
- [ ] Código compila sem erros
- [ ] Estrutura é idêntica ao orderhub

## Observações

- Mesmo que não usemos autenticação Cognito por enquanto, a infraestrutura deve estar configurada
- Isso permite que no futuro possamos usar tokens Cognito sem precisar refatorar
- As configurações devem ser idênticas ao orderhub (mesmo UserPoolId, ClientId, Region)

## Referências

- **Arquivo de referência (OrderHub):** `C:\Projetos\Fiap\fiap-fase4-orderhub-api\src\Infra\FastFood.OrderHub.Infra.Auth\CognitoAuthenticationConfig.cs`
