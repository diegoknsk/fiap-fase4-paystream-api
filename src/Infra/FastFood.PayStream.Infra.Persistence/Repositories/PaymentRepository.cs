using Microsoft.EntityFrameworkCore;
using System.Reflection;
using FastFood.PayStream.Application.Ports;
using FastFood.PayStream.Domain.Entities;
using FastFood.PayStream.Domain.Common.Enums;
using FastFood.PayStream.Infra.Persistence;
using FastFood.PayStream.Infra.Persistence.Entities;

namespace FastFood.PayStream.Infra.Persistence.Repositories;

/// <summary>
/// Implementação do repositório de pagamentos que faz o mapeamento entre a entidade de domínio Payment
/// e a entidade de persistência PaymentEntity.
/// </summary>
public class PaymentRepository : IPaymentRepository
{
    private readonly PayStreamDbContext _context;

    /// <summary>
    /// Construtor que recebe o DbContext.
    /// </summary>
    /// <param name="context">DbContext para acesso ao banco de dados.</param>
    public PaymentRepository(PayStreamDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<Payment?> GetByIdAsync(Guid id)
    {
        var entity = await _context.Payments
            .FirstOrDefaultAsync(p => p.Id == id);

        if (entity == null)
            return null;

        return MapToDomain(entity);
    }

    /// <inheritdoc />
    public async Task<Payment?> GetByOrderIdAsync(Guid orderId)
    {
        var entity = await _context.Payments
            .FirstOrDefaultAsync(p => p.OrderId == orderId);

        if (entity == null)
            return null;

        return MapToDomain(entity);
    }

    /// <inheritdoc />
    public async Task<Payment> AddAsync(Payment payment)
    {
        var entity = MapToEntity(payment);
        _context.Payments.Add(entity);
        await _context.SaveChangesAsync();

        // Retornar o Payment mapeado de volta com o ID gerado
        return MapToDomain(entity);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Payment payment)
    {
        var entity = await _context.Payments
            .FirstOrDefaultAsync(p => p.Id == payment.Id);

        if (entity == null)
            throw new InvalidOperationException($"Pagamento com ID {payment.Id} não encontrado.");

        // Atualizar propriedades da entidade
        entity.OrderId = payment.OrderId;
        entity.Status = (int)payment.Status;
        entity.ExternalTransactionId = payment.ExternalTransactionId;
        entity.QrCodeUrl = payment.QrCodeUrl;
        entity.CreatedAt = payment.CreatedAt;
        entity.TotalAmount = payment.TotalAmount;
        entity.OrderSnapshot = payment.OrderSnapshot;

        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Payments
            .AnyAsync(p => p.Id == id);
    }

    /// <summary>
    /// Mapeia PaymentEntity (persistência) para Payment (domínio).
    /// </summary>
    /// <param name="entity">Entidade de persistência.</param>
    /// <returns>Entidade de domínio.</returns>
    private static Payment MapToDomain(PaymentEntity entity)
    {
        // Usar construtor protegido e depois definir propriedades via reflexão
        var payment = (Payment)Activator.CreateInstance(
            typeof(Payment),
            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.CreateInstance,
            null,
            null,
            null)!;

        // Definir propriedades privadas via reflexão
        SetPrivateProperty(payment, nameof(Payment.Id), entity.Id);
        SetPrivateProperty(payment, nameof(Payment.OrderId), entity.OrderId);
        SetPrivateProperty(payment, nameof(Payment.Status), (EnumPaymentStatus)entity.Status);
        SetPrivateProperty(payment, nameof(Payment.ExternalTransactionId), entity.ExternalTransactionId);
        SetPrivateProperty(payment, nameof(Payment.QrCodeUrl), entity.QrCodeUrl);
        SetPrivateProperty(payment, nameof(Payment.CreatedAt), entity.CreatedAt);
        SetPrivateProperty(payment, nameof(Payment.TotalAmount), entity.TotalAmount);
        SetPrivateProperty(payment, nameof(Payment.OrderSnapshot), entity.OrderSnapshot);

        return payment;
    }

    /// <summary>
    /// Mapeia Payment (domínio) para PaymentEntity (persistência).
    /// </summary>
    /// <param name="payment">Entidade de domínio.</param>
    /// <returns>Entidade de persistência.</returns>
    private static PaymentEntity MapToEntity(Payment payment)
    {
        return new PaymentEntity
        {
            Id = payment.Id,
            OrderId = payment.OrderId,
            Status = (int)payment.Status,
            ExternalTransactionId = payment.ExternalTransactionId,
            QrCodeUrl = payment.QrCodeUrl,
            CreatedAt = payment.CreatedAt,
            TotalAmount = payment.TotalAmount,
            OrderSnapshot = payment.OrderSnapshot
        };
    }

    /// <summary>
    /// Define uma propriedade privada usando reflexão.
    /// </summary>
    /// <param name="obj">Objeto que contém a propriedade.</param>
    /// <param name="propertyName">Nome da propriedade.</param>
    /// <param name="value">Valor a ser definido.</param>
    private static void SetPrivateProperty(object obj, string propertyName, object? value)
    {
        var property = obj.GetType().GetProperty(
            propertyName,
            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

        if (property != null && property.CanWrite)
        {
            property.SetValue(obj, value);
        }
    }
}
