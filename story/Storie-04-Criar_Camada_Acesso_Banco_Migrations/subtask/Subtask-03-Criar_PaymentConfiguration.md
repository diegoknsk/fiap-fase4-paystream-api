# Subtask 03: Criar configuração PaymentConfiguration com JSONB

## Status
- **Estado:** ✅ Concluída
- **Data de Conclusão:** 02/01/2025

## Descrição
Criar a configuração de mapeamento do Entity Framework Core para a entidade PaymentEntity, incluindo configuração especial para o campo OrderSnapshot como JSONB no PostgreSQL.

## Passos de implementação
- [x] Criar diretório `src/Infra/FastFood.PayStream.Infra.Persistence/Configurations/` se não existir
- [x] Criar arquivo `PaymentConfiguration.cs` no diretório Configurations
- [x] Definir namespace `FastFood.PayStream.Infra.Persistence.Configurations`
- [x] Adicionar using para `Microsoft.EntityFrameworkCore`, `Microsoft.EntityFrameworkCore.Metadata.Builders` e `FastFood.PayStream.Infra.Persistence.Entities`
- [x] Criar classe pública `PaymentConfiguration` implementando `IEntityTypeConfiguration<PaymentEntity>`
- [x] Implementar método `Configure(EntityTypeBuilder<PaymentEntity> builder)`:
  - Configurar nome da tabela como "Payments"
  - Configurar chave primária: `builder.HasKey(p => p.Id)`
  - Configurar `OrderId` como obrigatório (IsRequired)
  - Configurar `Status` como obrigatório (IsRequired)
  - Configurar `CreatedAt` como obrigatório (IsRequired)
  - Configurar `TotalAmount` como obrigatório (IsRequired) e tipo decimal com precisão
  - Configurar `OrderSnapshot` como obrigatório (IsRequired) e tipo JSONB usando `.HasColumnType("jsonb")`
  - Configurar `ExternalTransactionId` como opcional (nullable)
  - Configurar `QrCodeUrl` como opcional (nullable)
- [x] Adicionar comentários XML para documentação

## Como testar
- Executar `dotnet build` no projeto Infra.Persistence (deve compilar sem erros)
- Verificar que a configuração implementa `IEntityTypeConfiguration<PaymentEntity>`
- Validar que todos os campos estão configurados corretamente
- Verificar que OrderSnapshot está configurado como JSONB

## Critérios de aceite
- [x] Arquivo `PaymentConfiguration.cs` criado em `src/Infra/FastFood.PayStream.Infra.Persistence/Configurations/`
- [x] Classe `PaymentConfiguration` implementa `IEntityTypeConfiguration<PaymentEntity>`
- [x] Tabela configurada com nome "Payments"
- [x] Chave primária configurada para Id
- [x] OrderId configurado como obrigatório
- [x] Status configurado como obrigatório
- [x] CreatedAt configurado como obrigatório
- [x] TotalAmount configurado como obrigatório com tipo decimal
- [x] OrderSnapshot configurado como obrigatório e tipo JSONB usando `.HasColumnType("jsonb")`
- [x] ExternalTransactionId configurado como opcional (nullable)
- [x] QrCodeUrl configurado como opcional (nullable)
- [x] Comentários XML adicionados
- [x] Projeto Infra.Persistence compila sem erros
