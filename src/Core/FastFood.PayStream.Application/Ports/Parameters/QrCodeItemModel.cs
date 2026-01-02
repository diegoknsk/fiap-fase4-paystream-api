namespace FastFood.PayStream.Application.Ports.Parameters;

/// <summary>
/// Modelo que representa um item do pedido para geração de QR Code.
/// </summary>
public class QrCodeItemModel
{
    /// <summary>
    /// Título do item.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Descrição do item.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Preço unitário do item.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Quantidade do item.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Unidade de medida do item.
    /// </summary>
    public string UnitMeasure { get; set; } = string.Empty;

    /// <summary>
    /// Valor total do item (calculado: UnitPrice * Quantity).
    /// </summary>
    public decimal TotalAmount => UnitPrice * Quantity;
}
