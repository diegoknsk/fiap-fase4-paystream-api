# Subtask 01: Criar interface IPaymentGateway na Application

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Criar a interface IPaymentGateway na camada Application que define o contrato para integração com gateways de pagamento (Mercado Pago real e Fake), seguindo o padrão do monolito.

## Passos de implementação
- [ ] Criar diretório `src/Core/FastFood.PayStream.Application/Ports/` se não existir
- [ ] Criar arquivo `IPaymentGateway.cs` no diretório Ports
- [ ] Definir namespace `FastFood.PayStream.Application.Ports`
- [ ] Criar interface pública `IPaymentGateway` com os seguintes métodos assíncronos:
  - `Task<string> GenerateQrCodeAsync(string externalReference, string orderCode, List<QrCodeItemModel> items)` - Gera QR Code
  - `Task<PaymentReceipt> GetReceiptFromGatewayAsync(string paymentId)` - Obtém comprovante
  - `Task<PaymentStatusResult> CheckPaymentStatusAsync(string externalReference)` - Verifica status do pagamento
- [ ] Criar classe `QrCodeItemModel` em `src/Core/FastFood.PayStream.Application/Ports/Parameters/` (ou similar):
  - Propriedades: Title (string), Description (string), UnitPrice (decimal), Quantity (int), UnitMeasure (string), TotalAmount (decimal, calculado)
- [ ] Criar classe `PaymentReceipt` em `src/Core/FastFood.PayStream.Application/Ports/Parameters/`:
  - Propriedades: PaymentId, ExternalReference, Status, StatusDetail, TotalPaidAmount, PaymentMethod, PaymentType, Currency, DateApproved
- [ ] Criar classe `PaymentStatusResult` em `src/Core/FastFood.PayStream.Application/Ports/Parameters/`:
  - Propriedades: IsApproved (bool), IsPending (bool), IsRejected (bool), IsCanceled (bool), TransactionId (string?)
- [ ] Adicionar comentários XML para documentação

## Como testar
- Executar `dotnet build` no projeto Application (deve compilar sem erros)
- Verificar que a interface está acessível
- Validar que todas as classes de parâmetros estão definidas

## Critérios de aceite
- [ ] Arquivo `IPaymentGateway.cs` criado em `src/Core/FastFood.PayStream.Application/Ports/`
- [ ] Interface `IPaymentGateway` criada com 3 métodos assíncronos
- [ ] Classe `QrCodeItemModel` criada com todas as propriedades
- [ ] Classe `PaymentReceipt` criada com todas as propriedades
- [ ] Classe `PaymentStatusResult` criada com todas as propriedades
- [ ] Comentários XML adicionados
- [ ] Projeto Application compila sem erros
