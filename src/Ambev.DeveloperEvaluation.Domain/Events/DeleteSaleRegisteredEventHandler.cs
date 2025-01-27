using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class DeleteSaleRegisteredEventHandler : INotificationHandler<DeleteSaleRegisteredEvent>
    {
        public async Task Handle(DeleteSaleRegisteredEvent notification, CancellationToken cancellationToken)
        {
            // Lógica para enviar o e-mail de boas-vindas
            Console.WriteLine($" exclude from sale {notification.Sale.SaleNumber}");
            await Task.CompletedTask;
        }
    }
}
