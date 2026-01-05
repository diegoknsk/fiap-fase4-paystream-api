# Subtask 04: Criar modelos para GetReceipt (Input, Output, Response)

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Criar os modelos InputModel, OutputModel e Response para o endpoint GetReceiptFromGateway, seguindo o padrão estabelecido.

## Passos de implementação
- [ ] Criar arquivo `GetReceiptInputModel.cs` em `src/Core/FastFood.PayStream.Application/InputModels/`
- [ ] Definir classe pública `GetReceiptInputModel` com:
  - `OrderId` (Guid) - ID do pedido
  - `FakeCheckout` (bool) - Indica se deve usar gateway fake (default: false)
- [ ] Criar arquivo `GetReceiptOutputModel.cs` em `src/Core/FastFood.PayStream.Application/OutputModels/`
- [ ] Definir classe pública `GetReceiptOutputModel` com propriedades do PaymentReceipt:
  - `PaymentId` (string)
  - `ExternalReference` (string)
  - `Status` (string)
  - `StatusDetail` (string)
  - `TotalPaidAmount` (decimal)
  - `PaymentMethod` (string)
  - `PaymentType` (string)
  - `Currency` (string)
  - `DateApproved` (DateTime)
- [ ] Criar arquivo `GetReceiptResponse.cs` em `src/Core/FastFood.PayStream.Application/Responses/`
- [ ] Definir classe pública `GetReceiptResponse` com mesma estrutura do OutputModel
- [ ] Adicionar comentários XML para documentação

## Como testar
- Executar `dotnet build` no projeto Application (deve compilar sem erros)
- Verificar que todas as classes podem ser instanciadas
- Validar que as estruturas estão corretas

## Critérios de aceite
- [ ] `GetReceiptInputModel` criado com OrderId e FakeCheckout
- [ ] `GetReceiptOutputModel` criado com todas as propriedades do PaymentReceipt
- [ ] `GetReceiptResponse` criado com mesma estrutura do OutputModel
- [ ] Comentários XML adicionados
- [ ] Projeto Application compila sem erros
