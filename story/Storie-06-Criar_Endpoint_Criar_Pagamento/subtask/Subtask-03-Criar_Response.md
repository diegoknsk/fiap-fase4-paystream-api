# Subtask 03: Criar Response CreatePaymentResponse

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Criar o Response que será retornado pelo endpoint, seguindo o padrão do projeto orderhub onde Response tem a mesma estrutura do OutputModel.

## Passos de implementação
- [ ] Criar diretório `src/Core/FastFood.PayStream.Application/Responses/` se não existir
- [ ] Criar arquivo `CreatePaymentResponse.cs` no diretório Responses
- [ ] Definir namespace `FastFood.PayStream.Application.Responses`
- [ ] Criar classe pública `CreatePaymentResponse` com as seguintes propriedades públicas (mesma estrutura do OutputModel):
  - `PaymentId` (Guid) - ID do pagamento criado
  - `OrderId` (Guid) - ID do pedido relacionado
  - `Status` (int) - Status do pagamento
  - `TotalAmount` (decimal) - Valor total do pedido
  - `CreatedAt` (DateTime) - Data de criação do pagamento
- [ ] Adicionar comentários XML para documentação da classe e propriedades

## Como testar
- Executar `dotnet build` no projeto Application (deve compilar sem erros)
- Verificar que a classe pode ser instanciada
- Validar que a estrutura é idêntica ao OutputModel
- Testar criação de instância com valores válidos

## Critérios de aceite
- [ ] Arquivo `CreatePaymentResponse.cs` criado em `src/Core/FastFood.PayStream.Application/Responses/`
- [ ] Classe `CreatePaymentResponse` criada com namespace `FastFood.PayStream.Application.Responses`
- [ ] Propriedades idênticas ao OutputModel (PaymentId, OrderId, Status, TotalAmount, CreatedAt)
- [ ] Comentários XML adicionados para documentação
- [ ] Projeto Application compila sem erros
