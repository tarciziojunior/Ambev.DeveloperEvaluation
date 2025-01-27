namespace Ambev.DeveloperEvaluation.Domain.Entities.DiscountSale
{
    public class NoDiscountStrategy : IDiscountStrategy
    {
        public decimal CalculateDiscount(int quantity, decimal unitPrice) => 0m;
    }
}
