using Xunit;
using Ambev.DeveloperEvaluation.Domain.Entities;
using System;

namespace Ambev.DeveloperEvaluation.Tests.Domain.Entities
{
    public class SaleItemTests
    {
        [Fact]
        public void Cancel_ShouldSetIsCancelledToTrue()
        {
            // Arrange
            var saleItem = new SaleItem
            {
                ProductId = Guid.NewGuid(),
                Product = "Test Product",
                Quantity = 2,
                UnitPrice = 10.0m,
                Discount = 0.0m,
                TotalItemAmount = 20.0m,
                TotalItemAmountDescount = 20.0m,
                IsCancelled = false
            };

            // Act
            saleItem.Cancel();

            // Assert
            Assert.True(saleItem.IsCancelled);
        }        

        [Fact]
        public void ApplyDiscount_ShouldThrowException_WhenDiscountIsNegative()
        {
            // Arrange
            var saleItem = new SaleItem
            {
                ProductId = Guid.NewGuid(),
                Product = "Test Product",
                Quantity = 2,
                UnitPrice = 10.0m,
                Discount = 0.0m,
                TotalItemAmount = 20.0m,
                TotalItemAmountDescount = 20.0m,
                IsCancelled = false
            };
            decimal discount = -1.0m;

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() => saleItem.ApplyDiscount(discount));
            Assert.Equal("Discount cannot be negative.", exception.Message);
        }

        [Fact]
        public void ApplyDiscount_ShouldThrowException_WhenDiscountIsNotAllowed()
        {
            // Arrange
            var saleItem = new SaleItem
            {
                ProductId = Guid.NewGuid(),
                Product = "Test Product",
                Quantity = 2,
                UnitPrice = 10.0m,
                Discount = 0.0m,
                TotalItemAmount = 20.0m,
                TotalItemAmountDescount = 20.0m,
                IsCancelled = false
            };
            decimal discount = 5.0m;

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() => saleItem.ApplyDiscount(discount));
            Assert.Equal($"Discount not allowed. Allowed value is {discount}", exception.Message);
        }
    }
}