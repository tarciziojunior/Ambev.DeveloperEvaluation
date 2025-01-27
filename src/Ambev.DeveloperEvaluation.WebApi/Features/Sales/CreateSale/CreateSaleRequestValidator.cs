using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;


public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{

    public CreateSaleRequestValidator()
    {
        // Validações para CreateSaleRequest
        RuleFor(x => x.SaleNumber)
            .GreaterThan(0); // Mensagem padrão: "'Sale Number' must be greater than '0'."        

        RuleFor(x => x.Customer)
            .NotEmpty() // Mensagem padrão: "'Customer' must not be empty."
            .MaximumLength(100); // Mensagem padrão: "The length of 'Customer' must be 100 characters or fewer."

        RuleFor(x => x.TotalSaleAmount)
            .GreaterThanOrEqualTo(0); // Mensagem padrão: "'Total Sale Amount' must be greater than or equal to '0'."

        RuleFor(x => x.Branch)
            .NotEmpty() // Mensagem padrão: "'Branch' must not be empty."
            .MaximumLength(50); // Mensagem padrão: "The length of 'Branch' must be 50 characters or fewer."

        RuleFor(x => x.Items)
            .NotEmpty() // Mensagem padrão: "'Items' must not be empty."
            .Must(items => items != null && items.Count > 0); // Mensagem padrão: "The specified condition was not met for 'Items'."

        // Validações para cada item em CreateSaleItemRequest
        RuleForEach(x => x.Items).ChildRules(item =>
        {
            item.RuleFor(i => i.ProductId)
                .NotEmpty(); // Mensagem padrão: "'Product Id' must not be empty."

            item.RuleFor(i => i.Product)
                .NotEmpty() // Mensagem padrão: "'Product' must not be empty."
                .MaximumLength(100); // Mensagem padrão: "The length of 'Product' must be 100 characters or fewer."

            item.RuleFor(i => i.Quantity)
                .GreaterThan(0); // Mensagem padrão: "'Quantity' must be greater than '0'."

            item.RuleFor(i => i.UnitPrice)
                .GreaterThanOrEqualTo(0); // Mensagem padrão: "'Unit Price' must be greater than or equal to '0'."

            //item.RuleFor(i => i.Discount)
            //    .GreaterThanOrEqualTo(0) // Mensagem padrão: "'Discount' must be greater than or equal to '0'."
            //    .LessThanOrEqualTo(i => i.UnitPrice * i.Quantity); // Mensagem padrão: "'Discount' must be less than or equal to 'calculated value'."

            //item.RuleFor(i => i.TotalItemAmount)
            //    .GreaterThanOrEqualTo(0) // Mensagem padrão: "'Total Item Amount' must be greater than or equal to '0'."
            //    .Must((i, totalItemAmount) => totalItemAmount == (i.UnitPrice * i.Quantity - i.Discount)); // Mensagem padrão: "The specified condition was not met for 'Total Item Amount'."
        });
    }
}