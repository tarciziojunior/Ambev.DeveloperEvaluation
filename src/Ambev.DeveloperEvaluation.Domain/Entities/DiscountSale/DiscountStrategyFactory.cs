namespace Ambev.DeveloperEvaluation.Domain.Entities.DiscountSale;

public static class DiscountStrategyFactory
{        
    public static IDiscountStrategy GetStrategy(int quantity)
    {
        return quantity switch
        {
            < 4 => new NoDiscountStrategy(),
            >= 4 and < 10 => new TenPercentDiscountStrategy(), // Faixa ajustada
            >= 10 and <= 20 => new TwentyPercentDiscountStrategy(), // Máximo ajustado para 15
            _ => throw new DomainException("Cannot sell more than 15 identical items.")
        };
    }
}
