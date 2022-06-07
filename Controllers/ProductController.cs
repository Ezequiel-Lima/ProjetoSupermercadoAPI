using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoSupermercado.Application.Interfaces;
using ProjetoSupermercado.Infrastructure.Data;
using ProjetoSupermercado.Models;

namespace ProjetoSupermercado.Controllers;

//[Authorize(AuthenticationSchemes = "CookieAuthentication")]
[Route("api/[controller]"), ApiController]
public sealed class ProductController : ControllerBase
{
    private readonly IReadRepository<Product> _repository;
    private readonly IWriteRepository<Product> _writeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductService _service;

    public ProductController(IReadRepository<Product> repository, IWriteRepository<Product> writeRepository, IProductService service, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _service = service;
        _writeRepository = writeRepository;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public IActionResult GetProduct()
    {
        return Ok(_repository.FindAll());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProductById(int id)
    {
        var result = _repository.FindByCondition(x => x.Id == id).FirstOrDefault();

        if (result == null)
            return NotFound();

        return result;
    }

    [HttpPost]
    public async Task<Product> CreateProductAsync(Product product)
    {
        var result = await _service.Create(product);
        return result;
    }

    [HttpPut]
    public async Task<Product> UpdateProductAsync(Product product)
    {
        var result = await _service.Put(product);
        return result;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            var result = _repository.FindByCondition(x => x.Id == id);
            _writeRepository.Delete(result.First());
            await _unitOfWork.CommitAsync();

            return Ok();
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
}