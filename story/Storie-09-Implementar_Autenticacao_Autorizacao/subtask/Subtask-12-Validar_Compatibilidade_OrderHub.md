# Subtask 12: Validar Compatibilidade com OrderHub

## Status
- **Estado:** 🔄 Pendente
- **Data de Início:** -
- **Data de Conclusão:** -

## Descrição
Validar que as configurações de autenticação são compatíveis com o orderhub, garantindo que tokens gerados para um projeto funcionem no outro.

## Objetivo
Garantir que ambos os projetos (orderhub e paystream) podem usar os mesmos tokens, pois estarão no mesmo cluster e terão as mesmas chaves.

## Validações a Realizar

### 1. Configurações JWT Customer
- [ ] `JwtCustomer:Issuer` é idêntico ao orderhub
- [ ] `JwtCustomer:Audience` é idêntico ao orderhub
- [ ] `JwtCustomer:SecretKey` é idêntico ao orderhub
- [ ] `JwtCustomer:ExpiresInMinutes` é idêntico ao orderhub

### 2. Configurações Cognito
- [ ] `Authentication:Cognito:Region` é idêntico ao orderhub
- [ ] `Authentication:Cognito:UserPoolId` é idêntico ao orderhub
- [ ] `Authentication:Cognito:ClientId` é idêntico ao orderhub
- [ ] `Authentication:Cognito:ClockSkewMinutes` é idêntico ao orderhub

### 3. Teste de Token Customer
- [ ] Token gerado pelo Lambda Customer funciona no orderhub
- [ ] Token gerado pelo Lambda Customer funciona no paystream
- [ ] Token usado no orderhub funciona no paystream (mesma chave)

### 4. Teste de Token Cognito
- [ ] Token Cognito gerado pelo Lambda Admin funciona no orderhub
- [ ] Token Cognito gerado pelo Lambda Admin funciona no paystream
- [ ] Token Cognito usado no orderhub funciona no paystream (mesmas configurações)

### 5. Estrutura de Código
- [ ] Estrutura de classes de autenticação é idêntica ao orderhub
- [ ] Nomes de esquemas são idênticos ("CustomerBearer", "Cognito")
- [ ] Nomes de políticas são idênticos ("Admin", "Customer")

## Como Validar

### Comparação de Configurações
1. Comparar `appsettings.json` de ambos os projetos
2. Comparar variáveis de ambiente (se usadas)
3. Verificar que valores são idênticos

### Teste Prático
1. Gerar token Customer no Lambda
2. Usar token no orderhub → deve funcionar
3. Usar mesmo token no paystream → deve funcionar
4. Gerar token Cognito no Lambda Admin
5. Usar token no orderhub → deve funcionar
6. Usar mesmo token no paystream → deve funcionar

## Critérios de Aceite

- [ ] Todas as configurações JWT são idênticas ao orderhub
- [ ] Todas as configurações Cognito são idênticas ao orderhub
- [ ] Token Customer funciona em ambos os projetos
- [ ] Token Cognito funciona em ambos os projetos
- [ ] Estrutura de código é idêntica ao orderhub
- [ ] Nomes de esquemas e políticas são idênticos

## Observações Importantes

- **CRÍTICO:** A chave secreta JWT (`JwtCustomer:SecretKey`) DEVE ser a mesma em ambos os projetos
- **CRÍTICO:** As configurações Cognito DEEM ser idênticas em ambos os projetos
- **Mesmo Cluster:** Ambos os projetos estarão no mesmo cluster Kubernetes, então devem usar os mesmos secrets
- **Mesmas Chaves:** Se as chaves forem diferentes, os tokens não funcionarão entre projetos

## Checklist de Compatibilidade

### JWT Customer
- [ ] Issuer: `FastFood.Auth` (mesmo)
- [ ] Audience: `FastFood.API` (mesmo)
- [ ] SecretKey: **MESMA CHAVE** (crítico)
- [ ] ExpiresInMinutes: 1440 (mesmo)

### Cognito
- [ ] Region: `us-east-1` (mesmo)
- [ ] UserPoolId: **MESMO ID** (crítico)
- [ ] ClientId: **MESMO ID** (crítico)
- [ ] ClockSkewMinutes: 5 (mesmo)

### Código
- [ ] Scheme "CustomerBearer" (mesmo nome)
- [ ] Scheme "Cognito" (mesmo nome)
- [ ] Policy "Admin" (mesmo nome)
- [ ] Policy "Customer" (mesmo nome)
