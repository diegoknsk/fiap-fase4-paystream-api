# Subtask 10: Testar Autenticação CustomerBearer

## Status
- **Estado:** 🔄 Pendente
- **Data de Início:** -
- **Data de Conclusão:** -

## Descrição
Testar que a autenticação CustomerBearer funciona corretamente, validando tokens JWT gerados pelo Lambda Customer.

## Objetivo
Validar que os endpoints do PaymentController aceitam tokens CustomerBearer válidos e rejeitam tokens inválidos ou ausentes.

## Testes a Realizar

### 1. Teste sem Token
- [ ] Chamar `POST /api/payment/create` sem token → Deve retornar **401 Unauthorized**
- [ ] Chamar `POST /api/payment/generate-qrcode` sem token → Deve retornar **401 Unauthorized**
- [ ] Chamar `GET /api/payment/receipt-from-gateway` sem token → Deve retornar **401 Unauthorized**

### 2. Teste com Token Customer Válido
- [ ] Obter token do Lambda Customer (via `/api/customer/anonymous`, `/api/customer/register` ou `/api/customer/identify`)
- [ ] Chamar `POST /api/payment/create` com token válido → Deve retornar **201 Created** ou **400 BadRequest** (dependendo dos dados)
- [ ] Chamar `POST /api/payment/generate-qrcode` com token válido → Deve retornar **200 OK** ou **404 NotFound** (dependendo se o pagamento existe)
- [ ] Chamar `GET /api/payment/receipt-from-gateway` com token válido → Deve retornar **200 OK** ou **404 NotFound**

### 3. Teste com Token Cognito (deve falhar)
- [ ] Obter token Cognito do Lambda Admin
- [ ] Chamar `POST /api/payment/create` com token Cognito → Deve retornar **403 Forbidden**
- [ ] Chamar `POST /api/payment/generate-qrcode` com token Cognito → Deve retornar **403 Forbidden**
- [ ] Chamar `GET /api/payment/receipt-from-gateway` com token Cognito → Deve retornar **403 Forbidden**

### 4. Teste com Token Expirado
- [ ] Criar token expirado (ou esperar expirar)
- [ ] Chamar endpoint com token expirado → Deve retornar **401 Unauthorized**

### 5. Teste com Token Inválido
- [ ] Chamar endpoint com token malformado → Deve retornar **401 Unauthorized**
- [ ] Chamar endpoint com token com assinatura inválida → Deve retornar **401 Unauthorized**
- [ ] Chamar endpoint com token com issuer/audience incorretos → Deve retornar **401 Unauthorized**

## Como Testar

### Via Swagger
1. Executar `dotnet run` e acessar Swagger UI
2. Clicar em "Authorize"
3. Selecionar "CustomerBearer"
4. Inserir token no formato: `Bearer {token}`
5. Testar endpoints

### Via Postman/curl
```bash
# Sem token (deve retornar 401)
curl -X POST https://localhost:5001/api/payment/create \
  -H "Content-Type: application/json" \
  -d '{"orderId": "...", ...}'

# Com token válido
curl -X POST https://localhost:5001/api/payment/create \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer {token}" \
  -d '{"orderId": "...", ...}'
```

## Critérios de Aceite

- [ ] Endpoints retornam 401 quando token está ausente
- [ ] Endpoints retornam 201/200 quando token Customer válido é fornecido
- [ ] Endpoints retornam 403 quando token Cognito é fornecido
- [ ] Endpoints retornam 401 quando token está expirado
- [ ] Endpoints retornam 401 quando token é inválido
- [ ] Token do Lambda Customer funciona corretamente
- [ ] Token do orderhub funciona no paystream (mesma chave)

## Observações

- **Token do Lambda Customer:** Deve ser obtido do projeto `fiap-fase4-auth-lambda`
- **Token do OrderHub:** Se ambos os projetos usam a mesma chave, tokens do orderhub devem funcionar no paystream
- **Claims esperadas:** `sub`, `customerId`, `jti`, `iat`
