using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class CreateSaleRegisteredEvent : INotification
    {
        public Sale Sale { get; }

        public CreateSaleRegisteredEvent(Sale sale)
        {
            this.Sale=sale; 
        }
    }
}
