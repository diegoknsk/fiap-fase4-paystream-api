# Subtask 02: Criar OutputModel CreatePaymentOutputModel

## Status
- **Estado:** ✅ Concluída
- **Data de Conclusão:** 02/01/2026

## Descrição
Criar o OutputModel que será usado pelo UseCase para retornar os dados do pagamento criado, antes de ser transformado em Response pelo Presenter.

## Passos de implementação
- [ ] Criar diretório `src/Core/FastFood.PayStream.Application/OutputModels/` se não existir
- [ ] Criar arquivo `CreatePaymentOutputModel.cs` no diretório OutputModels
- [ ] Definir namespace `FastFood.PayStream.Application.OutputModels`
- [ ] Criar classe pública `CreatePaymentOutputModel` com as seguintes propriedades públicas:
  - `PaymentId` (Guid) - ID do pagamento criado
  - `OrderId` (Guid) - ID do pedido relacionado
  - `Status` (int) - Status do pagamento (representa EnumPaymentStatus)
  - `TotalAmount` (decimal) - Valor total do pedido
  - `CreatedAt` (DateTime) - Data de criação do pagamento
- [ ] Adicionar comentários XML para documentação da classe e propriedades

## Como testar
- Executar `dotnet build` no projeto Application (deve compilar sem erros)
- Verificar que a classe pode ser instanciada
- Validar que todas as propriedades estão acessíveis
- Testar criação de instância com valores válidos

## Critérios de aceite
- [ ] Arquivo `CreatePaymentOutputModel.cs` criado em `src/Core/FastFood.PayStream.Application/OutputModels/`
- [ ] Classe `CreatePaymentOutputModel` criada com namespace `FastFood.PayStream.Application.OutputModels`
- [ ] Propriedade `PaymentId` (Guid) definida
- [ ] Propriedade `OrderId` (Guid) definida
- [ ] Propriedade `Status` (int) definida
- [ ] Propriedade `TotalAmount` (decimal) definida
- [ ] Propriedade `CreatedAt` (DateTime) definida
- [ ] Comentários XML adicionados para documentação
- [ ] Projeto Application compila sem erros
