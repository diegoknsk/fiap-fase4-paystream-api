# Storie-06: Criar Endpoint para Criar Novo Pagamento

## Status
- **Estado:** ✅ Concluída
- **Data de Conclusão:** 02/01/2026

## Descrição
Como desenvolvedor, quero criar um endpoint completo (UseCase, InputModel, OutputModel, Response, Presenter e Controller) para criar um novo pagamento, para que a API de pagamento possa receber solicitações de criação de pagamento vindas da API de pedidos, sem precisar conhecer a estrutura completa do pedido.

## Objetivo
Criar o fluxo completo para criação de pagamento, incluindo: InputModel para receber OrderId, TotalAmount e OrderSnapshot (JSON), UseCase CreatePaymentUseCase que cria a entidade de domínio Payment com status NotStarted, OutputModel, Response, Presenter, e endpoint POST no PaymentController. O pagamento será criado com status zerado (NotStarted), e os dados do pedido serão armazenados no campo OrderSnapshot (JSONB).

## Escopo Técnico
- Tecnologias: .NET 8, ASP.NET Core, Clean Architecture
- Arquivos afetados:
  - `src/Core/FastFood.PayStream.Application/InputModels/CreatePaymentInputModel.cs`
  - `src/Core/FastFood.PayStream.Application/OutputModels/CreatePaymentOutputModel.cs`
  - `src/Core/FastFood.PayStream.Application/Responses/CreatePaymentResponse.cs`
  - `src/Core/FastFood.PayStream.Application/Presenters/CreatePaymentPresenter.cs`
  - `src/Core/FastFood.PayStream.Application/UseCases/CreatePaymentUseCase.cs`
  - `src/InterfacesExternas/FastFood.PayStream.Api/Controllers/PaymentController.cs`
  - `src/InterfacesExternas/FastFood.PayStream.Api/Program.cs` (registro de DI)
- Estrutura do InputModel:
  - OrderId (Guid) - ID do pedido
  - TotalAmount (decimal) - Valor total do pedido
  - OrderSnapshot (string) - JSON serializado com resumo do pedido
- Comportamento:
  - Cria entidade Payment de domínio com status NotStarted
  - Persiste no banco via repositório
  - Retorna dados do pagamento criado

## Subtasks

- [x] [Subtask 01: Criar InputModel CreatePaymentInputModel](./subtask/Subtask-01-Criar_InputModel.md)
- [x] [Subtask 02: Criar OutputModel CreatePaymentOutputModel](./subtask/Subtask-02-Criar_OutputModel.md)
- [x] [Subtask 03: Criar Response CreatePaymentResponse](./subtask/Subtask-03-Criar_Response.md)
- [x] [Subtask 04: Criar Presenter CreatePaymentPresenter](./subtask/Subtask-04-Criar_Presenter.md)
- [x] [Subtask 05: Criar UseCase CreatePaymentUseCase](./subtask/Subtask-05-Criar_UseCase.md)
- [x] [Subtask 06: Registrar UseCase e Presenter no DI](./subtask/Subtask-06-Registrar_DI.md)
- [x] [Subtask 07: Criar endpoint POST no PaymentController](./subtask/Subtask-07-Criar_endpoint_Controller.md)
- [x] [Subtask 08: Criar testes unitários do UseCase](./subtask/Subtask-08-Criar_testes_UseCase.md)

## Critérios de Aceite da História

- [x] `CreatePaymentInputModel` criado com propriedades: OrderId (Guid), TotalAmount (decimal), OrderSnapshot (string)
- [x] `CreatePaymentOutputModel` criado com propriedades: PaymentId (Guid), OrderId (Guid), Status (int), TotalAmount (decimal), CreatedAt (DateTime)
- [x] `CreatePaymentResponse` criado com mesma estrutura do OutputModel
- [x] `CreatePaymentPresenter` criado transformando OutputModel em Response
- [x] `CreatePaymentUseCase` criado recebendo IPaymentRepository via construtor
- [x] UseCase cria entidade Payment de domínio usando construtor com OrderId, TotalAmount e OrderSnapshot
- [x] UseCase salva Payment via repositório
- [x] UseCase retorna OutputModel com dados do pagamento criado
- [x] UseCase valida que OrderId não é Guid vazio
- [x] UseCase valida que TotalAmount é maior que zero
- [x] UseCase valida que OrderSnapshot não é nulo/vazio
- [x] Endpoint POST `/api/payment/create` criado no PaymentController
- [x] Endpoint recebe CreatePaymentInputModel no body
- [x] Endpoint retorna ApiResponse<CreatePaymentResponse> com status 201 Created
- [x] UseCase e Presenter registrados no container de DI
- [x] Testes unitários criados cobrindo o UseCase
- [x] Projeto compila sem erros
- [x] Endpoint funciona corretamente via Swagger
