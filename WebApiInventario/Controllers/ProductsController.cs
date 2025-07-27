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

    [HttpGet("GetAllProducts")]
    public ActionResult<List<Product>> GetAllProducts()
    {
        return Ok(_service.GetAll());
    }

    [HttpGet("GetProduct/{id}")]
    public ActionResult<Product> GetProduct(int id)
    {
        var product = _service.GetById(id);
        if (product == null)
            return NotFound();
        return Ok(product);
    }

    [HttpPost("InsertProduct")]
    public IActionResult InsertProduct([FromBody] Product p)
    {
        _service.Insert(p);
        return CreatedAtAction(nameof(GetProduct), new { id = p.Id }, p);
    }

    [HttpPut("UpdateProduct/{id}")]
    public IActionResult UpdateProduct(int id, [FromBody] Product p)
    {
        if (id != p.Id)
        {
            return BadRequest(new { exito = false, mensaje = "El ID proporcionado no coincide con el producto." });
        }

        var existing = _service.GetById(id);
        if (existing == null)
        {
            return NotFound(new { exito = false, mensaje = "Producto no encontrado." });
        }

        _service.Update(p);
        return Ok(new { exito = true, mensaje = "Producto actualizado correctamente." });
    }

    [HttpDelete("DeleteProduct/{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var existing = _service.GetById(id);
        if (existing == null)
        {
            return NotFound(new { exito = false, mensaje = "Producto no encontrado." });
        }

        _service.Delete(id);
        return Ok(new { exito = true, mensaje = "Producto eliminado correctamente." });
    }
}
