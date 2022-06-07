using ProjetoSupermercado.Application.Interfaces;
using ProjetoSupermercado.Infrastructure.Data;
using ProjetoSupermercado.Models;

namespace ProjetoSupermercado.Application.Services;

public class StockService : IStockService
{
    private readonly IWriteRepository<Stock> _writeRepository;
    private readonly IReadRepository<Stock> _readRepository;
    private readonly IReadRepository<Product> _readProductRepository;
    private readonly IUnitOfWork _unitOfWork;

    public StockService(IUnitOfWork unitOfWork, IWriteRepository<Stock> writeRepository, IReadRepository<Stock> readRepository, IReadRepository<Product> readProductRepository)
    {
        _unitOfWork = unitOfWork;
        _writeRepository = writeRepository;
        _readRepository = readRepository;
        _readProductRepository = readProductRepository;
    }

    public async Task<Stock> Create(Stock stock)
    {
        var result = new Stock();

        result.ProductQuantity = stock.ProductQuantity;

        var teste = _readProductRepository.FindByCondition(x => x.Id == stock.ProductId).FirstOrDefault();

        if (teste == null) 
            throw new Exception("Produto não está cadastrado!");

        result.ProductId = teste.Id;

        result.ProductName = teste.Name;

        await _writeRepository.AddAsync(result);

        await _unitOfWork.CommitAsync();

        return result;
    }

    public async Task<Stock> UpdateStock(Sales sales)
    {
        var result = _readRepository.FindByCondition(x => x.ProductId == sales.ProductId).FirstOrDefault();

        result!.ProductQuantity = result.ProductQuantity - sales.ProductQuantitySales;

        if (result!.ProductQuantity < 0)
            throw new Exception();

        _writeRepository.Update(result);

        _unitOfWork.Commit();

        return result;
    }

    public async Task<Stock> UpdateStockName(Product product)
    {
        var result = _readRepository.FindByCondition(x => x.ProductId == product.Id).FirstOrDefault();

        if (result == null)
            return null;

        result!.ProductName = product.Name;

        _writeRepository.Update(result);

        _unitOfWork.Commit();

        return result;
    }

    public async Task<Stock> Put(Stock stock)
    {
        var result = _readRepository.FindByCondition(x => x.Id == stock.Id).FirstOrDefault();

        result!.ProductQuantity = stock.ProductQuantity;

        _writeRepository.Update(result);

        await _unitOfWork.CommitAsync();

        return result;
    }
}