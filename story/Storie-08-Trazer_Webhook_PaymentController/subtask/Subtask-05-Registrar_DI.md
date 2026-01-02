# Subtask 05: Registrar UseCase e Presenter no DI

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Registrar o UseCase PaymentNotificationUseCase e o Presenter PaymentNotificationPresenter no container de injeção de dependência no Program.cs da API.

## Passos de implementação
- [ ] Abrir arquivo `src/InterfacesExternas/FastFood.PayStream.Api/Program.cs`
- [ ] Verificar que `IPaymentRepository` já está registrado (story 04)
- [ ] Verificar que os gateways já estão registrados (ou preparar estrutura - story 07)
- [ ] Adicionar registro do Presenter:
  - `builder.Services.AddScoped<PaymentNotificationPresenter>();`
- [ ] Adicionar registro do UseCase:
  - `builder.Services.AddScoped<PaymentNotificationUseCase>();`
- [ ] Adicionar usings necessários:
  - `FastFood.PayStream.Application.Presenters`
  - `FastFood.PayStream.Application.UseCases`
- [ ] Verificar que a ordem de registro está correta (dependências antes dos dependentes)

## Como testar
- Executar `dotnet build` no projeto Api (deve compilar sem erros)
- Verificar que não há erros de DI ao iniciar a aplicação
- Validar que o UseCase e Presenter podem ser injetados

## Critérios de aceite
- [ ] `PaymentNotificationPresenter` registrado como Scoped no `Program.cs`
- [ ] `PaymentNotificationUseCase` registrado como Scoped no `Program.cs`
- [ ] Usings corretos adicionados
- [ ] `IPaymentRepository` já está registrado (verificado)
- [ ] Gateways já estão registrados ou estrutura preparada (verificado)
- [ ] Projeto Api compila sem erros
- [ ] Não há erros de DI ao iniciar aplicação
