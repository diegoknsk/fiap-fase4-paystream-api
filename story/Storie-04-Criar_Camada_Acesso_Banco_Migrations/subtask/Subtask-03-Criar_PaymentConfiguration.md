# Subtask 03: Criar configuração PaymentConfiguration com JSONB

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Criar a configuração de mapeamento do Entity Framework Core para a entidade PaymentEntity, incluindo configuração especial para o campo OrderSnapshot como JSONB no PostgreSQL.

## Passos de implementação
- [ ] Criar diretório `src/Infra/FastFood.PayStream.Infra.Persistence/Configurations/` se não existir
- [ ] Criar arquivo `PaymentConfiguration.cs` no diretório Configurations
- [ ] Definir namespace `FastFood.PayStream.Infra.Persistence.Configurations`
- [ ] Adicionar using para `Microsoft.EntityFrameworkCore`, `Microsoft.EntityFrameworkCore.Metadata.Builders` e `FastFood.PayStream.Infra.Persistence.Entities`
- [ ] Criar classe pública `PaymentConfiguration` implementando `IEntityTypeConfiguration<PaymentEntity>`
- [ ] Implementar método `Configure(EntityTypeBuilder<PaymentEntity> builder)`:
  - Configurar nome da tabela como "Payments"
  - Configurar chave primária: `builder.HasKey(p => p.Id)`
  - Configurar `OrderId` como obrigatório (IsRequired)
  - Configurar `Status` como obrigatório (IsRequired)
  - Configurar `CreatedAt` como obrigatório (IsRequired)
  - Configurar `TotalAmount` como obrigatório (IsRequired) e tipo decimal com precisão
  - Configurar `OrderSnapshot` como obrigatório (IsRequired) e tipo JSONB usando `.HasColumnType("jsonb")`
  - Configurar `ExternalTransactionId` como opcional (nullable)
  - Configurar `QrCodeUrl` como opcional (nullable)
- [ ] Adicionar comentários XML para documentação

## Como testar
- Executar `dotnet build` no projeto Infra.Persistence (deve compilar sem erros)
- Verificar que a configuração implementa `IEntityTypeConfiguration<PaymentEntity>`
- Validar que todos os campos estão configurados corretamente
- Verificar que OrderSnapshot está configurado como JSONB

## Critérios de aceite
- [ ] Arquivo `PaymentConfiguration.cs` criado em `src/Infra/FastFood.PayStream.Infra.Persistence/Configurations/`
- [ ] Classe `PaymentConfiguration` implementa `IEntityTypeConfiguration<PaymentEntity>`
- [ ] Tabela configurada com nome "Payments"
- [ ] Chave primária configurada para Id
- [ ] OrderId configurado como obrigatório
- [ ] Status configurado como obrigatório
- [ ] CreatedAt configurado como obrigatório
- [ ] TotalAmount configurado como obrigatório com tipo decimal
- [ ] OrderSnapshot configurado como obrigatório e tipo JSONB usando `.HasColumnType("jsonb")`
- [ ] ExternalTransactionId configurado como opcional (nullable)
- [ ] QrCodeUrl configurado como opcional (nullable)
- [ ] Comentários XML adicionados
- [ ] Projeto Infra.Persistence compila sem erros
