# Subtask 04: Implementar métodos de domínio na entidade Payment

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Implementar os métodos de domínio que encapsulam as regras de negócio da entidade Payment, seguindo o padrão do monolito: Start, GenerateQrCode, Approve, Reject e Cancel.

## Passos de implementação
- [ ] Abrir arquivo `Payment.cs` criado na subtask anterior
- [ ] Implementar método público `Start()`:
  - Alterar `Status` para `EnumPaymentStatus.Started`
- [ ] Implementar método público `GenerateQrCode(string qrCodeUrl)`:
  - Usar `DomainValidation.ThrowIfNullOrWhiteSpace(qrCodeUrl, "QR Code is required.")` para validar
  - Atribuir `QrCodeUrl = qrCodeUrl`
  - Alterar `Status` para `EnumPaymentStatus.QrCodeGenerated`
- [ ] Implementar método público `Approve(string transactionId)`:
  - Usar `DomainValidation.ThrowIfNullOrWhiteSpace(transactionId, "Transaction ID is required.")` para validar
  - Atribuir `ExternalTransactionId = transactionId`
  - Alterar `Status` para `EnumPaymentStatus.Approved`
- [ ] Implementar método público `Reject()`:
  - Alterar `Status` para `EnumPaymentStatus.Rejected`
- [ ] Implementar método público `Cancel()`:
  - Alterar `Status` para `EnumPaymentStatus.Canceled`
- [ ] Adicionar comentários XML para documentação de cada método
- [ ] Adicionar using para `FastFood.PayStream.Domain.Common.Exceptions` e `FastFood.PayStream.Domain.Common.Enums`

## Como testar
- Executar `dotnet build` no projeto Domain (deve compilar sem erros)
- Validar que todos os métodos estão acessíveis publicamente
- Verificar que os métodos de validação (GenerateQrCode, Approve) lançam exceção quando recebem valores inválidos
- Verificar que os métodos alteram o Status corretamente
- Testar fluxo completo: Start → GenerateQrCode → Approve
- Testar fluxo de rejeição: Start → GenerateQrCode → Reject
- Testar fluxo de cancelamento: Start → Cancel

## Critérios de aceite
- [ ] Método `Start()` implementado e altera Status para Started
- [ ] Método `GenerateQrCode(string qrCodeUrl)` implementado
- [ ] Método `GenerateQrCode` valida qrCodeUrl usando DomainValidation
- [ ] Método `GenerateQrCode` atribui QrCodeUrl e altera Status para QrCodeGenerated
- [ ] Método `Approve(string transactionId)` implementado
- [ ] Método `Approve` valida transactionId usando DomainValidation
- [ ] Método `Approve` atribui ExternalTransactionId e altera Status para Approved
- [ ] Método `Reject()` implementado e altera Status para Rejected
- [ ] Método `Cancel()` implementado e altera Status para Canceled
- [ ] Comentários XML adicionados para todos os métodos
- [ ] Usings corretos adicionados
- [ ] Projeto Domain compila sem erros
