namespace Ambev.DeveloperEvaluation.Domain.Entities.DiscountSale
{
    public class TwentyPercentDiscountStrategy : IDiscountStrategy
    {
        public decimal CalculateDiscount(int quantity, decimal unitPrice) => unitPrice * quantity * 0.20m;
    }
}
