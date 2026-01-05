# Subtask 01: Verificar estrutura de ApiResponse e criar se necessário

## Status
- **Estado:** ✅ Concluída
- **Data de Conclusão:** 02/01/2026

## Descrição
Verificar se a classe `ApiResponse<T>` já existe no projeto PayStream, e criar se necessário, seguindo o padrão do projeto orderhub para padronizar as respostas da API.

## Passos de implementação
- [x] Verificar se existe arquivo `ApiResponse.cs` em `src/Core/FastFood.PayStream.Application/Models/Common/` ou similar
- [x] Se não existir, criar diretório `src/Core/FastFood.PayStream.Application/Models/Common/` se necessário
- [x] Criar arquivo `ApiResponse.cs` no diretório Models/Common
- [x] Definir namespace `FastFood.PayStream.Application.Models.Common`
- [x] Criar classe genérica pública `ApiResponse<T>` com:
  - Propriedade `Success` (bool)
  - Propriedade `Message` (string?)
  - Propriedade `Content` (object?)
  - Construtor público: `ApiResponse(object? content, string? message = "Requisição bem-sucedida.", bool success = true)`
  - Método estático `Ok(T? data, string? message = "Requisição bem-sucedida.")` retornando `ApiResponse<T>`
  - Método estático `Ok(string? message = "Requisição bem-sucedida.")` retornando `ApiResponse<T>`
  - Método estático `Fail(string? message)` retornando `ApiResponse<T>`
- [x] Adicionar comentários XML para documentação da classe e métodos
- [x] Criar classe `ObjectExtensions` no mesmo namespace com método de extensão estático:
  - Método `ToNamedContent<T>(this T? obj)` que retorna o objeto diretamente
  - Adicionar comentários XML para documentação

## Como testar
- Executar `dotnet build` no projeto Application (deve compilar sem erros)
- Verificar que a classe pode ser instanciada
- Testar métodos estáticos `Ok` e `Fail`
- Verificar que o namespace está correto e acessível

## Critérios de aceite
- [x] Arquivo `ApiResponse.cs` criado em `src/Core/FastFood.PayStream.Application/Models/Common/`
- [x] Classe `ApiResponse<T>` criada com namespace `FastFood.PayStream.Application.Models.Common`
- [x] Propriedades `Success`, `Message` e `Content` definidas
- [x] Construtor público implementado com parâmetros corretos
- [x] Método estático `Ok(T? data, string? message)` implementado usando `ToNamedContent()`
- [x] Classe `ObjectExtensions` criada com método de extensão `ToNamedContent<T>()`
- [x] Método estático `Ok(string? message)` implementado
- [x] Método estático `Fail(string? message)` implementado
- [x] Comentários XML adicionados para documentação
- [x] Projeto Application compila sem erros
- [x] Classe pode ser usada em outros projetos que referenciam Application
