# Subtask 11: Testar Autenticação Cognito

## Status
- **Estado:** 🔄 Pendente
- **Data de Início:** -
- **Data de Conclusão:** -

## Descrição
Testar que a autenticação Cognito funciona corretamente, validando tokens Access Token gerados pelo Lambda Admin. Mesmo que não usemos Cognito por enquanto, a infraestrutura deve estar funcionando.

## Objetivo
Validar que a configuração Cognito está correta e que tokens Cognito são validados adequadamente, mesmo que não sejam usados nos endpoints atualmente.

## Testes a Realizar

### 1. Teste com Token Cognito Válido
- [ ] Obter Access Token do Lambda Admin (via `/api/admin/login`)
- [ ] Verificar que o token tem `token_use == "access"`
- [ ] Verificar que o token tem `client_id` correspondente ao configurado
- [ ] Verificar que o token tem claim `scope` com `aws.cognito.signin.user.admin`

### 2. Teste de Validação de Token
- [ ] Token Cognito válido deve passar na validação básica
- [ ] Token Cognito com `token_use != "access"` deve falhar
- [ ] Token Cognito com `client_id` incorreto deve falhar
- [ ] Token Cognito expirado deve falhar

### 3. Teste de Compatibilidade com OrderHub
- [ ] Token Cognito gerado para orderhub deve funcionar no paystream (mesmas configurações)
- [ ] Token Cognito gerado para paystream deve funcionar no orderhub (mesmas configurações)

## Como Testar

### Via Swagger
1. Executar `dotnet run` e acessar Swagger UI
2. Clicar em "Authorize"
3. Selecionar "Cognito"
4. Inserir Access Token no formato: `Bearer {token}`
5. Testar endpoints (mesmo que retornem 403, a validação do token deve funcionar)

### Via Postman/curl
```bash
# Com token Cognito válido
curl -X POST https://localhost:5001/api/payment/create \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer {cognito-access-token}" \
  -d '{"orderId": "...", ...}'
```

## Critérios de Aceite

- [ ] Token Cognito válido passa na validação básica
- [ ] Token Cognito com `token_use != "access"` é rejeitado
- [ ] Token Cognito com `client_id` incorreto é rejeitado
- [ ] Token Cognito expirado é rejeitado
- [ ] Token Cognito do orderhub funciona no paystream (mesmas configurações)
- [ ] Validações no `OnTokenValidated` funcionam corretamente
- [ ] Logs de validação aparecem no console (se configurado)

## Observações

- **Não usamos Cognito ainda:** Mesmo que não usemos Cognito nos endpoints, a infraestrutura deve estar funcionando
- **Mesmas configurações:** UserPoolId, ClientId e Region devem ser idênticos ao orderhub
- **Access Token:** Deve usar Access Token, não IdToken
- **Validações:** O `OnTokenValidated` valida `token_use` e `client_id`

## Referências

- **Lambda Admin:** `C:\Projetos\Fiap\fiap-fase4-auth-lambda\src\InterfacesExternas\FastFood.Auth.Lambda.Admin`
