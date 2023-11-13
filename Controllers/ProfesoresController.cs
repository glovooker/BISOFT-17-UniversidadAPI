using Microsoft.AspNetCore.Mvc;
using UniversidadAPI.Iterador;
using UniversidadAPI.Modelos;
using UniversidadAPI.Servicios;

namespace UniversidadAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfesoresController : ControllerBase
{
    private readonly ProfesoresService _profesorService;

    public ProfesoresController(ProfesoresService profesorService) =>
        _profesorService = profesorService;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var profesores = await _profesorService.GetAsync();
        var profesoresCollection = new GenericCollection<Profesor>(profesores);
        var iterator = profesoresCollection.CreateIterator();

        var profesoresList = new List<Profesor>();
        while (iterator.HasNext())
        {
            profesoresList.Add(iterator.Next());
        }

        return Ok(profesoresList);
    }

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Profesor>> Get(string id)
    {
        var profesor = await _profesorService.GetAsync(id);

        if (profesor is null)
        {
            return NotFound();
        }

        return profesor;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Profesor newProfesor)
    {
        await _profesorService.CreateAsync(newProfesor);

        return CreatedAtAction(nameof(Get), new { id = newProfesor.Id }, newProfesor);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Profesor updatedProfesor)
    {
        var profesor = await _profesorService.GetAsync(id);

        if (profesor is null)
        {
            return NotFound();
        }

        updatedProfesor.Id = profesor.Id;

        await _profesorService.UpdateAsync(id, updatedProfesor);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var profesor = await _profesorService.GetAsync(id);

        if (profesor is null)
        {
            return NotFound();
        }

        await _profesorService.RemoveAsync(id);

        return NoContent();
    }
}
