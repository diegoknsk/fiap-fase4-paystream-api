# Storie-09: Implementar Autenticação e Autorização

## Status
- **Estado:** 🔄 Pendente
- **Data de Início:** -
- **Data de Conclusão:** -
- **Prioridade:** Alta

## Descrição
Como desenvolvedor, preciso implementar autenticação e autorização na API PayStream copiando exatamente o esquema de autenticação do projeto `fiap-fase4-orderhub-api`, utilizando dois esquemas de autenticação distintos:
1. **Cognito** - Para autenticação de administradores (mesmas configurações do orderhub)
2. **CustomerBearer** - Para autenticação de customers (tokens JWT gerados pelo Lambda Customer)

A solução deve funcionar com os tokens gerados pelos lambdas do projeto `fiap-fase4-auth-lambda`, validando corretamente os tokens JWT emitidos por esses serviços. As configurações devem ser idênticas ao orderhub pois ambos ficarão no mesmo cluster e terão as mesmas chaves.

## Objetivo Geral
1. Copiar e adaptar toda a estrutura de autenticação do projeto `C:\Projetos\Fiap\fiap-fase4-orderhub-api`
2. Configurar autenticação JWT Bearer para tokens de Customer (CustomerBearer scheme)
3. Configurar autenticação JWT Bearer para tokens do AWS Cognito (Cognito scheme)
4. Criar políticas de autorização (Admin e Customer)
5. Aplicar atributos `[Authorize]` nos controllers conforme regras:
   - **WebhookPaymentController**: `[AllowAnonymous]` (já configurado, manter)
   - **PaymentController**: Todos os endpoints com `[Authorize(AuthenticationSchemes = "CustomerBearer", Policy = "Customer")]`
6. Configurar Swagger para suportar múltiplos esquemas de autenticação
7. Garantir compatibilidade com tokens gerados pelos lambdas de autenticação
8. Trazer a inteligência para interpretar tokens do Cognito mesmo que não usemos por enquanto

## Contexto e Referências

### Projeto de Referência
- **Projeto OrderHub:** `C:\Projetos\Fiap\fiap-fase4-orderhub-api`
- **Lambdas de Autenticação:** `C:\Projetos\Fiap\fiap-fase4-auth-lambda\src\InterfacesExternas`

### Esquemas de Autenticação

#### 1. CustomerBearer (Tokens JWT do Lambda Customer)
- **Fonte:** Lambda `FastFood.Auth.Lambda.Customer`
- **Endpoints que geram tokens:**
  - `POST /api/customer/anonymous` - Cria customer anônimo e retorna token
  - `POST /api/customer/register` - Registra customer por CPF e retorna token
  - `POST /api/customer/identify` - Identifica customer existente por CPF e retorna token
- **Estrutura do Token JWT:**
  - **Claims obrigatórias:**
    - `sub`: CustomerId (Guid) - Subject do token
    - `customerId`: CustomerId (Guid) - ID do customer
    - `jti`: JWT ID (Guid) - Identificador único do token
    - `iat`: Issued At (Unix timestamp) - Data de emissão
  - **Configuração esperada:**
    - `JwtCustomer:Issuer` - Emissor do token (ex: "FastFood.Auth")
    - `JwtCustomer:Audience` - Audiência do token (ex: "FastFood.API")
    - `JwtCustomer:SecretKey` - Chave secreta para assinatura (deve ser a mesma usada no Lambda Customer e no orderhub)
    - `JwtCustomer:ExpiresInMinutes` - Tempo de expiração em minutos (ex: 1440 = 24 horas)

#### 2. Cognito (Tokens do AWS Cognito)
- **Fonte:** Lambda `FastFood.Auth.Lambda.Admin`
- **Endpoint que gera tokens:**
  - `POST /api/admin/login` - Autentica admin via AWS Cognito e retorna AccessToken/IdToken
- **Estrutura do Token:**
  - **Tipo:** Access Token do AWS Cognito
  - **Claims obrigatórias:**
    - `token_use`: Deve ser "access" (não "id")
    - `client_id`: Client ID do Cognito (deve corresponder ao configurado)
    - `username`: Username do admin
    - `scope`: Deve conter "aws.cognito.signin.user.admin"
  - **Configuração esperada:**
    - `Authentication:Cognito:Region` - Região do Cognito (ex: "us-east-1") - **MESMA DO ORDERHUB**
    - `Authentication:Cognito:UserPoolId` - ID do User Pool do Cognito - **MESMO DO ORDERHUB**
    - `Authentication:Cognito:ClientId` - Client ID do Cognito - **MESMO DO ORDERHUB**
    - `Authentication:Cognito:ClockSkewMinutes` - Tolerância de relógio (opcional, padrão: 5 minutos)
  - **Authority:** `https://cognito-idp.{Region}.amazonaws.com/{UserPoolId}`

## Endpoints e Autorização

### WebhookPaymentController
- **POST `/api/webhookpayment/payment-notification`** - Receber notificação de pagamento
  - `[AllowAnonymous]` - **JÁ CONFIGURADO, MANTER**
  - Este endpoint deve permanecer público para permitir chamadas do gateway de pagamento

### PaymentController
Todos os endpoints devem usar autenticação **CustomerBearer** com política **Customer**:

1. **POST `/api/payment/create`** - Criar pagamento
   - `[Authorize(AuthenticationSchemes = "CustomerBearer", Policy = "Customer")]`

2. **POST `/api/payment/generate-qrcode`** - Gerar QR Code
   - `[Authorize(AuthenticationSchemes = "CustomerBearer", Policy = "Customer")]`

3. **GET `/api/payment/receipt-from-gateway`** - Obter comprovante
   - `[Authorize(AuthenticationSchemes = "CustomerBearer", Policy = "Customer")]`

## Configurações Necessárias

### appsettings.json
```json
{
  "JwtCustomer": {
    "Issuer": "FastFood.Auth",
    "Audience": "FastFood.API",
    "SecretKey": "", // DEVE SER A MESMA CHAVE DO ORDERHUB E DO LAMBDA CUSTOMER
    "ExpiresInMinutes": 1440
  },
  "Authentication": {
    "Cognito": {
      "Region": "", // MESMA REGIÃO DO ORDERHUB
      "UserPoolId": "", // MESMO USER POOL ID DO ORDERHUB
      "ClientId": "", // MESMO CLIENT ID DO ORDERHUB
      "ClockSkewMinutes": 5
    }
  }
}
```

### Variáveis de Ambiente (Alternativa/Complemento)
- `JWTCUSTOMER__ISSUER` - Emissor do token JWT
- `JWTCUSTOMER__AUDIENCE` - Audiência do token JWT
- `JWTCUSTOMER__SECRETKEY` - Chave secreta do JWT (mesma do Lambda Customer e orderhub)
- `JWTCUSTOMER__EXPIRESINMINUTES` - Tempo de expiração em minutos
- `AUTHENTICATION__COGNITO__REGION` - Região do Cognito (mesma do orderhub)
- `AUTHENTICATION__COGNITO__USERPOOLID` - User Pool ID (mesmo do orderhub)
- `AUTHENTICATION__COGNITO__CLIENTID` - Client ID (mesmo do orderhub)
- `AUTHENTICATION__COGNITO__CLOCKSKEWMINUTES` - Tolerância de relógio (opcional)

## Arquivos a Criar

### 1. Configurações de Autenticação (copiar do orderhub)
- `src/Infra/FastFood.PayStream.Infra.Auth/JwtOptions.cs` (copiar de orderhub)
- `src/Infra/FastFood.PayStream.Infra.Auth/CognitoOptions.cs` (copiar de orderhub)
- `src/Infra/FastFood.PayStream.Infra.Auth/JwtAuthenticationConfig.cs` (copiar e adaptar de orderhub)
- `src/Infra/FastFood.PayStream.Infra.Auth/CognitoAuthenticationConfig.cs` (copiar de orderhub)
- `src/Infra/FastFood.PayStream.Infra.Auth/AuthorizationConfig.cs` (copiar de orderhub)

### 2. Swagger (copiar do orderhub)
- `src/InterfacesExternas/FastFood.PayStream.Api/Config/Auth/AuthorizeBySchemeOperationFilter.cs` (copiar de orderhub)

## Arquivos a Modificar

### 1. Program.cs
- Adicionar `JwtAuthenticationConfig.ConfigureJwtSecurityTokenHandler()` no início
- Adicionar configuração de autenticação JWT Bearer para CustomerBearer
- Adicionar configuração de autenticação Cognito
- Configurar políticas de autorização (Admin e Customer)
- Configurar Swagger com múltiplos esquemas de segurança
- Adicionar `app.UseAuthentication()` antes de `app.UseAuthorization()`

### 2. Controllers
- **WebhookPaymentController.cs:** Manter `[AllowAnonymous]` (já está configurado)
- **PaymentController.cs:** Adicionar `[Authorize(AuthenticationSchemes = "CustomerBearer", Policy = "Customer")]` em todos os endpoints

### 3. Projeto Infra
- Criar projeto `FastFood.PayStream.Infra.Auth` (se não existir) ou adicionar ao projeto Infra existente
- Adicionar referência ao projeto Auth no projeto Api

## Detalhamento Técnico

### 1. Configuração JWT Bearer (CustomerBearer)

Copiar exatamente a implementação do orderhub:

```csharp
// No início do Program.cs
JwtAuthenticationConfig.ConfigureJwtSecurityTokenHandler();

// Na configuração de serviços
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddCustomerJwtBearer(builder.Configuration)
    .AddCognitoJwtBearer(builder.Configuration);
```

**Importante:**
- Desabilitar mapeamento automático de claims: `JwtSecurityTokenHandler.DefaultMapInboundClaims = false;`
- Validar que o token contém as claims obrigatórias (`sub`, `customerId`, `jti`, `iat`)
- Usar a mesma chave secreta do orderhub e do Lambda Customer

### 2. Configuração Cognito JWT Bearer

Copiar exatamente a implementação do orderhub:

```csharp
.AddCognitoJwtBearer(builder.Configuration);
```

**Implementação do método de extensão:**
- Configurar `Authority` baseado em `Region` e `UserPoolId`
- Validar `token_use == "access"`
- Validar `client_id` corresponde ao configurado
- Configurar eventos `OnTokenValidated` para validações adicionais
- **Usar as mesmas configurações do orderhub** (mesmo UserPoolId, ClientId, Region)

### 3. Políticas de Autorização

Copiar exatamente a implementação do orderhub:

```csharp
builder.Services.AddAuthorizationPolicies();
```

**Políticas:**
- **Admin:** Requer autenticação e claim `scope` com valor `aws.cognito.signin.user.admin`
- **Customer:** Requer apenas autenticação (para tokens CustomerBearer)

### 4. Configuração Swagger

Copiar exatamente a implementação do orderhub:

```csharp
builder.Services.AddSwaggerGen(c =>
{
    // ... configurações existentes ...

    // CustomerBearer scheme
    c.AddSecurityDefinition("CustomerBearer", new()
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Customer token (Bearer {token})"
    });

    // Cognito scheme
    c.AddSecurityDefinition("Cognito", new()
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

## Validações e Testes

### Validações de Compatibilidade

1. **Token Customer (CustomerBearer):**
   - Token gerado pelo Lambda Customer deve ser aceito
   - Claims `sub`, `customerId`, `jti`, `iat` devem estar presentes
   - Token expirado deve retornar 401
   - Token com assinatura inválida deve retornar 401
   - Token com issuer/audience incorretos deve retornar 401
   - **Token válido do orderhub deve funcionar no paystream** (mesma chave)

2. **Token Cognito (Admin):**
   - Access Token gerado pelo Lambda Admin deve ser aceito
   - `token_use` deve ser "access"
   - `client_id` deve corresponder ao configurado
   - Token expirado deve retornar 401
   - Token inválido deve retornar 401
   - **Token válido do orderhub deve funcionar no paystream** (mesmas configurações)

### Testes Funcionais

1. **Endpoints Payment (Customer):**
   - [ ] POST `/api/payment/create` sem token → 401
   - [ ] POST `/api/payment/create` com token Cognito → 403
   - [ ] POST `/api/payment/create` com token Customer válido → 201
   - [ ] POST `/api/payment/generate-qrcode` sem token → 401
   - [ ] POST `/api/payment/generate-qrcode` com token Customer válido → 200
   - [ ] GET `/api/payment/receipt-from-gateway` sem token → 401
   - [ ] GET `/api/payment/receipt-from-gateway` com token Customer válido → 200

2. **Endpoints Webhook (Anônimo):**
   - [ ] POST `/api/webhookpayment/payment-notification` sem token → 200 (deve funcionar)
   - [ ] POST `/api/webhookpayment/payment-notification` com token → 200 (deve funcionar também)

3. **Compatibilidade com OrderHub:**
   - [ ] Token gerado pelo Lambda Customer funciona em ambos os projetos
   - [ ] Token Cognito gerado pelo Lambda Admin funciona em ambos os projetos
   - [ ] Configurações são idênticas entre os projetos

## Subtasks

### Fase 1: Criar Estrutura de Autenticação
- [Subtask 01: Criar projeto Infra.Auth e copiar classes de configuração](./subtask/Subtask-01-Criar_Classes_Configuracao.md)
- [Subtask 02: Implementar extensão AddCustomerJwtBearer](./subtask/Subtask-02-Implementar_AddCustomerJwtBearer.md)
- [Subtask 03: Implementar extensão AddCognitoJwtBearer](./subtask/Subtask-03-Implementar_AddCognitoJwtBearer.md)
- [Subtask 04: Configurar políticas de autorização](./subtask/Subtask-04-Configurar_Politicas_Autorizacao.md)

### Fase 2: Configurar Program.cs e Swagger
- [Subtask 05: Configurar autenticação no Program.cs](./subtask/Subtask-05-Configurar_Autenticacao_Program.md)
- [Subtask 06: Configurar Swagger com múltiplos esquemas](./subtask/Subtask-06-Configurar_Swagger.md)
- [Subtask 07: Implementar AuthorizeBySchemeOperationFilter](./subtask/Subtask-07-Implementar_OperationFilter.md)

### Fase 3: Aplicar Autorização nos Controllers
- [Subtask 08: Aplicar [Authorize] no PaymentController](./subtask/Subtask-08-Aplicar_Authorize_PaymentController.md)
- [Subtask 09: Validar [AllowAnonymous] no WebhookPaymentController](./subtask/Subtask-09-Validar_Webhook_AllowAnonymous.md)

### Fase 4: Testes e Validação
- [Subtask 10: Testar autenticação CustomerBearer](./subtask/Subtask-10-Testar_CustomerBearer.md)
- [Subtask 11: Testar autenticação Cognito](./subtask/Subtask-11-Testar_Cognito.md)
- [Subtask 12: Validar compatibilidade com orderhub](./subtask/Subtask-12-Validar_Compatibilidade_OrderHub.md)

## Parâmetros de Configuração Necessários

### JWT Settings (CustomerBearer)
| Parâmetro | Fonte | Descrição | Exemplo | Observação |
|-----------|-------|-----------|---------|------------|
| `JwtCustomer:Issuer` | appsettings.json ou `JWTCUSTOMER__ISSUER` | Emissor do token JWT | "FastFood.Auth" | Mesmo do orderhub |
| `JwtCustomer:Audience` | appsettings.json ou `JWTCUSTOMER__AUDIENCE` | Audiência do token JWT | "FastFood.API" | Mesmo do orderhub |
| `JwtCustomer:SecretKey` | appsettings.json ou `JWTCUSTOMER__SECRETKEY` | Chave secreta para assinatura | "sua-chave-secreta-aqui" | **DEVE SER A MESMA DO ORDERHUB E DO LAMBDA CUSTOMER** |
| `JwtCustomer:ExpiresInMinutes` | appsettings.json ou `JWTCUSTOMER__EXPIRESINMINUTES` | Tempo de expiração em minutos | "1440" | Mesmo do orderhub |

### Cognito Settings
| Parâmetro | Fonte | Descrição | Exemplo | Observação |
|-----------|-------|-----------|---------|------------|
| `Authentication:Cognito:Region` | appsettings.json ou `AUTHENTICATION__COGNITO__REGION` | Região do AWS Cognito | "us-east-1" | **MESMA DO ORDERHUB** |
| `Authentication:Cognito:UserPoolId` | appsettings.json ou `AUTHENTICATION__COGNITO__USERPOOLID` | ID do User Pool do Cognito | "us-east-1_XXXXXXXXX" | **MESMO DO ORDERHUB** |
| `Authentication:Cognito:ClientId` | appsettings.json ou `AUTHENTICATION__COGNITO__CLIENTID` | Client ID do Cognito | "xxxxxxxxxxxxxxxxxxxxx" | **MESMO DO ORDERHUB** |
| `Authentication:Cognito:ClockSkewMinutes` | appsettings.json ou `AUTHENTICATION__COGNITO__CLOCKSKEWMINUTES` | Tolerância de relógio em minutos (opcional) | "5" | Mesmo do orderhub |

## Critérios de Aceite

### Funcionais
- [ ] Todos os endpoints de PaymentController requerem autenticação CustomerBearer com política Customer
- [ ] Endpoint de WebhookPaymentController permanece público (AllowAnonymous)
- [ ] Tokens gerados pelo Lambda Customer são aceitos nos endpoints CustomerBearer
- [ ] Tokens gerados pelo Lambda Admin (Cognito) são aceitos (mesmo que não usemos por enquanto)
- [ ] Tokens válidos do orderhub funcionam no paystream (mesma chave e configurações)
- [ ] Tokens expirados retornam 401 Unauthorized
- [ ] Tokens inválidos retornam 401 Unauthorized
- [ ] Tentativa de acesso com token incorreto retorna 403 Forbidden

### Técnicos
- [ ] Configurações suportam appsettings.json e variáveis de ambiente
- [ ] Swagger exibe corretamente os esquemas de autenticação
- [ ] Swagger permite testar endpoints com ambos os esquemas
- [ ] Código segue padrão arquitetural do projeto
- [ ] Sem vazamento de informações sensíveis em logs de erro
- [ ] Estrutura de autenticação é idêntica ao orderhub
- [ ] Configurações são compatíveis com o orderhub (mesmas chaves)

### Qualidade
- [ ] Código compila sem erros
- [ ] Testes funcionais passam
- [ ] Documentação atualizada (README, se necessário)
- [ ] Sem code smells críticos

## Observações Importantes

1. **Chave Secreta JWT:**
   - A chave secreta (`JwtCustomer:SecretKey`) **DEVE** ser a mesma usada no Lambda Customer e no orderhub
   - Se as chaves forem diferentes, os tokens não serão validados
   - Recomenda-se usar variáveis de ambiente ou secrets do Kubernetes para produção
   - **CRÍTICO:** Ambos os projetos (orderhub e paystream) devem usar a mesma chave

2. **Configuração Cognito:**
   - O `UserPoolId` e `ClientId` devem corresponder exatamente aos usados no Lambda Admin e no orderhub
   - A região deve estar correta para que o Authority seja construído corretamente
   - **CRÍTICO:** Todas as configurações Cognito devem ser idênticas ao orderhub

3. **Webhook:**
   - O endpoint de webhook deve permanecer público (`[AllowAnonymous]`)
   - Não deve exigir autenticação para permitir chamadas do gateway de pagamento
   - **JÁ ESTÁ CONFIGURADO, APENAS VALIDAR**

4. **Swagger:**
   - O Swagger deve permitir selecionar qual esquema usar para cada endpoint
   - O filtro `AuthorizeBySchemeOperationFilter` deve detectar automaticamente qual esquema usar baseado no `[Authorize]`
   - Endpoints com `[AllowAnonymous]` não devem mostrar botão de autenticação

5. **Compatibilidade com OrderHub:**
   - Tokens gerados pelo Lambda Customer devem funcionar em ambos os projetos
   - Tokens Cognito gerados pelo Lambda Admin devem funcionar em ambos os projetos
   - Configurações devem ser idênticas para garantir compatibilidade
   - Ambos os projetos estarão no mesmo cluster Kubernetes

6. **Inteligência Cognito:**
   - Mesmo que não usemos autenticação Cognito por enquanto, a infraestrutura deve estar configurada
   - Isso permite que no futuro possamos usar tokens Cognito sem precisar refatorar
   - A validação de tokens Cognito deve estar funcionando corretamente

## Referências

- **Projeto OrderHub (Referência):** `C:\Projetos\Fiap\fiap-fase4-orderhub-api`
- **Lambdas de Autenticação:** `C:\Projetos\Fiap\fiap-fase4-auth-lambda\src\InterfacesExternas`
- **Story de Autenticação do OrderHub:** `C:\Projetos\Fiap\fiap-fase4-orderhub-api\story\Storie-04-Implementar_Autenticacao_Autorizacao\story.md`
- **Documentação Microsoft:** [ASP.NET Core Authentication](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/)
- **Documentação AWS Cognito:** [AWS Cognito User Pools](https://docs.aws.amazon.com/cognito/latest/developerguide/cognito-user-identity-pools.html)
- **JWT.io:** [JWT Debugger](https://jwt.io/) - Para validar estrutura de tokens
