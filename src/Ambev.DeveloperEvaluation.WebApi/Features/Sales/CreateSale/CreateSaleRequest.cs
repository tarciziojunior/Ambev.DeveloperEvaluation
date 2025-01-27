namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequest
    {
        public CreateSaleRequest() { }


        // Properties of the sale
        public int? SaleNumber { get; set; } // Sale number
        public DateTime? SaleDate { get; set; } // Date when the sale was made
        public string? Customer { get; set; } // Customer
        public decimal? TotalSaleAmount { get; set; } // Total amount of the sale
        public string? Branch { get; set; } // Branch where the sale was made
        public bool? IsCancelled { get; set; } // Cancellation status (Cancelled/Not Cancelled)

        // List of sale items
        public required List<CreateSaleItemRequest>? Items { get; set; } // Products, quantities, unit prices, discounts, etc.

       
    }
}
