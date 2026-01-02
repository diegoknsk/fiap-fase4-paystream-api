# Subtask 03: Criar estrutura básica do WebhookPaymentController

## Status
- **Estado:** ✅ Concluída
- **Data de Conclusão:** 02/01/2026

## Descrição
Criar a estrutura básica do `WebhookPaymentController` com atributos de roteamento, documentação Swagger e preparação para injeção de UseCases, seguindo o padrão do projeto orderhub.

## Passos de implementação
- [x] Criar arquivo `WebhookPaymentController.cs` em `src/InterfacesExternas/FastFood.PayStream.Api/Controllers/`
- [x] Adicionar usings necessários:
  - `Microsoft.AspNetCore.Mvc`
  - `Microsoft.AspNetCore.Authorization`
  - `FastFood.PayStream.Application.Models.Common`
- [x] Criar namespace `FastFood.PayStream.Api.Controllers`
- [x] Adicionar atributo `[ApiController]` na classe
- [x] Adicionar atributo `[Route("api/[controller]")]` na classe
- [x] Criar classe pública `WebhookPaymentController` herdando de `ControllerBase`
- [x] Adicionar comentário XML de documentação da classe (para Swagger)
- [x] Criar construtor público (por enquanto vazio ou com comentário indicando que UseCases serão injetados nas próximas stories)
- [x] Adicionar comentários explicando que os endpoints serão implementados nas próximas stories
- [x] Notar que este controller geralmente usa `[AllowAnonymous]` para webhooks externos
- [x] Verificar que o arquivo compila sem erros

## Como testar
- Executar `dotnet build` no projeto Api (deve compilar sem erros)
- Verificar que o controller aparece no Swagger (mesmo sem endpoints)
- Validar que a estrutura está correta (herança, atributos, namespace)
- Verificar que não há erros de compilação

## Critérios de aceite
- [x] Arquivo `WebhookPaymentController.cs` criado em `src/InterfacesExternas/FastFood.PayStream.Api/Controllers/`
- [x] Classe `WebhookPaymentController` herda de `ControllerBase`
- [x] Atributo `[ApiController]` aplicado na classe
- [x] Atributo `[Route("api/[controller]")]` aplicado na classe
- [x] Namespace `FastFood.PayStream.Api.Controllers` definido
- [x] Construtor público criado (pode estar vazio por enquanto)
- [x] Comentários XML adicionados para documentação Swagger
- [x] Projeto Api compila sem erros
- [x] Controller aparece no Swagger (estrutura básica)
