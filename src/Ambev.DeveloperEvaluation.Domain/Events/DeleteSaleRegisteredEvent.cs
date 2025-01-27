using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class DeleteSaleRegisteredEvent : INotification
    {
        public Sale Sale { get; }

        public DeleteSaleRegisteredEvent(Sale sale)
        {
            this.Sale=sale; 
        }
    }
}
