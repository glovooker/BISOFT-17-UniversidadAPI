using Microsoft.AspNetCore.Mvc;
using UniversidadAPI.Modelos;
using UniversidadAPI.Servicios;

namespace UniversidadAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlumnosController : ControllerBase
{
    private readonly AlumnosService _alumnosService;

    public AlumnosController(AlumnosService alumnosService) =>
        _alumnosService = alumnosService;

    [HttpGet]
    public async Task<List<Alumno>> Get() =>
        await _alumnosService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Alumno>> Get(string id)
    {
        var alumno = await _alumnosService.GetAsync(id);

        if (alumno is null)
        {
            return NotFound();
        }

        return alumno;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Alumno newAlumno)
    {
        await _alumnosService.CreateAsync(newAlumno);

        return CreatedAtAction(nameof(Get), new { id = newAlumno.Id }, newAlumno);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Alumno updatedAlumno)
    {
        var alumno = await _alumnosService.GetAsync(id);

        if (alumno is null)
        {
            return NotFound();
        }

        updatedAlumno.Id = alumno.Id;

        await _alumnosService.UpdateAsync(id, updatedAlumno);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var alumno = await _alumnosService.GetAsync(id);

        if (alumno is null)
        {
            return NotFound();
        }

        await _alumnosService.RemoveAsync(id);

        return NoContent();
    }
}
