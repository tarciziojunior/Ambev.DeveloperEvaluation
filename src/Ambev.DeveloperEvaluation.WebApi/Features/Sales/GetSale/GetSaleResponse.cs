namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// Response model for GetSale operation
/// </summary>
public class GetSaleResponse
{
    public int SaleNumber { get; set; } // Número da venda
    public DateTime SaleDate { get; set; } // Data da venda
    public required string Customer { get; set; } // Cliente
    public decimal TotalSaleAmount { get; set; } // Valor total da venda
    public required string Branch { get; set; } // Filial
    public bool IsCancelled { get; set; } // Status de cancelamento
    public required List<GetSaleItemResponse> Items { get; set; } // Itens da venda
}
