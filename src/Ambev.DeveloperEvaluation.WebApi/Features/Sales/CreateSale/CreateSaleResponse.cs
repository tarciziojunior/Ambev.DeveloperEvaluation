namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleResponse
    {
        public Guid Id { get; set; }        
        public decimal TotalSaleAmountDiscount { get; set; } // Total amount of the sale
    }
}
