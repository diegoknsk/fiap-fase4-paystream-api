# Subtask 08: Registrar UseCases e dependências no DI

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Registrar todos os UseCases, Presenters e implementações de IPaymentGateway no container de injeção de dependência no Program.cs da API.

## Passos de implementação
- [ ] Abrir arquivo `src/InterfacesExternas/FastFood.PayStream.Api/Program.cs`
- [ ] Verificar que `IPaymentRepository` já está registrado (story 04)
- [ ] Adicionar registro dos Presenters:
  - `builder.Services.AddScoped<GenerateQrCodePresenter>();`
  - `builder.Services.AddScoped<GetReceiptPresenter>();`
- [ ] Adicionar registro dos UseCases:
  - `builder.Services.AddScoped<GenerateQrCodeUseCase>();`
  - `builder.Services.AddScoped<GetReceiptUseCase>();`
- [ ] Criar implementações de IPaymentGateway (será feito em story futura, mas preparar estrutura):
  - Registrar `PaymentFakeGateway` como implementação de `IPaymentGateway` com nome/chave "Fake"
  - Registrar `PaymentMercadoPagoGateway` como implementação de `IPaymentGateway` com nome/chave "Real"
  - Ou usar factory pattern para selecionar gateway baseado em parâmetro
- [ ] Adicionar usings necessários:
  - `FastFood.PayStream.Application.Presenters`
  - `FastFood.PayStream.Application.UseCases`
  - `FastFood.PayStream.Application.Ports`
- [ ] Nota: As implementações reais dos gateways serão criadas em story futura, mas a estrutura de DI deve estar preparada

## Como testar
- Executar `dotnet build` no projeto Api (deve compilar sem erros)
- Verificar que não há erros de DI ao iniciar a aplicação
- Validar que os UseCases e Presenters podem ser injetados

## Critérios de aceite
- [ ] `GenerateQrCodePresenter` registrado como Scoped
- [ ] `GetReceiptPresenter` registrado como Scoped
- [ ] `GenerateQrCodeUseCase` registrado como Scoped
- [ ] `GetReceiptUseCase` registrado como Scoped
- [ ] Estrutura preparada para registro dos gateways (nota: implementações serão criadas depois)
- [ ] Usings corretos adicionados
- [ ] Projeto Api compila sem erros
- [ ] Não há erros de DI ao iniciar aplicação (se gateways mockados temporariamente)
