using Microsoft.AspNetCore.Mvc;
using UniversidadAPI.Modelos;
using UniversidadAPI.Servicios;

namespace UniversidadAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CursosController : ControllerBase
{
    private readonly CursosService _cursosService;

    public CursosController(CursosService cursosService) =>
        _cursosService = cursosService;

    [HttpGet]
    public async Task<List<Curso>> Get() =>
        await _cursosService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Curso>> Get(string id)
    {
        var curso = await _cursosService.GetAsync(id);

        if (curso is null)
        {
            return NotFound();
        }

        return curso;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Curso newCurso)
    {
        await _cursosService.CreateAsync(newCurso);

        return CreatedAtAction(nameof(Get), new { id = newCurso.Id }, newCurso);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Curso updatedCurso)
    {
        var curso = await _cursosService.GetAsync(id);

        if (curso is null)
        {
            return NotFound();
        }

        updatedCurso.Id = curso.Id;

        await _cursosService.UpdateAsync(id, updatedCurso);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var curso = await _cursosService.GetAsync(id);

        if (curso is null)
        {
            return NotFound();
        }

        await _cursosService.RemoveAsync(id);

        return NoContent();
    }
}