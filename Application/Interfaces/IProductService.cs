using ProjetoSupermercado.Models;

namespace ProjetoSupermercado.Application.Interfaces;

public interface IProductService
{
    Task<Product> Create(Product product);

    Task<Product> Put(Product product);
}