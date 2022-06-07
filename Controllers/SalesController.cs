using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoSupermercado.Application.Interfaces;
using ProjetoSupermercado.Infrastructure.Data;
using ProjetoSupermercado.Models;

namespace ProjetoSupermercado.Controllers;

//[Authorize(AuthenticationSchemes = "CookieAuthentication")]
[Route("api/[controller]"), ApiController]
public sealed class SalesController : ControllerBase
{
    private readonly IReadRepository<Sales> _repository;
    private readonly ISalesService _service;

    public SalesController(IReadRepository<Sales> repository, ISalesService service)
    {
        _repository = repository;
        _service = service;
    }

    [HttpGet]
    public IActionResult GetSales()
    {
        return Ok(_repository.FindAll());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Sales>> GetSalesById(int id)
    {
        var result =  _repository.FindByCondition(x => x.Id == id).FirstOrDefault();

        if(result == null)
            return NotFound();

        return result;
    }

    [HttpPost]
    public async Task<ActionResult<Sales>> CreateSalesAsync(Sales sales)
    {
        try
        {
            var result = await _service.Create(sales);
            return result;
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
}