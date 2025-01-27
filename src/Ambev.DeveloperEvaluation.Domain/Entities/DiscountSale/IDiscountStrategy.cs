namespace Ambev.DeveloperEvaluation.Domain.Entities.DiscountSale
{
    public interface IDiscountStrategy
    {
        decimal CalculateDiscount(int quantity, decimal unitPrice);
    }
}
