# Subtask 03: Criar estrutura básica do WebhookPaymentController

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Criar a estrutura básica do `WebhookPaymentController` com atributos de roteamento, documentação Swagger e preparação para injeção de UseCases, seguindo o padrão do projeto orderhub.

## Passos de implementação
- [ ] Criar arquivo `WebhookPaymentController.cs` em `src/InterfacesExternas/FastFood.PayStream.Api/Controllers/`
- [ ] Adicionar usings necessários:
  - `Microsoft.AspNetCore.Mvc`
  - `Microsoft.AspNetCore.Authorization`
  - `FastFood.PayStream.Application.Models.Common`
- [ ] Criar namespace `FastFood.PayStream.Api.Controllers`
- [ ] Adicionar atributo `[ApiController]` na classe
- [ ] Adicionar atributo `[Route("api/[controller]")]` na classe
- [ ] Criar classe pública `WebhookPaymentController` herdando de `ControllerBase`
- [ ] Adicionar comentário XML de documentação da classe (para Swagger)
- [ ] Criar construtor público (por enquanto vazio ou com comentário indicando que UseCases serão injetados nas próximas stories)
- [ ] Adicionar comentários explicando que os endpoints serão implementados nas próximas stories
- [ ] Notar que este controller geralmente usa `[AllowAnonymous]` para webhooks externos
- [ ] Verificar que o arquivo compila sem erros

## Como testar
- Executar `dotnet build` no projeto Api (deve compilar sem erros)
- Verificar que o controller aparece no Swagger (mesmo sem endpoints)
- Validar que a estrutura está correta (herança, atributos, namespace)
- Verificar que não há erros de compilação

## Critérios de aceite
- [ ] Arquivo `WebhookPaymentController.cs` criado em `src/InterfacesExternas/FastFood.PayStream.Api/Controllers/`
- [ ] Classe `WebhookPaymentController` herda de `ControllerBase`
- [ ] Atributo `[ApiController]` aplicado na classe
- [ ] Atributo `[Route("api/[controller]")]` aplicado na classe
- [ ] Namespace `FastFood.PayStream.Api.Controllers` definido
- [ ] Construtor público criado (pode estar vazio por enquanto)
- [ ] Comentários XML adicionados para documentação Swagger
- [ ] Projeto Api compila sem erros
- [ ] Controller aparece no Swagger (estrutura básica)
