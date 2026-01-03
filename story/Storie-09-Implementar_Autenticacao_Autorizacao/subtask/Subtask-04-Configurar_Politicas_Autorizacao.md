# Subtask 04: Configurar Políticas de Autorização

## Status
- **Estado:** 🔄 Pendente
- **Data de Início:** -
- **Data de Conclusão:** -

## Descrição
Copiar e adaptar a implementação de `AuthorizationConfig.cs` do projeto orderhub, que contém as políticas de autorização (Admin e Customer).

## Objetivo
Criar a classe `AuthorizationConfig` com o método de extensão `AddAuthorizationPolicies` que configura as políticas de autorização para Admin (Cognito) e Customer (CustomerBearer).

## Arquivo a Criar

### `src/Infra/FastFood.PayStream.Infra.Auth/AuthorizationConfig.cs`

Copiar do orderhub e adaptar namespace:

```csharp
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace FastFood.PayStream.Infra.Auth
{
    /// <summary>
    /// Configuração de políticas de autorização
    /// </summary>
    public static class AuthorizationConfig
    {
        public const string AdminPolicy = "Admin";
        public const string CustomerPolicy = "Customer";
        public const string CustomerWithScopePolicy = "CustomerWithScope";

        /// <summary>
        /// Adiciona as políticas de autorização
        /// </summary>
        public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                // Política para Admin (Cognito)
                options.AddPolicy(AdminPolicy, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "aws.cognito.signin.user.admin");
                });

                // Política para Customer (JWT Bearer)
                options.AddPolicy(CustomerPolicy, policy =>
                {
                    policy.RequireAuthenticatedUser();
                });

                // Política para Customer com validação de scope e role
                options.AddPolicy(CustomerWithScopePolicy, policy =>
                {
                    policy.RequireAssertion(context =>
                        context.User.HasClaim("role", "customer") && 
                        context.User.HasClaim("scope", "customer"));
                });
            });

            return services;
        }
    }
}
```

## Passos de Implementação

1. [ ] Copiar `AuthorizationConfig.cs` do orderhub
2. [ ] Adaptar namespace para `FastFood.PayStream.Infra.Auth`
3. [ ] Verificar que todas as dependências estão disponíveis
4. [ ] Verificar que o código compila sem erros

## Políticas de Autorização

### 1. Admin Policy
- **Nome:** "Admin"
- **Requisitos:**
  - Usuário autenticado
  - Claim `scope` com valor `aws.cognito.signin.user.admin`
- **Uso:** Endpoints que requerem autenticação Cognito de administrador

### 2. Customer Policy
- **Nome:** "Customer"
- **Requisitos:**
  - Usuário autenticado
- **Uso:** Endpoints que requerem autenticação CustomerBearer (tokens JWT do Lambda Customer)

### 3. CustomerWithScope Policy
- **Nome:** "CustomerWithScope"
- **Requisitos:**
  - Claim `role` com valor `customer`
  - Claim `scope` com valor `customer`
- **Uso:** Política adicional para validações mais específicas (pode não ser usada inicialmente)

## Como Testar

- Executar `dotnet build` no projeto Infra.Auth (deve compilar sem erros)
- Verificar que o método de extensão está acessível
- Validar que a estrutura é idêntica ao orderhub

## Critérios de Aceite

- [ ] `AuthorizationConfig.cs` criado com estrutura idêntica ao orderhub
- [ ] Namespace adaptado para `FastFood.PayStream.Infra.Auth`
- [ ] Constantes `AdminPolicy`, `CustomerPolicy`, `CustomerWithScopePolicy` definidas
- [ ] Método `AddAuthorizationPolicies` implementado
- [ ] Política "Admin" configurada com validação de scope
- [ ] Política "Customer" configurada (apenas autenticação)
- [ ] Política "CustomerWithScope" configurada (opcional)
- [ ] Código compila sem erros
- [ ] Estrutura é idêntica ao orderhub

## Referências

- **Arquivo de referência (OrderHub):** `C:\Projetos\Fiap\fiap-fase4-orderhub-api\src\Infra\FastFood.OrderHub.Infra.Auth\AuthorizationConfig.cs`
