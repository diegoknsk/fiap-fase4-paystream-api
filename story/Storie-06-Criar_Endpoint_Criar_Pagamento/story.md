# Storie-06: Criar Endpoint para Criar Novo Pagamento

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

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

- [ ] [Subtask 01: Criar InputModel CreatePaymentInputModel](./subtask/Subtask-01-Criar_InputModel.md)
- [ ] [Subtask 02: Criar OutputModel CreatePaymentOutputModel](./subtask/Subtask-02-Criar_OutputModel.md)
- [ ] [Subtask 03: Criar Response CreatePaymentResponse](./subtask/Subtask-03-Criar_Response.md)
- [ ] [Subtask 04: Criar Presenter CreatePaymentPresenter](./subtask/Subtask-04-Criar_Presenter.md)
- [ ] [Subtask 05: Criar UseCase CreatePaymentUseCase](./subtask/Subtask-05-Criar_UseCase.md)
- [ ] [Subtask 06: Registrar UseCase e Presenter no DI](./subtask/Subtask-06-Registrar_DI.md)
- [ ] [Subtask 07: Criar endpoint POST no PaymentController](./subtask/Subtask-07-Criar_endpoint_Controller.md)
- [ ] [Subtask 08: Criar testes unitários do UseCase](./subtask/Subtask-08-Criar_testes_UseCase.md)

## Critérios de Aceite da História

- [ ] `CreatePaymentInputModel` criado com propriedades: OrderId (Guid), TotalAmount (decimal), OrderSnapshot (string)
- [ ] `CreatePaymentOutputModel` criado com propriedades: PaymentId (Guid), OrderId (Guid), Status (int), TotalAmount (decimal), CreatedAt (DateTime)
- [ ] `CreatePaymentResponse` criado com mesma estrutura do OutputModel
- [ ] `CreatePaymentPresenter` criado transformando OutputModel em Response
- [ ] `CreatePaymentUseCase` criado recebendo IPaymentRepository via construtor
- [ ] UseCase cria entidade Payment de domínio usando construtor com OrderId, TotalAmount e OrderSnapshot
- [ ] UseCase salva Payment via repositório
- [ ] UseCase retorna OutputModel com dados do pagamento criado
- [ ] UseCase valida que OrderId não é Guid vazio
- [ ] UseCase valida que TotalAmount é maior que zero
- [ ] UseCase valida que OrderSnapshot não é nulo/vazio
- [ ] Endpoint POST `/api/payment/create` criado no PaymentController
- [ ] Endpoint recebe CreatePaymentInputModel no body
- [ ] Endpoint retorna ApiResponse<CreatePaymentResponse> com status 201 Created
- [ ] UseCase e Presenter registrados no container de DI
- [ ] Testes unitários criados cobrindo o UseCase
- [ ] Projeto compila sem erros
- [ ] Endpoint funciona corretamente via Swagger
