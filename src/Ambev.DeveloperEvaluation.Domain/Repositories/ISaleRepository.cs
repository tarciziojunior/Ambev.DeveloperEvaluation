using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleRepository
    {
        Task<Sale> GetByIdAsync(Guid id); // Obtém uma venda por ID
        Task<List<Sale>> GetAllAsync(); // Obtém todas as vendas
        Task<Sale> AddAsync(Sale sale); // Adiciona uma nova venda
        Task<Sale> UpdateAsync(Sale sale); // Atualiza uma venda existente
        Task<bool> DeleteAsync(Guid id); // Remove uma venda por ID
    }
}
