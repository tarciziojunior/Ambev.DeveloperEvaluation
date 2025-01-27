namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem
{
    public Guid ProductId { get;  set; } // ID do produto
    public required string Product { get;  set; } // Nome do produto
    public int Quantity { get;  set; } // Quantidade
    public decimal UnitPrice { get;  set; } // Preço unitário
    public decimal Discount { get;  set; } // Desconto
    public decimal TotalItemAmount { get;  set; } // Valor total do item
    public decimal TotalItemAmountDescount { get;  set; } // Valor total do item
    public bool IsCancelled { get;  set; } // Status de cancelamento

    

    // Método para cancelar o item
    public void Cancel()
    {
        IsCancelled = true;
    }

    public void ApplyDiscount(decimal discount)
    {
        if (discount < 0)
            throw new DomainException("Discount cannot be negative.");        
        if(this.Discount != discount)
        {
            throw new DomainException($"Discount not allowed. Allowed value is {discount}");
        }
        Discount = discount;
        TotalItemAmountDescount = (UnitPrice * Quantity) - discount;            
    }
}
