using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class CreateSaleRegisteredEventHandler : INotificationHandler<CreateSaleRegisteredEvent>
    {
        public async Task Handle(CreateSaleRegisteredEvent notification, CancellationToken cancellationToken)
        {
            // Lógica para enviar o e-mail de boas-vindas
            Console.WriteLine($"Sale create {notification.Sale.SaleNumber}");
            await Task.CompletedTask;
        }
    }
}
