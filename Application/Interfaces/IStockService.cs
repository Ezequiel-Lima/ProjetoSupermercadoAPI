using ProjetoSupermercado.Models;

namespace ProjetoSupermercado.Application.Interfaces;

public interface IStockService
{
    Task<Stock> Create(Stock stock);

    Task<Stock> UpdateStock(Sales sales);

    Task<Stock> Put(Stock stock);

    Task<Stock> UpdateStockName(Product product);
}