# Subtask 09: Criar migration inicial

## Status
- **Estado:** ✅ Concluída
- **Data de Conclusão:** 02/01/2025

## Descrição
Criar a migration inicial do Entity Framework Core para criar a tabela Payments no banco de dados PostgreSQL, incluindo todas as colunas e configurações (JSONB para OrderSnapshot).

## Passos de implementação
- [x] Verificar que o projeto Infra.Persistence tem referência ao projeto Domain
- [x] Verificar que o projeto Infra.Persistence tem pacote `Microsoft.EntityFrameworkCore.Design`
- [x] Abrir terminal na raiz do projeto Infra.Persistence
- [x] Executar comando para criar migration:
  - `dotnet ef migrations add InitialCreate --startup-project ..\..\InterfacesExternas\FastFood.PayStream.Api\FastFood.PayStream.Api.csproj`
- [x] Verificar que a migration foi criada em `src/Infra/FastFood.PayStream.Infra.Persistence/Migrations/`
- [x] Abrir arquivo da migration criada e verificar:
  - Tabela "Payments" está sendo criada
  - Todas as colunas estão presentes (Id, OrderId, Status, ExternalTransactionId, QrCodeUrl, CreatedAt, TotalAmount, OrderSnapshot)
  - OrderSnapshot está configurado como JSONB
  - Chave primária está configurada para Id
- [x] Verificar que o arquivo `*ModelSnapshot.cs` foi criado/atualizado

## Como testar
- Executar `dotnet ef migrations list` para verificar que a migration aparece na lista
- Verificar que os arquivos de migration foram criados corretamente
- Validar que a migration pode ser aplicada (usar `dotnet ef database update` - mas não aplicar ainda, apenas verificar)
- Verificar que não há erros de compilação na migration

## Critérios de aceite
- [x] Migration criada com nome `InitialCreate` (ou similar com timestamp)
- [x] Arquivos de migration criados em `src/Infra/FastFood.PayStream.Infra.Persistence/Migrations/`
- [x] Migration cria tabela "Payments"
- [x] Todas as 8 colunas estão na migration (Id, OrderId, Status, ExternalTransactionId, QrCodeUrl, CreatedAt, TotalAmount, OrderSnapshot)
- [x] Coluna OrderSnapshot está configurada como JSONB
- [x] Chave primária configurada para Id
- [x] Arquivo ModelSnapshot criado/atualizado
- [x] `dotnet ef migrations list` mostra a migration
- [x] Migration compila sem erros
