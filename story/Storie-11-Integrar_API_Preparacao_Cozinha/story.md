# Storie-11: Integrar API de Preparação da Cozinha no GetReceipt

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Como desenvolvedor, quero que ao gerar um recibo na API `Payment/receipt-from-gateway` (independente se for com parâmetro fake para resultado fake ou não), antes de finalizar o use case, seja necessário enviar esse pedido para a cozinha através da API de preparação.

## Objetivo
Integrar a chamada para a API de preparação da cozinha (`http://localhost:5010/api/Preparation`) no fluxo de obtenção de recibo, garantindo que:
- O pedido seja enviado para a cozinha antes de finalizar o use case
- Erros da API de preparação sejam propagados corretamente
- A implementação siga a arquitetura Clean Architecture (serviço na camada Infra)
- Configurações (URL e token) sejam externalizadas via appsettings.json

## Escopo Técnico
- Tecnologias: .NET 8, ASP.NET Core, HttpClient, Clean Architecture
- Arquivos afetados:
  - `src/Core/FastFood.PayStream.Application/Ports/IKitchenService.cs` (nova interface)
  - `src/Core/FastFood.PayStream.Application/Ports/Parameters/KitchenPreparationRequest.cs` (novo modelo)
  - `src/Infra/FastFood.PayStream.Infra/Services/KitchenService.cs` (nova implementação)
  - `src/Core/FastFood.PayStream.Application/UseCases/GetReceiptUseCase.cs` (modificar)
  - `src/InterfacesExternas/FastFood.PayStream.Api/appsettings.json` (adicionar configurações)
  - `src/InterfacesExternas/FastFood.PayStream.Api/Program.cs` (registrar DI)
- Dados necessários:
  - OrderId (já disponível no Payment)
  - OrderSnapshot (já disponível no Payment)
  - Token de autenticação (configurável via appsettings)

## Subtasks

- [ ] [Subtask 01: Criar interface IKitchenService na Application](./subtask/Subtask-01-Criar_interface_IKitchenService.md)
- [ ] [Subtask 02: Criar modelo KitchenPreparationRequest](./subtask/Subtask-02-Criar_modelo_KitchenPreparationRequest.md)
- [ ] [Subtask 03: Criar implementação KitchenService na Infra](./subtask/Subtask-03-Criar_implementacao_KitchenService.md)
- [ ] [Subtask 04: Adicionar configurações no appsettings.json](./subtask/Subtask-04-Adicionar_configuracoes_appsettings.md)
- [ ] [Subtask 05: Modificar GetReceiptUseCase para chamar serviço de cozinha](./subtask/Subtask-05-Modificar_GetReceiptUseCase.md)
- [ ] [Subtask 06: Registrar serviços no DI](./subtask/Subtask-06-Registrar_DI.md)

## Critérios de Aceite da História

- [ ] Interface `IKitchenService` criada na camada Application (Port)
- [ ] Modelo `KitchenPreparationRequest` criado com OrderId e OrderSnapshot
- [ ] Implementação `KitchenService` criada na camada Infra usando HttpClient
- [ ] `KitchenService` faz POST para `/api/Preparation` com dados corretos
- [ ] `KitchenService` envia token de autenticação no header Authorization
- [ ] `KitchenService` propaga erros da API de preparação (exceções HTTP)
- [ ] `GetReceiptUseCase` chama `IKitchenService.SendToPreparationAsync` antes de finalizar
- [ ] Configurações `KitchenApi:BaseUrl` e `KitchenApi:Token` adicionadas no appsettings.json
- [ ] `KitchenService` registrado no DI com HttpClient configurado
- [ ] `GetReceiptUseCase` recebe `IKitchenService` via construtor
- [ ] Projeto compila sem erros
- [ ] Endpoint funciona corretamente via Swagger
- [ ] Erros da API de preparação são retornados corretamente ao cliente

## Dados da API de Preparação

**Endpoint:** `POST /api/Preparation`

**Headers:**
- `Authorization: Bearer {token}`
- `Content-Type: application/json`

**Body:**
```json
{
    "orderId": "97b5572b-2934-4db2-8483-3c9f8ec76c7e",
    "orderSnapshot": "{\"items\":[...],\"order\":{...},\"pricing\":{...},\"version\":1}"
}
```

**Observações:**
- O `orderSnapshot` deve ser enviado como string JSON (não deserializado)
- O token deve ser configurável via appsettings.json
- A URL base deve ser configurável via appsettings.json
- Erros HTTP devem ser propagados como exceções
