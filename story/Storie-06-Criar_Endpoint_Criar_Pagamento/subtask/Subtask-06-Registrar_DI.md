# Subtask 06: Registrar UseCase e Presenter no DI

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Registrar o UseCase CreatePaymentUseCase e o Presenter CreatePaymentPresenter no container de injeção de dependência no Program.cs da API.

## Passos de implementação
- [ ] Abrir arquivo `src/InterfacesExternas/FastFood.PayStream.Api/Program.cs`
- [ ] Verificar se `IPaymentRepository` já está registrado (deve ter sido feito na story 04)
- [ ] Adicionar registro do Presenter:
  - `builder.Services.AddScoped<CreatePaymentPresenter>();`
- [ ] Adicionar registro do UseCase:
  - `builder.Services.AddScoped<CreatePaymentUseCase>();`
- [ ] Adicionar usings necessários:
  - `FastFood.PayStream.Application.Presenters`
  - `FastFood.PayStream.Application.UseCases`
- [ ] Verificar que a ordem de registro está correta (dependências antes dos dependentes)

## Como testar
- Executar `dotnet build` no projeto Api (deve compilar sem erros)
- Verificar que não há erros de DI ao iniciar a aplicação
- Validar que o UseCase e Presenter podem ser injetados em outros serviços

## Critérios de aceite
- [ ] `CreatePaymentPresenter` registrado como Scoped no `Program.cs`
- [ ] `CreatePaymentUseCase` registrado como Scoped no `Program.cs`
- [ ] Usings corretos adicionados
- [ ] `IPaymentRepository` já está registrado (verificado)
- [ ] Projeto Api compila sem erros
- [ ] Não há erros de DI ao iniciar aplicação
