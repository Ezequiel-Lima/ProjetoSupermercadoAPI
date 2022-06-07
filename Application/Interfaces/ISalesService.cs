using ProjetoSupermercado.Models;

namespace ProjetoSupermercado.Application.Interfaces;

public interface ISalesService
{
    Task<Sales> Create(Sales sales);
}