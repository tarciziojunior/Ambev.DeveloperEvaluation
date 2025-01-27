using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class InMemorySaleRepository: ISaleRepository
    {
        // Dicionário thread-safe para armazenar as vendas em memória
        private static readonly ConcurrentDictionary<Guid, Sale> _sales = new();

        public InMemorySaleRepository()
        {
        }

        public Task<Sale> GetByIdAsync(Guid id)
        {
            _sales.TryGetValue(id, out var sale);
            return Task.FromResult(sale);
        }

        public Task<List<Sale>> GetAllAsync()
        {
            return Task.FromResult(_sales.Values.ToList());
        }

        public Task<Sale> AddAsync(Sale sale)
        {            
            _sales.TryAdd(sale.Id, sale);
            return Task.FromResult(sale);
        }

        public Task<Sale> UpdateAsync(Sale sale)
        {
            if (_sales.ContainsKey(sale.Id))
            {
                _sales[sale.Id] = sale;
                return Task.FromResult(sale);
            }

            return Task.FromResult<Sale>(null);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            return Task.FromResult(_sales.TryRemove(id, out _));
        }
    }
}
