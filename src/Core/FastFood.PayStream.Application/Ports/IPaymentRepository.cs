using FastFood.PayStream.Domain.Entities;

namespace FastFood.PayStream.Application.Ports;

/// <summary>
/// Interface que define o contrato para acesso aos dados de pagamento.
/// Seguindo o padrão de Clean Architecture, a Application define as interfaces e a Infra implementa.
/// </summary>
public interface IPaymentRepository
{
    /// <summary>
    /// Busca um pagamento pelo seu identificador único.
    /// </summary>
    /// <param name="id">Identificador único do pagamento.</param>
    /// <returns>Pagamento encontrado ou null se não existir.</returns>
    Task<Payment?> GetByIdAsync(Guid id);

    /// <summary>
    /// Busca um pagamento pelo ID do pedido relacionado.
    /// </summary>
    /// <param name="orderId">ID do pedido relacionado.</param>
    /// <returns>Pagamento encontrado ou null se não existir.</returns>
    Task<Payment?> GetByOrderIdAsync(Guid orderId);

    /// <summary>
    /// Adiciona um novo pagamento ao banco de dados.
    /// </summary>
    /// <param name="payment">Pagamento a ser adicionado.</param>
    /// <returns>Pagamento adicionado com ID gerado.</returns>
    Task<Payment> AddAsync(Payment payment);

    /// <summary>
    /// Atualiza um pagamento existente no banco de dados.
    /// </summary>
    /// <param name="payment">Pagamento com as alterações a serem persistidas.</param>
    Task UpdateAsync(Payment payment);

    /// <summary>
    /// Verifica se um pagamento existe pelo seu identificador único.
    /// </summary>
    /// <param name="id">Identificador único do pagamento.</param>
    /// <returns>True se o pagamento existe, False caso contrário.</returns>
    Task<bool> ExistsAsync(Guid id);
}
