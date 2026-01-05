using System.ComponentModel.DataAnnotations;

namespace FastFood.PayStream.Application.InputModels;

/// <summary>
/// Modelo de entrada para criação de um novo pagamento.
/// </summary>
public class CreatePaymentInputModel
{
    /// <summary>
    /// ID do pedido relacionado ao pagamento.
    /// </summary>
    [Required(ErrorMessage = "OrderId é obrigatório.")]
    public Guid OrderId { get; set; }

    /// <summary>
    /// Valor total do pedido.
    /// </summary>
    [Required(ErrorMessage = "TotalAmount é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "TotalAmount deve ser maior que zero.")]
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Resumo do pedido serializado como JSON.
    /// </summary>
    [Required(ErrorMessage = "OrderSnapshot é obrigatório.")]
    public string OrderSnapshot { get; set; } = string.Empty;
}
