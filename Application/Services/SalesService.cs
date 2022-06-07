using ProjetoSupermercado.Application.Interfaces;
using ProjetoSupermercado.Infrastructure.Data;
using ProjetoSupermercado.Models;

namespace ProjetoSupermercado.Application.Services;

public class SalesService : ISalesService
{
    private readonly IWriteRepository<Sales> _writeRepository;
    private readonly IReadRepository<Product> _readProductRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStockService _stockService;

    public SalesService(IUnitOfWork unitOfWork, IWriteRepository<Sales> writeRepository, IStockService stockService, IReadRepository<Product> readProductRepository)
    {
        _unitOfWork = unitOfWork;
        _writeRepository = writeRepository;
        _stockService = stockService;
        _readProductRepository = readProductRepository;
    }

    public async Task<Sales> Create(Sales sales)
    {
        var result = new Sales();

        var cambiarra = _readProductRepository.FindByCondition(x => x.Id == sales.ProductId).First();

        result.ProductId = sales.ProductId;
        result.ProductName = cambiarra.Name;
        result.SaleDate = DateTime.Now;
        result.ProductQuantitySales = sales.ProductQuantitySales;

        await _stockService.UpdateStock(result);

        await _writeRepository.AddAsync(result);

        await _unitOfWork.CommitAsync();

        return result;
    }
}