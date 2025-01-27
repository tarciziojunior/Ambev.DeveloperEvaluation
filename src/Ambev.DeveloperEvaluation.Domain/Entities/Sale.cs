using Ambev.DeveloperEvaluation.Domain.Entities.DiscountSale;

namespace Ambev.DeveloperEvaluation.Domain.Entities;
public class Sale
{
    public Guid Id { get;  set; } // Identificador único da venda
    public int? SaleNumber { get;  set; } // Número da venda
    public DateTime? SaleDate { get;  set; } // Data da venda
    public string? Customer { get;  set; } // Cliente
    public decimal? TotalSaleAmount { get;  set; } // Valor total da venda            
    public decimal? TotalSaleAmountDiscount { get; set; } // Total amount of the sale
    public string? Branch { get;  set; } // Filial
    public bool? IsCancelled { get;  set; } // Status de cancelamento
    public List<SaleItem> Items { get;  set; } // Itens da venda

    public Sale()
    {
        Id = Guid.NewGuid();
        SaleNumber = null;
        SaleDate = null;
        Customer = null;
        TotalSaleAmount = null;
        TotalSaleAmountDiscount = null;
        Branch = null;
        IsCancelled = null;
        Items = []; // Inicializa como lista vazia

    }

    public void Cancel()
    {
        IsCancelled = true;
    }
    public void ApplyDiscounts()
    {
        foreach (var item in Items)
        {
            var strategy = DiscountStrategyFactory.GetStrategy(item.Quantity);
            var discount = strategy.CalculateDiscount(item.Quantity, item.UnitPrice);
            item.ApplyDiscount(discount);
        }
    }
    
    public void CalculateTotalAmountDiscount()
    {
        TotalSaleAmountDiscount = Items.Sum(item => item.TotalItemAmountDescount);
    }
}
