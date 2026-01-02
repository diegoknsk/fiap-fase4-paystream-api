# Subtask 04: Criar DbContext PayStreamDbContext

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Criar o DbContext `PayStreamDbContext` que gerencia a conexão com o banco de dados PostgreSQL e aplica as configurações de mapeamento, seguindo o padrão do projeto auth-lambda.

## Passos de implementação
- [ ] Criar arquivo `PayStreamDbContext.cs` em `src/Infra/FastFood.PayStream.Infra.Persistence/`
- [ ] Definir namespace `FastFood.PayStream.Infra.Persistence`
- [ ] Adicionar using para `Microsoft.EntityFrameworkCore` e `FastFood.PayStream.Infra.Persistence.Entities` e `FastFood.PayStream.Infra.Persistence.Configurations`
- [ ] Criar classe pública `PayStreamDbContext` herdando de `DbContext`
- [ ] Criar construtor público recebendo `DbContextOptions<PayStreamDbContext> options` e passando para base
- [ ] Criar propriedade pública `DbSet<PaymentEntity> Payments { get; set; } = null!;`
- [ ] Sobrescrever método `OnModelCreating(ModelBuilder modelBuilder)`:
  - Chamar `base.OnModelCreating(modelBuilder)`
  - Aplicar configuração: `modelBuilder.ApplyConfiguration(new PaymentConfiguration())`
- [ ] Adicionar comentários XML para documentação da classe

## Como testar
- Executar `dotnet build` no projeto Infra.Persistence (deve compilar sem erros)
- Verificar que a classe herda de `DbContext`
- Validar que o DbSet Payments está definido
- Verificar que OnModelCreating aplica a configuração corretamente
- Testar que o DbContext pode ser instanciado (com opções)

## Critérios de aceite
- [ ] Arquivo `PayStreamDbContext.cs` criado em `src/Infra/FastFood.PayStream.Infra.Persistence/`
- [ ] Classe `PayStreamDbContext` herda de `DbContext`
- [ ] Construtor público recebe `DbContextOptions<PayStreamDbContext> options`
- [ ] Propriedade `DbSet<PaymentEntity> Payments` criada e inicializada como `null!`
- [ ] Método `OnModelCreating` sobrescrito e aplica `PaymentConfiguration`
- [ ] Comentários XML adicionados para documentação
- [ ] Projeto Infra.Persistence compila sem erros
- [ ] DbContext pode ser instanciado com opções
