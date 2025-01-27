using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class DeleteSaleHandlerTests
    {
        private readonly Mock<ISaleRepository> _saleRepositoryMock;
        private readonly DeleteSaleHandler _handler;
        private readonly Mock<IMediator> _mediator;

        public DeleteSaleHandlerTests()
        {
            _saleRepositoryMock = new Mock<ISaleRepository>();
            _mediator = new Mock<IMediator>();
            _handler = new DeleteSaleHandler(_saleRepositoryMock.Object, _mediator.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsSuccessResponse()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var command = new DeleteSaleCommand(saleId);

            _saleRepositoryMock
                .Setup(repo => repo.DeleteAsync(saleId))
                .ReturnsAsync(true);

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(response.Success);
            _saleRepositoryMock.Verify(repo => repo.DeleteAsync(saleId), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidCommand_ThrowsValidationException()
        {
            // Arrange
            var saleId = Guid.Empty; // Invalid ID
            var command = new DeleteSaleCommand(saleId);

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_NonExistentSale_ThrowsKeyNotFoundException()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var command = new DeleteSaleCommand(saleId);

            _saleRepositoryMock
                .Setup(repo => repo.DeleteAsync(saleId))
                .ReturnsAsync(false);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal($"Sale with ID {saleId} not found", exception.Message);
        }
    }
}