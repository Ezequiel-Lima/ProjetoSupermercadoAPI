using ProjetoSupermercado.Application.Interfaces;
using ProjetoSupermercado.Infrastructure.Data;
using ProjetoSupermercado.Models;

namespace ProjetoSupermercado.Application.Services;

public class ProductService : IProductService
{
    private readonly IWriteRepository<Product> _writeRepository;
    private readonly IReadRepository<Product> _readRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStockService _stockService;

    public ProductService(IUnitOfWork unitOfWork, IWriteRepository<Product> writeRepository, IReadRepository<Product> readRepository, IStockService stockService)
    {
        _unitOfWork = unitOfWork;
        _writeRepository = writeRepository;
        _readRepository = readRepository;
        _stockService = stockService;
    }

    public async Task<Product> Create(Product product)
    {
        var result = new Product();

        result.Name = product.Name;
        result.Price = product.Price;

        await _writeRepository.AddAsync(result);

        await _unitOfWork.CommitAsync();

        return result;
    }

    public async Task<Product> Put(Product product)
    {
        var result = _readRepository.FindByCondition(x => x.Id == product.Id).FirstOrDefault();

        result!.Name = product.Name;
        result.Price = product.Price;

         _writeRepository.Update(result);

        await _stockService.UpdateStockName(result);

        await _unitOfWork.CommitAsync();

        return result;
    }
}