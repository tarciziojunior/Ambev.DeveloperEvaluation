namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Response model for GetSale operation
/// </summary>
public class GetSaleResult
{
    public int SaleNumber { get; set; } // Número da venda
    public DateTime SaleDate { get; set; } // Data da venda
    public string? Customer { get; set; } // Cliente
    public decimal TotalSaleAmount { get; set; } // Valor total da venda
    public string? Branch { get; set; } // Filial
    public bool IsCancelled { get; set; } // Status de cancelamento
    public List<GetSaleItemResult>? Items { get; set; } // Itens da venda
}
