namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleItemRequest
    {
        public Guid ProductId { get; set; }
        public required string Product { get; set; } // Product name
        public int Quantity { get; set; } // Quantity of the product
        public decimal UnitPrice { get; set; } // Unit price of the product
        public decimal Discount { get; set; } // Discount applied to the item
        public decimal TotalItemAmount { get; set; } // Total amount of the item (calculated)

        public bool IsCancelled { get; set; }
    }
}
