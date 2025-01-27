namespace Ambev.DeveloperEvaluation.Domain.Entities.DiscountSale
{
    public class TenPercentDiscountStrategy : IDiscountStrategy
    {
        public decimal CalculateDiscount(int quantity, decimal unitPrice) => unitPrice * quantity * 0.10m;
    }
}
