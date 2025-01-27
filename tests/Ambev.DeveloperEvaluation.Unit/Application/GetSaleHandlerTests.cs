using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class GetSaleHandlerTests
    {
        private readonly Mock<ISaleRepository> _saleRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetSaleHandler _handler;

        public GetSaleHandlerTests()
        {
            _saleRepositoryMock = new Mock<ISaleRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetSaleHandler(_saleRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsSaleResult()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var command = new GetSaleCommand(saleId);

            var sale = new Sale
            {
                Id = saleId,
                SaleNumber = 123,
                SaleDate = DateTime.UtcNow,
                Customer = "John Doe",
                TotalSaleAmount = 100.0m,
                Branch = "Main Branch",
                IsCancelled = false
            };

            var expectedResult = new GetSaleResult
            {
                SaleNumber = sale.SaleNumber.Value,
                SaleDate = sale.SaleDate.Value,
                Customer = sale.Customer,
                TotalSaleAmount = sale.TotalSaleAmount.Value,
                Branch = sale.Branch,
                IsCancelled = sale.IsCancelled.Value
            };

            _saleRepositoryMock.Setup(repo => repo.GetByIdAsync(saleId))
                .ReturnsAsync(sale);

            _mapperMock.Setup(mapper => mapper.Map<GetSaleResult>(sale))
                .Returns(expectedResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResult.SaleNumber, result.SaleNumber);
            Assert.Equal(expectedResult.SaleDate, result.SaleDate);
            Assert.Equal(expectedResult.Customer, result.Customer);
            Assert.Equal(expectedResult.TotalSaleAmount, result.TotalSaleAmount);
            Assert.Equal(expectedResult.Branch, result.Branch);
            Assert.Equal(expectedResult.IsCancelled, result.IsCancelled);

            _saleRepositoryMock.Verify(repo => repo.GetByIdAsync(saleId), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<GetSaleResult>(sale), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidCommand_ThrowsValidationException()
        {
            // Arrange
            var command = new GetSaleCommand(Guid.Empty);

            var validator = new GetSaleValidator();
            var validationResult = await validator.ValidateAsync(command);

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_SaleNotFound_ThrowsKeyNotFoundException()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var command = new GetSaleCommand(saleId);

            _saleRepositoryMock.Setup(repo => repo.GetByIdAsync(saleId))
                .ReturnsAsync((Sale)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}