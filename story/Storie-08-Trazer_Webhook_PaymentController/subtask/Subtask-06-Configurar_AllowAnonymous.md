# Subtask 06: Configurar endpoint como AllowAnonymous

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Garantir que o endpoint de webhook está configurado corretamente como AllowAnonymous e pode ser acessado sem autenticação, permitindo que gateways externos chamem o webhook.

## Passos de implementação
- [ ] Verificar que o atributo `[AllowAnonymous]` foi adicionado no método PaymentNotification (já feito na subtask 04)
- [ ] Verificar que não há políticas de autorização globais que possam bloquear o endpoint
- [ ] Testar que o endpoint pode ser chamado sem token de autenticação
- [ ] Documentar no comentário XML do endpoint que é um webhook público
- [ ] Considerar adicionar validação de origem (opcional, para produção):
  - Verificar header X-Requested-With ou similar
  - Ou validar IP de origem (se necessário)
  - Nota: Por enquanto, manter simples para desenvolvimento

## Como testar
- Executar `dotnet run` no projeto Api
- Chamar endpoint via Swagger sem autenticação (deve funcionar)
- Chamar endpoint via Postman/curl sem headers de autenticação (deve funcionar)
- Verificar logs para confirmar que não há erros de autorização

## Critérios de aceite
- [ ] Atributo `[AllowAnonymous]` aplicado no endpoint PaymentNotification
- [ ] Endpoint pode ser chamado sem autenticação via Swagger
- [ ] Endpoint pode ser chamado sem autenticação via Postman/curl
- [ ] Não há erros de autorização nos logs
- [ ] Comentário XML documenta que é um webhook público
- [ ] Endpoint funciona corretamente para chamadas externas
