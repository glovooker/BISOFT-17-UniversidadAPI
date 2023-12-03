using Microsoft.AspNetCore.Mvc;
using UniversidadAPI.Iterador;
using UniversidadAPI.Modelos;
using UniversidadAPI.Servicios;

namespace UniversidadAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CiclosController : ControllerBase
{
    private readonly CiclosService _ciclosService;

    public CiclosController(CiclosService ciclosService) =>
        _ciclosService = ciclosService;

    [HttpGet]
    [Route("RetrieveAll")]
    public async Task<IActionResult> Get()
    {
        var ciclos = await _ciclosService.GetAsync();
        var ciclosCollection = new GenericCollection<Ciclo>(ciclos);
        var iterator = ciclosCollection.CreateIterator();

        var ciclosList = new List<Ciclo>();
        while (iterator.HasNext())
        {
            ciclosList.Add(iterator.Next());
        }

        return Ok(ciclosList);
    }

    [HttpGet]
    [Route("RetrieveById")]
    public async Task<ActionResult<Ciclo>> Get(string id)
    {
        var ciclo = await _ciclosService.GetAsync(id);

        if (ciclo is null)
        {
            return NotFound();
        }

        return ciclo;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Post(Ciclo newCiclo)
    {
        await _ciclosService.CreateAsync(newCiclo);

        return CreatedAtAction(nameof(Get), new { id = newCiclo.Id }, newCiclo);
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> Update(Ciclo updatedCiclo)
    {
        var ciclo = await _ciclosService.GetAsync(updatedCiclo.Id);

        if (ciclo is null)
        {
            return NotFound();
        }

        updatedCiclo.Id = ciclo.Id;

        await _ciclosService.UpdateAsync(updatedCiclo.Id, updatedCiclo);

        return NoContent();
    }

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> Delete(string id)
    {
        var ciclo = await _ciclosService.GetAsync(id);

        if (ciclo is null)
        {
            return NotFound();
        }

        await _ciclosService.RemoveAsync(id);

        return NoContent();
    }
}