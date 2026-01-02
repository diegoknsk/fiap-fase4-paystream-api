# Storie-07: Trazer Endpoints GenerateQrCode e GetReceiptFromGateway

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Como desenvolvedor, quero trazer os endpoints GenerateQrCode e GetReceiptFromGateway do monolito para o microserviço de pagamento, adaptando para usar OrderSnapshot (JSON) ao invés de buscar dados do pedido, e suportando tanto o fluxo padrão (Mercado Pago) quanto o fake para desenvolvimento/testes.

## Objetivo
Criar os UseCases, InputModels, OutputModels, Responses, Presenters e endpoints para GenerateQrCode e GetReceiptFromGateway, adaptando do monolito para:
- Usar OrderSnapshot (JSON) do Payment ao invés de buscar pedido
- Deserializar OrderSnapshot para obter dados necessários (produtos, código do pedido, etc.)
- Suportar fluxo fake via parâmetro `fakeCheckout`
- Integrar com IPaymentGateway (Mercado Pago real e Fake)
- Atualizar Payment no repositório após gerar QR Code

## Escopo Técnico
- Tecnologias: .NET 8, ASP.NET Core, JSON deserialization, Clean Architecture
- Arquivos afetados:
  - `src/Core/FastFood.PayStream.Application/InputModels/GenerateQrCodeInputModel.cs`
  - `src/Core/FastFood.PayStream.Application/OutputModels/GenerateQrCodeOutputModel.cs`
  - `src/Core/FastFood.PayStream.Application/Responses/GenerateQrCodeResponse.cs`
  - `src/Core/FastFood.PayStream.Application/Presenters/GenerateQrCodePresenter.cs`
  - `src/Core/FastFood.PayStream.Application/UseCases/GenerateQrCodeUseCase.cs`
  - `src/Core/FastFood.PayStream.Application/InputModels/GetReceiptInputModel.cs`
  - `src/Core/FastFood.PayStream.Application/OutputModels/GetReceiptOutputModel.cs`
  - `src/Core/FastFood.PayStream.Application/Responses/GetReceiptResponse.cs`
  - `src/Core/FastFood.PayStream.Application/Presenters/GetReceiptPresenter.cs`
  - `src/Core/FastFood.PayStream.Application/UseCases/GetReceiptUseCase.cs`
  - `src/Core/FastFood.PayStream.Application/Ports/IPaymentGateway.cs` (interface do gateway)
  - `src/InterfacesExternas/FastFood.PayStream.Api/Controllers/PaymentController.cs`
  - `src/InterfacesExternas/FastFood.PayStream.Api/Program.cs` (registro de DI)
- Adaptações necessárias:
  - Deserializar OrderSnapshot (JSON) para obter lista de produtos e código do pedido
  - Criar QrCodeItemModel a partir dos dados do OrderSnapshot
  - Buscar Payment por OrderId (não mais buscar Order)
  - Atualizar Payment com QrCodeUrl e status após gerar QR Code

## Subtasks

- [ ] [Subtask 01: Criar interface IPaymentGateway na Application](./subtask/Subtask-01-Criar_interface_IPaymentGateway.md)
- [ ] [Subtask 02: Criar modelos para GenerateQrCode (Input, Output, Response)](./subtask/Subtask-02-Criar_modelos_GenerateQrCode.md)
- [ ] [Subtask 03: Criar Presenter e UseCase GenerateQrCodeUseCase](./subtask/Subtask-03-Criar_UseCase_GenerateQrCode.md)
- [ ] [Subtask 04: Criar modelos para GetReceipt (Input, Output, Response)](./subtask/Subtask-04-Criar_modelos_GetReceipt.md)
- [ ] [Subtask 05: Criar Presenter e UseCase GetReceiptUseCase](./subtask/Subtask-05-Criar_UseCase_GetReceipt.md)
- [ ] [Subtask 06: Criar endpoint GenerateQrCode no PaymentController](./subtask/Subtask-06-Criar_endpoint_GenerateQrCode.md)
- [ ] [Subtask 07: Criar endpoint GetReceiptFromGateway no PaymentController](./subtask/Subtask-07-Criar_endpoint_GetReceipt.md)
- [ ] [Subtask 08: Registrar UseCases e dependências no DI](./subtask/Subtask-08-Registrar_DI.md)

## Critérios de Aceite da História

- [ ] Interface `IPaymentGateway` criada com métodos: GenerateQrCodeAsync, GetReceiptFromGatewayAsync, CheckPaymentStatusAsync
- [ ] `GenerateQrCodeInputModel` criado com OrderId (Guid) e FakeCheckout (bool)
- [ ] `GenerateQrCodeOutputModel` criado com QrCodeUrl (string)
- [ ] `GenerateQrCodeResponse` criado
- [ ] `GenerateQrCodeUseCase` implementado:
  - Busca Payment por OrderId via repositório
  - Deserializa OrderSnapshot (JSON) para obter dados do pedido
  - Cria QrCodeItemModel a partir dos dados do OrderSnapshot
  - Chama IPaymentGateway.GenerateQrCodeAsync (real ou fake conforme parâmetro)
  - Atualiza Payment com QrCodeUrl e status QrCodeGenerated
  - Salva Payment atualizado via repositório
- [ ] `GetReceiptInputModel` criado com OrderId (Guid) e FakeCheckout (bool)
- [ ] `GetReceiptOutputModel` criado com dados do comprovante
- [ ] `GetReceiptResponse` criado
- [ ] `GetReceiptUseCase` implementado:
  - Busca Payment por OrderId via repositório
  - Valida que Payment tem ExternalTransactionId
  - Chama IPaymentGateway.GetReceiptFromGatewayAsync (real ou fake)
  - Retorna comprovante
- [ ] Endpoint POST `/api/payment/generate-qrcode` criado no PaymentController
- [ ] Endpoint GET `/api/payment/receipt-from-gateway` criado no PaymentController
- [ ] Ambos endpoints suportam parâmetro `fakeCheckout` (query string)
- [ ] UseCases e Presenters registrados no DI
- [ ] Projeto compila sem erros
- [ ] Endpoints funcionam corretamente via Swagger
