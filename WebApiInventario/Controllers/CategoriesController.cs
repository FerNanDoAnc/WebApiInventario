using Microsoft.AspNetCore.Mvc;
using InventarioBackend.Services;
using InventarioBackend.Models;

namespace InventarioBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly CategoryService _service;

    public CategoriesController(CategoryService service)
    {
        _service = service;
    }

    [HttpGet("GetAllCategory")]
    public ActionResult<List<Category>> GetAllCategory()
    {
        return _service.GetAll();
    }

    [HttpGet("GetCategory/{id}")]
    public ActionResult<Category> GetCategory(int id)
    {
        var category = _service.GetById(id);
        if (category == null)
            return NotFound();
        return Ok(category);
    }

    [HttpPost("InsertCategory")]
    public IActionResult InsertCategory([FromBody] Category category)
    {
        if (category == null || string.IsNullOrWhiteSpace(category.Name))
            return BadRequest("Invalid category data.");
        _service.Insert(category);
        return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
    }

    [HttpPut("UpdateCategory/{id}")]
    public IActionResult UpdateCategory(int id, [FromBody] Category category)
    {
        if (id != category.Id)
        {
            return BadRequest(new { exito = false, mensaje = "El ID proporcionado no coincide con la Categoría." });
        }

        var existing = _service.GetById(id);
        if (existing == null)
        {
            return NotFound(new { exito = false, mensaje = "Categoría no encontrada." });
        }

        _service.Update(category);
        return Ok(new { exito = true, mensaje = "Categoría actualizada correctamente." });
    }

    [HttpDelete("DeleteCategory/{id}")]
    public IActionResult DeleteCategory(int id)
    {
        var existing = _service.GetById(id);
        if (existing == null)
        {
            return NotFound(new { exito = false, mensaje = "Categoría no encontrada." });
        }

        _service.Delete(id);
        return Ok(new { exito = true, mensaje = "Categoría eliminada correctamente." });
    }

}