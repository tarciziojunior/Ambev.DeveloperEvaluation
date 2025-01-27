using Xunit;
using Ambev.DeveloperEvaluation.Domain.Entities.DiscountSale;
using System;

namespace Ambev.DeveloperEvaluation.Tests.Domain.Entities.DiscountSale
{
    public class DiscountStrategyFactoryTests
    {
        [Theory]
        [InlineData(3, typeof(NoDiscountStrategy))]
        [InlineData(4, typeof(TenPercentDiscountStrategy))]
        [InlineData(9, typeof(TenPercentDiscountStrategy))]
        [InlineData(10, typeof(TwentyPercentDiscountStrategy))]
        [InlineData(15, typeof(TwentyPercentDiscountStrategy))]
        public void GetStrategy_ShouldReturnCorrectStrategy(int quantity, Type expectedStrategyType)
        {
            // Act
            var strategy = DiscountStrategyFactory.GetStrategy(quantity);

            // Assert
            Assert.IsType(expectedStrategyType, strategy);
        }

        [Fact]
        public void GetStrategy_ShouldThrowException_WhenQuantityIsGreaterThan20()
        {
            // Arrange
            int quantity = 21;

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() => DiscountStrategyFactory.GetStrategy(quantity));
            Assert.Equal("Cannot sell more than 15 identical items.", exception.Message);
        }
    }

    public class TenPercentDiscountStrategyTests
    {
        [Fact]
        public void CalculateDiscount_ShouldReturnCorrectDiscount()
        {
            // Arrange
            var strategy = new TenPercentDiscountStrategy();
            int quantity = 5;
            decimal unitPrice = 10.0m;
            decimal expectedDiscount = 5.0m; // 10 * 5 * 0.10 = 5.0

            // Act
            var discount = strategy.CalculateDiscount(quantity, unitPrice);

            // Assert
            Assert.Equal(expectedDiscount, discount);
        }
    }

    public class TwentyPercentDiscountStrategyTests
    {
        [Fact]
        public void CalculateDiscount_ShouldReturnCorrectDiscount()
        {
            // Arrange
            var strategy = new TwentyPercentDiscountStrategy();
            int quantity = 10;
            decimal unitPrice = 10.0m;
            decimal expectedDiscount = 20.0m; // 10 * 10 * 0.20 = 20.0

            // Act
            var discount = strategy.CalculateDiscount(quantity, unitPrice);

            // Assert
            Assert.Equal(expectedDiscount, discount);
        }
    }

    public class NoDiscountStrategyTests
    {
        [Fact]
        public void CalculateDiscount_ShouldReturnZero()
        {
            // Arrange
            var strategy = new NoDiscountStrategy();
            int quantity = 2;
            decimal unitPrice = 10.0m;
            decimal expectedDiscount = 0.0m;

            // Act
            var discount = strategy.CalculateDiscount(quantity, unitPrice);

            // Assert
            Assert.Equal(expectedDiscount, discount);
        }
    }
}