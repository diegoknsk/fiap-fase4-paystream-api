using Microsoft.EntityFrameworkCore;
using FastFood.PayStream.Infra.Persistence.Entities;
using FastFood.PayStream.Infra.Persistence.Configurations;

namespace FastFood.PayStream.Infra.Persistence;

/// <summary>
/// DbContext que gerencia a conexão com o banco de dados PostgreSQL e aplica as configurações de mapeamento.
/// </summary>
public class PayStreamDbContext : DbContext
{
    /// <summary>
    /// Construtor que recebe as opções de configuração do DbContext.
    /// </summary>
    /// <param name="options">Opções de configuração do DbContext.</param>
    public PayStreamDbContext(DbContextOptions<PayStreamDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// DbSet para acessar a tabela Payments.
    /// </summary>
    public DbSet<PaymentEntity> Payments { get; set; } = null!;

    /// <summary>
    /// Configura o modelo de dados aplicando as configurações de mapeamento.
    /// </summary>
    /// <param name="modelBuilder">Builder para configuração do modelo.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Aplicar configurações de mapeamento
        modelBuilder.ApplyConfiguration(new PaymentConfiguration());
    }
}
