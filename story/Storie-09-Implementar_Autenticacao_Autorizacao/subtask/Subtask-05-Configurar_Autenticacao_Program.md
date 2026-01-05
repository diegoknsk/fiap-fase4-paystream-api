# Subtask 05: Configurar Autenticação no Program.cs

## Status
- **Estado:** 🔄 Pendente
- **Data de Início:** -
- **Data de Conclusão:** -

## Descrição
Configurar autenticação e autorização no `Program.cs` da API, adicionando as extensões de autenticação CustomerBearer e Cognito, e configurando as políticas de autorização.

## Objetivo
Adicionar toda a configuração de autenticação no `Program.cs`, incluindo:
1. Configuração do JWT Security Token Handler
2. Configuração de autenticação CustomerBearer
3. Configuração de autenticação Cognito
4. Configuração de políticas de autorização
5. Middleware de autenticação e autorização

## Arquivo a Modificar

### `src/InterfacesExternas/FastFood.PayStream.Api/Program.cs`

## Passos de Implementação

1. [ ] Adicionar using no início do arquivo:
   ```csharp
   using FastFood.PayStream.Infra.Auth;
   using Microsoft.AspNetCore.Authentication.JwtBearer;
   ```

2. [ ] Adicionar no início do arquivo (antes de `var builder = ...`):
   ```csharp
   // Configurar JWT Security Token Handler
   JwtAuthenticationConfig.ConfigureJwtSecurityTokenHandler();
   ```

3. [ ] Adicionar referência ao projeto Infra.Auth no projeto Api (se ainda não tiver)

4. [ ] Adicionar configuração de autenticação após `builder.Services.AddControllers()`:
   ```csharp
   // Configure authentication
   builder.Services
       .AddAuthentication(options =>
       {
           options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
           options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
       })
       .AddCustomerJwtBearer(builder.Configuration)
       .AddCognitoJwtBearer(builder.Configuration);
   ```

5. [ ] Adicionar configuração de políticas de autorização:
   ```csharp
   // Configure authorization policies
   builder.Services.AddAuthorizationPolicies();
   ```

6. [ ] Adicionar middleware de autenticação e autorização (antes de `app.MapControllers()`):
   ```csharp
   app.UseAuthentication();
   app.UseAuthorization();
   ```

7. [ ] Verificar que o código compila sem erros

## Ordem de Configuração no Program.cs

A ordem deve ser:

```csharp
// 1. Configurar JWT Security Token Handler (no início)
JwtAuthenticationConfig.ConfigureJwtSecurityTokenHandler();

var builder = WebApplication.CreateBuilder(args);

// 2. Configurações existentes (DbContext, Repositories, etc.)

// 3. Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(...);

// 4. Configure authentication
builder.Services
    .AddAuthentication(...)
    .AddCustomerJwtBearer(builder.Configuration)
    .AddCognitoJwtBearer(builder.Configuration);

// 5. Configure authorization policies
builder.Services.AddAuthorizationPolicies();

var app = builder.Build();

// 6. Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 7. Authentication e Authorization middleware (IMPORTANTE: nesta ordem)
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
```

## Como Testar

- Executar `dotnet build` no projeto Api (deve compilar sem erros)
- Verificar que não há erros de referência
- Validar que a ordem dos middlewares está correta

## Critérios de Aceite

- [ ] `JwtAuthenticationConfig.ConfigureJwtSecurityTokenHandler()` chamado no início
- [ ] Using `FastFood.PayStream.Infra.Auth` adicionado
- [ ] Referência ao projeto Infra.Auth adicionada no projeto Api
- [ ] Configuração de autenticação adicionada com CustomerBearer e Cognito
- [ ] Configuração de políticas de autorização adicionada
- [ ] `app.UseAuthentication()` adicionado antes de `app.UseAuthorization()`
- [ ] Ordem dos middlewares está correta
- [ ] Código compila sem erros
- [ ] Estrutura é idêntica ao orderhub

## Observações Importantes

- **Ordem dos Middlewares:** `UseAuthentication()` deve vir ANTES de `UseAuthorization()`
- **Ordem no Pipeline:** Authentication e Authorization devem vir ANTES de `MapControllers()`
- **Default Schemes:** Configurados como `JwtBearerDefaults.AuthenticationScheme` (padrão)

## Referências

- **Arquivo de referência (OrderHub):** `C:\Projetos\Fiap\fiap-fase4-orderhub-api\src\InterfacesExternas\FastFood.OrderHub.Api\Program.cs`
