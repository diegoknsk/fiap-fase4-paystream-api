using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FastFood.PayStream.Infra.Persistence.Entities;

namespace FastFood.PayStream.Infra.Persistence.Configurations;

/// <summary>
/// Configuração de mapeamento do Entity Framework Core para a entidade PaymentEntity.
/// </summary>
public class PaymentConfiguration : IEntityTypeConfiguration<PaymentEntity>
{
    /// <summary>
    /// Configura o mapeamento da entidade PaymentEntity para a tabela Payments.
    /// </summary>
    /// <param name="builder">Builder para configuração da entidade.</param>
    public void Configure(EntityTypeBuilder<PaymentEntity> builder)
    {
        // Configurar nome da tabela
        builder.ToTable("Payments");

        // Configurar chave primária
        builder.HasKey(p => p.Id);

        // Configurar propriedades obrigatórias
        builder.Property(p => p.OrderId)
            .IsRequired();

        builder.Property(p => p.Status)
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.Property(p => p.TotalAmount)
            .IsRequired()
            .HasPrecision(18, 2);

        // Configurar OrderSnapshot como JSONB no PostgreSQL
        builder.Property(p => p.OrderSnapshot)
            .IsRequired()
            .HasColumnType("jsonb");

        // Configurar propriedades opcionais (nullable)
        builder.Property(p => p.ExternalTransactionId)
            .IsRequired(false);

        builder.Property(p => p.QrCodeUrl)
            .IsRequired(false);
    }
}
