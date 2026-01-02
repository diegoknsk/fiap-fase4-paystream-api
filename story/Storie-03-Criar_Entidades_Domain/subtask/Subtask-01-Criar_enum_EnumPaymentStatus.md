# Subtask 01: Criar enum EnumPaymentStatus

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Criar o enum `EnumPaymentStatus` que representa todos os possíveis status de um pagamento no sistema, seguindo o padrão do monolito mas adaptado para o contexto do microserviço.

## Passos de implementação
- [ ] Criar diretório `src/Core/FastFood.PayStream.Domain/Common/Enums/` se não existir
- [ ] Criar arquivo `EnumPaymentStatus.cs` no diretório Common/Enums
- [ ] Definir namespace `FastFood.PayStream.Domain.Common.Enums`
- [ ] Criar enum público `EnumPaymentStatus` com os seguintes valores:
  - `NotStarted = 0` - Nenhuma tentativa de pagamento
  - `Started = 1` - Cliente iniciou o pagamento
  - `QrCodeGenerated = 2` - QR Code emitido
  - `Approved = 3` - Pagamento confirmado
  - `Rejected = 4` - Pagamento recusado
  - `Canceled = 5` - Cliente cancelou / timeout / erro
- [ ] Adicionar comentários XML para documentação de cada valor do enum

## Como testar
- Executar `dotnet build` no projeto Domain (deve compilar sem erros)
- Verificar que o enum está acessível no namespace correto
- Validar que todos os valores estão definidos corretamente (0 a 5)
- Verificar que o arquivo segue o padrão de nomenclatura do projeto

## Critérios de aceite
- [ ] Arquivo `EnumPaymentStatus.cs` criado em `src/Core/FastFood.PayStream.Domain/Common/Enums/`
- [ ] Enum definido com namespace `FastFood.PayStream.Domain.Common.Enums`
- [ ] Todos os 6 valores do enum definidos (NotStarted, Started, QrCodeGenerated, Approved, Rejected, Canceled)
- [ ] Valores numéricos corretos (0 a 5)
- [ ] Comentários XML adicionados para documentação
- [ ] Projeto Domain compila sem erros
- [ ] Enum pode ser importado e usado em outros projetos que referenciam Domain
