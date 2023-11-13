using Microsoft.AspNetCore.Mvc;
using UniversidadAPI.Iterador;
using UniversidadAPI.Modelos;
using UniversidadAPI.Servicios;

namespace UniversidadAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarrerasController : ControllerBase
{
    private readonly CarrerasService _carrerasService;

    public CarrerasController(CarrerasService carrerasService) =>
        _carrerasService = carrerasService;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var carreras = await _carrerasService.GetAsync();
        var carrerasCollection = new GenericCollection<Carrera>(carreras);
        var iterator = carrerasCollection.CreateIterator();

        var carrerasList = new List<Carrera>();
        while (iterator.HasNext())
        {
            carrerasList.Add(iterator.Next());
        }

        return Ok(carrerasList);
    }

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Carrera>> Get(string id)
    {
        var carrera = await _carrerasService.GetAsync(id);

        if (carrera is null)
        {
            return NotFound();
        }

        return carrera;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Carrera newCarrera)
    {
        await _carrerasService.CreateAsync(newCarrera);

        return CreatedAtAction(nameof(Get), new { id = newCarrera.Id }, newCarrera);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Carrera updatedCarrera)
    {
        var carrera = await _carrerasService.GetAsync(id);

        if (carrera is null)
        {
            return NotFound();
        }

        updatedCarrera.Id = carrera.Id;

        await _carrerasService.UpdateAsync(id, updatedCarrera);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var carrera = await _carrerasService.GetAsync(id);

        if (carrera is null)
        {
            return NotFound();
        }

        await _carrerasService.RemoveAsync(id);

        return NoContent();
    }
}
