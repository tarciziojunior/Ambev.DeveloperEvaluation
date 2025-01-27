namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// Response model for GetSale operation
/// </summary>
public class GetSaleItemResponse
{
    public Guid ProductId { get; set; } // ID do produto
    public required string Product { get; set; } // Nome do produto
    public int Quantity { get; set; } // Quantidade
    public decimal UnitPrice { get; set; } // Preço unitário
    public decimal Discount { get; set; } // Desconto
    public decimal TotalItemAmount { get; set; } // Valor total do item
    public bool IsCancelled { get; set; } // Status de cancelamento
}
