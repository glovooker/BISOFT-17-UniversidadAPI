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
    [Route("RetrieveAll")]
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

    [HttpGet]
    [Route("RetrieveById")]
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
    [Route("Create")]
    public async Task<IActionResult> Post(Profesor newProfesor)
    {
        await _profesorService.CreateAsync(newProfesor);

        return CreatedAtAction(nameof(Get), new { id = newProfesor.Id }, newProfesor);
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> Update(Profesor updatedProfesor)
    {
        var profesor = await _profesorService.GetAsync(updatedProfesor.Id);

        if (profesor is null)
        {
            return NotFound();
        }

        updatedProfesor.Id = profesor.Id;

        await _profesorService.UpdateAsync(updatedProfesor.Id, updatedProfesor);

        return NoContent();
    }

    [HttpDelete]
    [Route("Delete")]
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
