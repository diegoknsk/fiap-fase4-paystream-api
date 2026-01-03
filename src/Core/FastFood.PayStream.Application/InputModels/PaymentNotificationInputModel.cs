using System.ComponentModel.DataAnnotations;

namespace FastFood.PayStream.Application.InputModels;

/// <summary>
/// Modelo de entrada para notificação de pagamento (webhook).
/// </summary>
public class PaymentNotificationInputModel
{
    /// <summary>
    /// ID do pedido relacionado ao pagamento.
    /// </summary>
    [Required(ErrorMessage = "OrderId é obrigatório.")]
    public Guid OrderId { get; set; }

    /// <summary>
    /// Indica se deve usar gateway fake para desenvolvimento/testes (default: false).
    /// </summary>
    public bool FakeCheckout { get; set; } = false;
}
