# Subtask 06: Configurar Swagger com Múltiplos Esquemas

## Status
- **Estado:** 🔄 Pendente
- **Data de Início:** -
- **Data de Conclusão:** -

## Descrição
Configurar Swagger para suportar múltiplos esquemas de autenticação (CustomerBearer e Cognito), permitindo testar endpoints com ambos os tipos de token.

## Objetivo
Adicionar definições de segurança no Swagger para CustomerBearer e Cognito, permitindo que o Swagger UI exiba botões de autenticação e permita testar endpoints com tokens.

## Arquivo a Modificar

### `src/InterfacesExternas/FastFood.PayStream.Api/Program.cs`

## Passos de Implementação

1. [ ] Adicionar using no início do arquivo (se ainda não tiver):
   ```csharp
   using Microsoft.OpenApi.Models;
   ```

2. [ ] Modificar a configuração do Swagger em `builder.Services.AddSwaggerGen()`:
   ```csharp
   builder.Services.AddSwaggerGen(c =>
   {
       var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
       var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
       if (File.Exists(xmlPath))
       {
           c.IncludeXmlComments(xmlPath);
       }

       // CustomerBearer scheme
       c.AddSecurityDefinition("CustomerBearer", new OpenApiSecurityScheme
       {
           In = ParameterLocation.Header,
           Name = "Authorization",
           Type = SecuritySchemeType.Http,
           Scheme = "bearer",
           BearerFormat = "JWT",
           Description = "Customer token (Bearer {token})"
       });

       // Cognito scheme
       c.AddSecurityDefinition("Cognito", new OpenApiSecurityScheme
       {
           In = ParameterLocation.Header,
           Name = "Authorization",
           Type = SecuritySchemeType.Http,
           Scheme = "bearer",
           BearerFormat = "JWT",
           Description = "JWT Bearer do Cognito. Ex: 'Bearer {token}'"
       });

       c.OperationFilter<AuthorizeBySchemeOperationFilter>();
   });
   ```

3. [ ] Verificar que o código compila sem erros

## Como Testar

- Executar `dotnet build` no projeto Api (deve compilar sem erros)
- Executar `dotnet run` e acessar Swagger UI
- Verificar que aparece um botão "Authorize" no Swagger
- Verificar que é possível selecionar entre "CustomerBearer" e "Cognito"
- Verificar que endpoints protegidos mostram o ícone de cadeado

## Critérios de Aceite

- [ ] Using `Microsoft.OpenApi.Models` adicionado
- [ ] Security definition "CustomerBearer" adicionada
- [ ] Security definition "Cognito" adicionada
- [ ] `OperationFilter<AuthorizeBySchemeOperationFilter>()` adicionado
- [ ] Código compila sem erros
- [ ] Swagger UI exibe botão "Authorize"
- [ ] É possível selecionar esquema de autenticação no Swagger
- [ ] Endpoints protegidos mostram ícone de cadeado

## Observações

- O `AuthorizeBySchemeOperationFilter` será criado na próxima subtask
- Por enquanto, pode deixar comentado ou criar um filtro básico se necessário
- As definições de segurança devem corresponder exatamente aos nomes dos esquemas: "CustomerBearer" e "Cognito"
