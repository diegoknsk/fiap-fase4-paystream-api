# Subtask 04: Criar DbContext PayStreamDbContext

## Status
- **Estado:** ✅ Concluída
- **Data de Conclusão:** 02/01/2025

## Descrição
Criar o DbContext `PayStreamDbContext` que gerencia a conexão com o banco de dados PostgreSQL e aplica as configurações de mapeamento, seguindo o padrão do projeto auth-lambda.

## Passos de implementação
- [x] Criar arquivo `PayStreamDbContext.cs` em `src/Infra/FastFood.PayStream.Infra.Persistence/`
- [x] Definir namespace `FastFood.PayStream.Infra.Persistence`
- [x] Adicionar using para `Microsoft.EntityFrameworkCore` e `FastFood.PayStream.Infra.Persistence.Entities` e `FastFood.PayStream.Infra.Persistence.Configurations`
- [x] Criar classe pública `PayStreamDbContext` herdando de `DbContext`
- [x] Criar construtor público recebendo `DbContextOptions<PayStreamDbContext> options` e passando para base
- [x] Criar propriedade pública `DbSet<PaymentEntity> Payments { get; set; } = null!;`
- [x] Sobrescrever método `OnModelCreating(ModelBuilder modelBuilder)`:
  - Chamar `base.OnModelCreating(modelBuilder)`
  - Aplicar configuração: `modelBuilder.ApplyConfiguration(new PaymentConfiguration())`
- [x] Adicionar comentários XML para documentação da classe

## Como testar
- Executar `dotnet build` no projeto Infra.Persistence (deve compilar sem erros)
- Verificar que a classe herda de `DbContext`
- Validar que o DbSet Payments está definido
- Verificar que OnModelCreating aplica a configuração corretamente
- Testar que o DbContext pode ser instanciado (com opções)

## Critérios de aceite
- [x] Arquivo `PayStreamDbContext.cs` criado em `src/Infra/FastFood.PayStream.Infra.Persistence/`
- [x] Classe `PayStreamDbContext` herda de `DbContext`
- [x] Construtor público recebe `DbContextOptions<PayStreamDbContext> options`
- [x] Propriedade `DbSet<PaymentEntity> Payments` criada e inicializada como `null!`
- [x] Método `OnModelCreating` sobrescrito e aplica `PaymentConfiguration`
- [x] Comentários XML adicionados para documentação
- [x] Projeto Infra.Persistence compila sem erros
- [x] DbContext pode ser instanciado com opções
