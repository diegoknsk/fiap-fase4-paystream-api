# Subtask 05: Registrar controllers e validar compilação

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Garantir que os controllers estão sendo descobertos corretamente pelo ASP.NET Core e que a API compila e executa sem erros, validando toda a estrutura criada.

## Passos de implementação
- [ ] Verificar que `Program.cs` tem `builder.Services.AddControllers()` configurado
- [ ] Verificar que `Program.cs` tem `app.MapControllers()` no pipeline
- [ ] Executar `dotnet build` na solução completa
- [ ] Verificar que não há erros de compilação
- [ ] Executar `dotnet run` no projeto Api
- [ ] Verificar que a API inicia sem erros
- [ ] Acessar `/swagger` e verificar que ambos controllers aparecem
- [ ] Validar que a estrutura está pronta para receber UseCases nas próximas stories
- [ ] Documentar qualquer configuração adicional necessária

## Como testar
- Executar `dotnet build` na raiz da solução (deve compilar sem erros)
- Executar `dotnet run` no projeto Api (deve iniciar sem erros)
- Acessar `https://localhost:XXXX/swagger` (porta configurada)
- Verificar que `PaymentController` aparece no Swagger
- Verificar que `WebhookPaymentController` aparece no Swagger
- Validar que não há erros no console da aplicação
- Testar que a API responde a requisições básicas (health check, etc.)

## Critérios de aceite
- [ ] `AddControllers()` configurado no `Program.cs`
- [ ] `MapControllers()` configurado no pipeline
- [ ] Solução compila sem erros (`dotnet build`)
- [ ] API inicia sem erros (`dotnet run`)
- [ ] Swagger acessível e funcionando
- [ ] `PaymentController` visível no Swagger
- [ ] `WebhookPaymentController` visível no Swagger
- [ ] Estrutura pronta para receber UseCases nas próximas stories
- [ ] Nenhum erro de runtime ou compilação
