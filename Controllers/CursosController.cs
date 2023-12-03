using Microsoft.AspNetCore.Mvc;
using UniversidadAPI.Iterador;
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
    [Route("RetrieveAll")]
    public async Task<IActionResult> Get()
    {
        var cursos = await _cursosService.GetAsync();
        var cursosCollection = new GenericCollection<Curso>(cursos);
        var iterator = cursosCollection.CreateIterator();

        var cursosList = new List<Curso>();
        while (iterator.HasNext())
        {
            cursosList.Add(iterator.Next());
        }

        return Ok(cursosList);
    }

    [HttpGet]
    [Route("RetrieveById")]
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
    [Route("Create")]
    public async Task<IActionResult> Post(Curso newCurso)
    {
        await _cursosService.CreateAsync(newCurso);

        return CreatedAtAction(nameof(Get), new { id = newCurso.Id }, newCurso);
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> Update(Curso updatedCurso)
    {
        var curso = await _cursosService.GetAsync(updatedCurso.Id);

        if (curso is null)
        {
            return NotFound();
        }

        await _cursosService.UpdateAsync(updatedCurso.Id, updatedCurso);

        return NoContent();
    }

    [HttpDelete]
    [Route("Delete")]
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