# Subtask 02: Criar modelos para GenerateQrCode (Input, Output, Response)

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Criar os modelos InputModel, OutputModel e Response para o endpoint GenerateQrCode, seguindo o padrão estabelecido.

## Passos de implementação
- [ ] Criar arquivo `GenerateQrCodeInputModel.cs` em `src/Core/FastFood.PayStream.Application/InputModels/`
- [ ] Definir classe pública `GenerateQrCodeInputModel` com:
  - `OrderId` (Guid) - ID do pedido
  - `FakeCheckout` (bool) - Indica se deve usar gateway fake (default: false)
- [ ] Criar arquivo `GenerateQrCodeOutputModel.cs` em `src/Core/FastFood.PayStream.Application/OutputModels/`
- [ ] Definir classe pública `GenerateQrCodeOutputModel` com:
  - `QrCodeUrl` (string) - URL do QR Code gerado
  - `PaymentId` (Guid) - ID do pagamento
  - `OrderId` (Guid) - ID do pedido
- [ ] Criar arquivo `GenerateQrCodeResponse.cs` em `src/Core/FastFood.PayStream.Application/Responses/`
- [ ] Definir classe pública `GenerateQrCodeResponse` com mesma estrutura do OutputModel
- [ ] Adicionar comentários XML para documentação de todas as classes

## Como testar
- Executar `dotnet build` no projeto Application (deve compilar sem erros)
- Verificar que todas as classes podem ser instanciadas
- Validar que as estruturas estão corretas

## Critérios de aceite
- [ ] `GenerateQrCodeInputModel` criado com OrderId e FakeCheckout
- [ ] `GenerateQrCodeOutputModel` criado com QrCodeUrl, PaymentId e OrderId
- [ ] `GenerateQrCodeResponse` criado com mesma estrutura do OutputModel
- [ ] Comentários XML adicionados
- [ ] Projeto Application compila sem erros
