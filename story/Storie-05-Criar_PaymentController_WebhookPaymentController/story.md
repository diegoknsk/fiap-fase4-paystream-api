# Storie-05: Criar PaymentController e WebhookPaymentController

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Como desenvolvedor, quero criar a estrutura básica dos controllers PaymentController e WebhookPaymentController na API, seguindo o padrão de Clean Architecture do projeto orderhub, para que possamos expor os endpoints de pagamento e webhook de forma organizada e seguindo as convenções estabelecidas.

## Objetivo
Criar os controllers `PaymentController` e `WebhookPaymentController` com a estrutura básica, atributos de roteamento, documentação Swagger, e preparação para injeção de dependência de UseCases (que serão criados nas próximas stories). Os controllers devem seguir o padrão do projeto orderhub, injetando UseCases diretamente ao invés de usar orchestrators.

## Escopo Técnico
- Tecnologias: .NET 8, ASP.NET Core, Swagger/OpenAPI
- Arquivos afetados:
  - `src/InterfacesExternas/FastFood.PayStream.Api/Controllers/PaymentController.cs`
  - `src/InterfacesExternas/FastFood.PayStream.Api/Controllers/WebhookPaymentController.cs`
- Estrutura dos controllers:
  - `PaymentController`: Controller para endpoints de pagamento (criação, geração de QR Code, obtenção de comprovante)
  - `WebhookPaymentController`: Controller para receber notificações de webhook do gateway de pagamento
- Padrão a seguir:
  - Injeção de UseCases via construtor (não orchestrators)
  - Uso de `ApiResponse<T>` para respostas padronizadas
  - Documentação com XML comments para Swagger
  - Atributos `[ApiController]` e `[Route("api/[controller]")]`
  - Atributos `[ProducesResponseType]` para documentação de respostas

## Subtasks

- [ ] [Subtask 01: Verificar estrutura de ApiResponse e criar se necessário](./subtask/Subtask-01-Verificar_criar_ApiResponse.md)
- [ ] [Subtask 02: Criar estrutura básica do PaymentController](./subtask/Subtask-02-Criar_PaymentController_basico.md)
- [ ] [Subtask 03: Criar estrutura básica do WebhookPaymentController](./subtask/Subtask-03-Criar_WebhookPaymentController_basico.md)
- [ ] [Subtask 04: Configurar documentação Swagger nos controllers](./subtask/Subtask-04-Configurar_Swagger_controllers.md)
- [ ] [Subtask 05: Registrar controllers e validar compilação](./subtask/Subtask-05-Registrar_validar_controllers.md)

## Critérios de Aceite da História

- [ ] Classe `ApiResponse<T>` existe na camada Application ou CrossCutting (seguindo padrão do orderhub)
- [ ] `PaymentController` criado em `src/InterfacesExternas/FastFood.PayStream.Api/Controllers/PaymentController.cs`
- [ ] `PaymentController` herda de `ControllerBase`
- [ ] `PaymentController` tem atributo `[ApiController]` e `[Route("api/[controller]")]`
- [ ] `PaymentController` tem construtor preparado para receber UseCases (comentado ou vazio por enquanto)
- [ ] `WebhookPaymentController` criado em `src/InterfacesExternas/FastFood.PayStream.Api/Controllers/WebhookPaymentController.cs`
- [ ] `WebhookPaymentController` herda de `ControllerBase`
- [ ] `WebhookPaymentController` tem atributo `[ApiController]` e `[Route("api/[controller]")]`
- [ ] `WebhookPaymentController` tem construtor preparado para receber UseCases (comentado ou vazio por enquanto)
- [ ] Ambos controllers têm comentários XML para documentação Swagger
- [ ] Projeto Api compila sem erros
- [ ] Swagger exibe os controllers (mesmo sem endpoints ainda)
- [ ] Estrutura segue padrão do projeto orderhub (UseCases injetados, não orchestrators)
