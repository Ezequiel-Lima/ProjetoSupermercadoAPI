using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoSupermercado.Application.Interfaces;
using ProjetoSupermercado.Infrastructure.Data;
using ProjetoSupermercado.Models;

namespace ProjetoSupermercado.Controllers;

//[Authorize(AuthenticationSchemes = "CookieAuthentication")]
[Route("api/[controller]"), ApiController]
public sealed class StockController : ControllerBase
{
    private readonly IReadRepository<Stock> _repository;
    private readonly IWriteRepository<Stock> _writeRepository;
    private readonly IStockService _service;
    private readonly IUnitOfWork _unitOfWork;

    public StockController(IReadRepository<Stock> repository, IWriteRepository<Stock> writeRepository, IStockService service, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _writeRepository = writeRepository;
        _service = service;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public IActionResult GetStock()
    {
        return Ok(_repository.FindAll());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Stock>> GetStockById(int id)
    {
        var result = _repository.FindByCondition(x => x.Id == id).FirstOrDefault();

        if (result == null)
            return NotFound();

        return result;
    }

    [HttpPost]
    public async Task<Stock> CreateStockAsync(Stock stock)
    {
        var result = await _service.Create(stock);
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

    [HttpPut]
    public async Task<ActionResult<Stock>> UpdateStockAsync(Stock stock)
    {
        try
        {
            var result = await _service.Put(stock);
            return result;
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
}