# Subtask 02: Criar Presenter PaymentNotificationPresenter

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Criar o Presenter que transforma o OutputModel em Response para o webhook de notificação de pagamento.

## Passos de implementação
- [ ] Criar arquivo `PaymentNotificationPresenter.cs` em `src/Core/FastFood.PayStream.Application/Presenters/`
- [ ] Definir namespace `FastFood.PayStream.Application.Presenters`
- [ ] Adicionar usings:
  - `FastFood.PayStream.Application.OutputModels`
  - `FastFood.PayStream.Application.Responses`
- [ ] Criar classe pública `PaymentNotificationPresenter`
- [ ] Criar método público `Present(PaymentNotificationOutputModel output)` retornando `PaymentNotificationResponse`:
  - Mapear todas as propriedades do OutputModel para o Response
  - Retornar nova instância de PaymentNotificationResponse
- [ ] Adicionar comentários XML para documentação

## Como testar
- Executar `dotnet build` no projeto Application (deve compilar sem erros)
- Criar teste básico validando que o Presenter transforma corretamente
- Verificar que todas as propriedades são mapeadas

## Critérios de aceite
- [ ] Arquivo `PaymentNotificationPresenter.cs` criado em `src/Core/FastFood.PayStream.Application/Presenters/`
- [ ] Classe `PaymentNotificationPresenter` criada
- [ ] Método `Present(PaymentNotificationOutputModel output)` implementado
- [ ] Método retorna `PaymentNotificationResponse`
- [ ] Todas as propriedades são mapeadas corretamente
- [ ] Comentários XML adicionados
- [ ] Projeto Application compila sem erros
