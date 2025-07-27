using InventarioBackend.Models;
using InventarioBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventarioBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _service;

    public ProductsController(ProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<Product>> GetAll()
    {
        return Ok(_service.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Product> Get(int id)
    {
        var product = _service.GetById(id);
        if (product == null)
            return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Product p)
    {
        _service.Insert(p);
        return CreatedAtAction(nameof(Get), new { id = p.Id }, p);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Product p)
    {
        if (id != p.Id)
            return BadRequest();

        var existing = _service.GetById(id);
        if (existing == null)
            return NotFound();

        _service.Update(p);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var existing = _service.GetById(id);
        if (existing == null)
            return NotFound();

        _service.Delete(id);
        return NoContent();
    }
}
