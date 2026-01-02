# Subtask 02: Criar classe DomainValidation para validações

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Criar a classe utilitária `DomainValidation` que será usada para validações de regras de domínio, seguindo o padrão do monolito para manter consistência.

## Passos de implementação
- [ ] Criar diretório `src/Core/FastFood.PayStream.Domain/Common/Exceptions/` se não existir
- [ ] Criar arquivo `DomainValidation.cs` no diretório Common/Exceptions
- [ ] Definir namespace `FastFood.PayStream.Domain.Common.Exceptions`
- [ ] Criar classe estática pública `DomainValidation`
- [ ] Implementar método estático `ThrowIfNullOrWhiteSpace(string? value, string message)` que:
  - Verifica se o valor é null ou string vazia/whitespace
  - Lança `ArgumentException` com a mensagem fornecida se a validação falhar
- [ ] Adicionar comentários XML para documentação do método

## Como testar
- Executar `dotnet build` no projeto Domain (deve compilar sem erros)
- Criar teste unitário básico validando que o método lança exceção quando valor é null
- Criar teste unitário validando que o método lança exceção quando valor é string vazia
- Criar teste unitário validando que o método não lança exceção quando valor é válido
- Verificar que a classe pode ser usada em outras partes do Domain

## Critérios de aceite
- [ ] Arquivo `DomainValidation.cs` criado em `src/Core/FastFood.PayStream.Domain/Common/Exceptions/`
- [ ] Classe estática pública `DomainValidation` criada
- [ ] Método `ThrowIfNullOrWhiteSpace(string? value, string message)` implementado
- [ ] Método lança `ArgumentException` quando value é null, vazio ou whitespace
- [ ] Método não lança exceção quando value é válido
- [ ] Comentários XML adicionados para documentação
- [ ] Projeto Domain compila sem erros
- [ ] Classe pode ser usada em outras partes do Domain
