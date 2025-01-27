using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

/// <summary>
/// Handler for processing DeleteSaleCommand requests
/// </summary>
public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResponse>
{
    private readonly ISaleRepository _SaleRepository;
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of DeleteSaleHandler
    /// </summary>
    /// <param name="SaleRepository">The Sale repository</param>
    /// <param name="validator">The validator for DeleteSaleCommand</param>
    public DeleteSaleHandler(
        ISaleRepository SaleRepository, IMediator mediator)
    {
        _SaleRepository = SaleRepository;
        _mediator = mediator;
    }

    /// <summary>
    /// Handles the DeleteSaleCommand request
    /// </summary>
    /// <param name="request">The DeleteSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the delete operation</returns>
    public async Task<DeleteSaleResponse> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteSaleValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        var sale = await _SaleRepository.GetByIdAsync(request.Id);
        var success = await _SaleRepository.DeleteAsync(request.Id);
        if (!success)
            throw new KeyNotFoundException($"Sale with ID {request.Id} not found");
        // Publicar o evento de crição
        await _mediator.Publish(new DeleteSaleRegisteredEvent(sale), cancellationToken);
        return new DeleteSaleResponse { Success = true };
    }
}
