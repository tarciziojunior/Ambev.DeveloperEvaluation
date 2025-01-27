using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class CreateSaleHandlerTests
    {
        private readonly Mock<ISaleRepository> _saleRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IMediator> _mediator;
        private readonly CreateSaleHandler _handler;

        public CreateSaleHandlerTests()
        {
            _saleRepositoryMock = new Mock<ISaleRepository>();
            _mapperMock = new Mock<IMapper>();
            _mediator = new Mock<IMediator>();
            _handler = new CreateSaleHandler(_saleRepositoryMock.Object, _mapperMock.Object,_mediator.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsCreateSaleResult()
        {
            // Arrange
            var command = new CreateSaleCommand
            {
                SaleNumber = 1,
                SaleDate = DateTime.UtcNow,
                Customer = "John Doe",
                TotalSaleAmount = 1000,
                Branch = "Main Branch",
                IsCancelled = false,
                Items = new List<CreateSaleItemCommand>
                {
                    new CreateSaleItemCommand
                    {
                        ProductId = Guid.NewGuid(),
                        Product = "Product A",
                        Quantity = 2,
                        UnitPrice = 500,
                        Discount = 100,
                        TotalItemAmount = 900,
                        IsCancelled = false
                    }
                }
            };

            var sale = new Sale();
            var createSaleResult = new CreateSaleResult { Id = Guid.NewGuid(), TotalSaleAmountDiscount = 900 };

            _mapperMock.Setup(m => m.Map<Sale>(command)).Returns(sale);
            _saleRepositoryMock.Setup(r => r.AddAsync(sale)).ReturnsAsync(sale);
            _mapperMock.Setup(m => m.Map<CreateSaleResult>(sale)).Returns(createSaleResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(createSaleResult.Id, result.Id);
            Assert.Equal(createSaleResult.TotalSaleAmountDiscount, result.TotalSaleAmountDiscount);

            _mapperMock.Verify(m => m.Map<Sale>(command), Times.Once);
            _saleRepositoryMock.Verify(r => r.AddAsync(sale), Times.Once);
            _mapperMock.Verify(m => m.Map<CreateSaleResult>(sale), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidCommand_ThrowsValidationException()
        {
            // Arrange
            var command = new CreateSaleCommand
            {
                SaleNumber = 1,
                SaleDate = DateTime.UtcNow,
                Customer = "", // Invalid: Customer is required
                TotalSaleAmount = 1000,
                Branch = "Main Branch",
                IsCancelled = false,
                Items = new List<CreateSaleItemCommand>()
            };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}