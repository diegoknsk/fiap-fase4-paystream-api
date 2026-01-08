# Subtask 06: Registrar serviços no DI

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Registrar o KitchenService e configurar HttpClient no container de injeção de dependência no Program.cs, garantindo que o serviço esteja disponível para o GetReceiptUseCase.

## Passos de implementação
- [ ] Abrir arquivo `src/InterfacesExternas/FastFood.PayStream.Api/Program.cs`
- [ ] Adicionar registro do HttpClient para KitchenService:
  - `builder.Services.AddHttpClient<KitchenService>()` ou
  - `builder.Services.AddHttpClient("KitchenService")` e configurar base address
- [ ] Registrar `IKitchenService` mapeado para `KitchenService`:
  - `builder.Services.AddScoped<IKitchenService, KitchenService>()`
- [ ] Se necessário, configurar base address do HttpClient usando configurações:
  - Ler `KitchenApi:BaseUrl` da configuração
  - Configurar `BaseAddress` do HttpClient
- [ ] Atualizar registro do `GetReceiptUseCase` para incluir `IKitchenService`:
  - O construtor do GetReceiptUseCase já deve receber IKitchenService
  - Verificar que o DI resolve corretamente todas as dependências
- [ ] Adicionar using necessário: `using FastFood.PayStream.Infra.Services;`
- [ ] Adicionar using necessário: `using FastFood.PayStream.Application.Ports;`

## Como testar
- Executar `dotnet build` no projeto Api (deve compilar sem erros)
- Executar `dotnet run` e verificar que a aplicação inicia sem erros
- Verificar que o GetReceiptUseCase pode ser resolvido pelo DI
- Testar endpoint via Swagger e verificar que a chamada para cozinha é feita

## Critérios de aceite
- [ ] HttpClient registrado para KitchenService
- [ ] `IKitchenService` registrado mapeado para `KitchenService`
- [ ] `GetReceiptUseCase` pode ser resolvido pelo DI (todas as dependências resolvidas)
- [ ] Base address configurado se necessário
- [ ] Usings necessários adicionados
- [ ] Projeto Api compila sem erros
- [ ] Aplicação inicia sem erros
- [ ] Endpoint funciona corretamente via Swagger
