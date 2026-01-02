# Subtask 01: Verificar estrutura de ApiResponse e criar se necessário

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Verificar se a classe `ApiResponse<T>` já existe no projeto PayStream, e criar se necessário, seguindo o padrão do projeto orderhub para padronizar as respostas da API.

## Passos de implementação
- [ ] Verificar se existe arquivo `ApiResponse.cs` em `src/Core/FastFood.PayStream.Application/Models/Common/` ou similar
- [ ] Se não existir, criar diretório `src/Core/FastFood.PayStream.Application/Models/Common/` se necessário
- [ ] Criar arquivo `ApiResponse.cs` no diretório Models/Common
- [ ] Definir namespace `FastFood.PayStream.Application.Models.Common`
- [ ] Criar classe genérica pública `ApiResponse<T>` com:
  - Propriedade `Success` (bool)
  - Propriedade `Message` (string?)
  - Propriedade `Content` (object?)
  - Construtor público: `ApiResponse(object? content, string? message = "Requisição bem-sucedida.", bool success = true)`
  - Método estático `Ok(T? data, string? message = "Requisição bem-sucedida.")` retornando `ApiResponse<T>`
  - Método estático `Ok(string? message = "Requisição bem-sucedida.")` retornando `ApiResponse<T>`
  - Método estático `Fail(string? message)` retornando `ApiResponse<T>`
- [ ] Adicionar comentários XML para documentação da classe e métodos
- [ ] Criar classe `ObjectExtensions` no mesmo namespace com método de extensão estático:
  - Método `ToNamedContent<T>(this T? obj)` que retorna o objeto diretamente
  - Adicionar comentários XML para documentação

## Como testar
- Executar `dotnet build` no projeto Application (deve compilar sem erros)
- Verificar que a classe pode ser instanciada
- Testar métodos estáticos `Ok` e `Fail`
- Verificar que o namespace está correto e acessível

## Critérios de aceite
- [ ] Arquivo `ApiResponse.cs` criado em `src/Core/FastFood.PayStream.Application/Models/Common/`
- [ ] Classe `ApiResponse<T>` criada com namespace `FastFood.PayStream.Application.Models.Common`
- [ ] Propriedades `Success`, `Message` e `Content` definidas
- [ ] Construtor público implementado com parâmetros corretos
- [ ] Método estático `Ok(T? data, string? message)` implementado usando `ToNamedContent()`
- [ ] Classe `ObjectExtensions` criada com método de extensão `ToNamedContent<T>()`
- [ ] Método estático `Ok(string? message)` implementado
- [ ] Método estático `Fail(string? message)` implementado
- [ ] Comentários XML adicionados para documentação
- [ ] Projeto Application compila sem erros
- [ ] Classe pode ser usada em outros projetos que referenciam Application
