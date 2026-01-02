# Subtask 09: Criar migration inicial

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Criar a migration inicial do Entity Framework Core para criar a tabela Payments no banco de dados PostgreSQL, incluindo todas as colunas e configurações (JSONB para OrderSnapshot).

## Passos de implementação
- [ ] Verificar que o projeto Infra.Persistence tem referência ao projeto Domain
- [ ] Verificar que o projeto Infra.Persistence tem pacote `Microsoft.EntityFrameworkCore.Design`
- [ ] Abrir terminal na raiz do projeto Infra.Persistence
- [ ] Executar comando para criar migration:
  - `dotnet ef migrations add InitialCreate --startup-project ..\..\InterfacesExternas\FastFood.PayStream.Api\FastFood.PayStream.Api.csproj`
- [ ] Verificar que a migration foi criada em `src/Infra/FastFood.PayStream.Infra.Persistence/Migrations/`
- [ ] Abrir arquivo da migration criada e verificar:
  - Tabela "Payments" está sendo criada
  - Todas as colunas estão presentes (Id, OrderId, Status, ExternalTransactionId, QrCodeUrl, CreatedAt, TotalAmount, OrderSnapshot)
  - OrderSnapshot está configurado como JSONB
  - Chave primária está configurada para Id
- [ ] Verificar que o arquivo `*ModelSnapshot.cs` foi criado/atualizado

## Como testar
- Executar `dotnet ef migrations list` para verificar que a migration aparece na lista
- Verificar que os arquivos de migration foram criados corretamente
- Validar que a migration pode ser aplicada (usar `dotnet ef database update` - mas não aplicar ainda, apenas verificar)
- Verificar que não há erros de compilação na migration

## Critérios de aceite
- [ ] Migration criada com nome `InitialCreate` (ou similar com timestamp)
- [ ] Arquivos de migration criados em `src/Infra/FastFood.PayStream.Infra.Persistence/Migrations/`
- [ ] Migration cria tabela "Payments"
- [ ] Todas as 8 colunas estão na migration (Id, OrderId, Status, ExternalTransactionId, QrCodeUrl, CreatedAt, TotalAmount, OrderSnapshot)
- [ ] Coluna OrderSnapshot está configurada como JSONB
- [ ] Chave primária configurada para Id
- [ ] Arquivo ModelSnapshot criado/atualizado
- [ ] `dotnet ef migrations list` mostra a migration
- [ ] Migration compila sem erros
