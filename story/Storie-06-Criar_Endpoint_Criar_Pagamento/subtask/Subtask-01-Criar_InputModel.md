# Subtask 01: Criar InputModel CreatePaymentInputModel

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Criar o InputModel que será usado para receber os dados de criação de pagamento no endpoint, contendo OrderId, TotalAmount e OrderSnapshot.

## Passos de implementação
- [ ] Criar diretório `src/Core/FastFood.PayStream.Application/InputModels/` se não existir
- [ ] Criar arquivo `CreatePaymentInputModel.cs` no diretório InputModels
- [ ] Definir namespace `FastFood.PayStream.Application.InputModels`
- [ ] Criar classe pública `CreatePaymentInputModel` com as seguintes propriedades públicas:
  - `OrderId` (Guid) - ID do pedido relacionado
  - `TotalAmount` (decimal) - Valor total do pedido
  - `OrderSnapshot` (string) - JSON serializado com resumo do pedido
- [ ] Adicionar comentários XML para documentação da classe e propriedades
- [ ] Adicionar data annotations para validação (se necessário):
  - `[Required]` para OrderId
  - `[Range(0.01, double.MaxValue)]` para TotalAmount
  - `[Required]` para OrderSnapshot

## Como testar
- Executar `dotnet build` no projeto Application (deve compilar sem erros)
- Verificar que a classe pode ser instanciada
- Validar que todas as propriedades estão acessíveis
- Testar criação de instância com valores válidos

## Critérios de aceite
- [ ] Arquivo `CreatePaymentInputModel.cs` criado em `src/Core/FastFood.PayStream.Application/InputModels/`
- [ ] Classe `CreatePaymentInputModel` criada com namespace `FastFood.PayStream.Application.InputModels`
- [ ] Propriedade `OrderId` (Guid) definida
- [ ] Propriedade `TotalAmount` (decimal) definida
- [ ] Propriedade `OrderSnapshot` (string) definida
- [ ] Comentários XML adicionados para documentação
- [ ] Data annotations adicionadas para validação (opcional, mas recomendado)
- [ ] Projeto Application compila sem erros
