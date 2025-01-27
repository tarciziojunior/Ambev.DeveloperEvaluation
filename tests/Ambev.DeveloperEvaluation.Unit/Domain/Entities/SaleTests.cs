using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Entities.DiscountSale;
using Xunit;

namespace Ambev.DeveloperEvaluation.Tests.Domain.Entities
{
    public class SaleTests
    {
        [Fact]
        public void Cancel_ShouldSetIsCancelledToTrue()
        {
            // Arrange
            var sale = new Sale();

            // Act
            sale.Cancel();

            // Assert
            Assert.True(sale.IsCancelled);
        }

        [Fact]
        public void ApplyDiscounts_ShouldApplyDiscountToEachItem()
        {
            // Arrange
            var sale = new Sale
            {
                Items = new List<SaleItem>
                {
                    new SaleItem { ProductId = Guid.NewGuid(), Product = "Product A", Quantity = 5, UnitPrice = 10.0m , Discount = 5},
                    new SaleItem { ProductId = Guid.NewGuid(), Product = "Product B", Quantity = 3, UnitPrice = 20.0m , Discount = 0 }
                }
            };

            // Act
            sale.ApplyDiscounts();

            // Assert
            foreach (var item in sale.Items)
            {
                Assert.True(item.Discount >= 0);
                Assert.Equal((item.UnitPrice * item.Quantity) - item.Discount, item.TotalItemAmountDescount);
            }
        }

        [Fact]
        public void CalculateTotalAmountDiscount_ShouldSumAllItemDiscounts()
        {
            // Arrange
            var sale = new Sale
            {
                Items = new List<SaleItem>
                {
                    new SaleItem { ProductId = Guid.NewGuid(), Product = "Product A", Quantity = 2, UnitPrice = 10.0m, Discount = 5.0m },
                    new SaleItem { ProductId = Guid.NewGuid(), Product = "Product B", Quantity = 3, UnitPrice = 20.0m, Discount = 10.0m }
                }
            };

            // Act
            sale.CalculateTotalAmountDiscount();

            // Assert
            var expectedTotalDiscount = sale.Items.Sum(item => item.TotalItemAmountDescount);
            Assert.Equal(expectedTotalDiscount, sale.TotalSaleAmountDiscount);
        }

        
    }
}