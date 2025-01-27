using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommand : IRequest<CreateSaleResult>
{
    public int SaleNumber { get; set; } // Número da venda
    public DateTime SaleDate { get; set; } // Data da venda
    public required string Customer { get; set; } // Cliente
    public decimal TotalSaleAmount { get; set; } // Valor total da venda
    public required string Branch { get; set; } // Filial
    public bool IsCancelled { get; set; } // Status de cancelamento
    public required List<CreateSaleItemCommand> Items { get; set; } // Itens da venda
}