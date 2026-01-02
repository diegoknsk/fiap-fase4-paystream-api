# Subtask 02: Criar estrutura básica do PaymentController

## Status
- **Estado:** ✅ Concluída
- **Data de Conclusão:** 02/01/2026

## Descrição
Criar a estrutura básica do `PaymentController` com atributos de roteamento, documentação Swagger e preparação para injeção de UseCases, seguindo o padrão do projeto orderhub.

## Passos de implementação
- [x] Criar arquivo `PaymentController.cs` em `src/InterfacesExternas/FastFood.PayStream.Api/Controllers/`
- [x] Adicionar usings necessários:
  - `Microsoft.AspNetCore.Mvc`
  - `FastFood.PayStream.Application.Models.Common`
- [x] Criar namespace `FastFood.PayStream.Api.Controllers`
- [x] Adicionar atributo `[ApiController]` na classe
- [x] Adicionar atributo `[Route("api/[controller]")]` na classe
- [x] Criar classe pública `PaymentController` herdando de `ControllerBase`
- [x] Adicionar comentário XML de documentação da classe (para Swagger)
- [x] Criar construtor público (por enquanto vazio ou com comentário indicando que UseCases serão injetados nas próximas stories)
- [x] Adicionar comentários explicando que os endpoints serão implementados nas próximas stories
- [x] Verificar que o arquivo compila sem erros

## Como testar
- Executar `dotnet build` no projeto Api (deve compilar sem erros)
- Verificar que o controller aparece no Swagger (mesmo sem endpoints)
- Validar que a estrutura está correta (herança, atributos, namespace)
- Verificar que não há erros de compilação

## Critérios de aceite
- [x] Arquivo `PaymentController.cs` criado em `src/InterfacesExternas/FastFood.PayStream.Api/Controllers/`
- [x] Classe `PaymentController` herda de `ControllerBase`
- [x] Atributo `[ApiController]` aplicado na classe
- [x] Atributo `[Route("api/[controller]")]` aplicado na classe
- [x] Namespace `FastFood.PayStream.Api.Controllers` definido
- [x] Construtor público criado (pode estar vazio por enquanto)
- [x] Comentários XML adicionados para documentação Swagger
- [x] Projeto Api compila sem erros
- [x] Controller aparece no Swagger (estrutura básica)
