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

    [HttpGet]
    public ActionResult<List<Category>> GetAll()
    {
        return _service.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Category> Get(int id)
    {
        var category = _service.GetById(id);
        if (category == null)
            return NotFound();
        return Ok(category);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Category category)
    {
        if (category == null || string.IsNullOrWhiteSpace(category.Name))
            return BadRequest("Invalid category data.");
        _service.Insert(category);
        return CreatedAtAction(nameof(Get), new { id = category.Id }, category);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Category category)
    {
        if (id != category.Id)
            return BadRequest("Category ID mismatch.");
        
        var existing = _service.GetById(id);
        if (existing == null)
            return NotFound();
        _service.Update(category);
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