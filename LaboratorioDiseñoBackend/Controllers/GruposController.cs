using Microsoft.AspNetCore.Mvc;
using UniversidadAPI.Iterador;
using UniversidadAPI.Modelos;
using UniversidadAPI.Servicios;

namespace UniversidadAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GruposController : ControllerBase
{
    private readonly GruposService _gruposService;

    public GruposController(GruposService gruposService) =>
        _gruposService = gruposService;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var grupos = await _gruposService.GetAsync();
        var gruposCollection = new GenericCollection<Grupo>(grupos);
        var iterator = gruposCollection.CreateIterator();

        var gruposList = new List<Grupo>();
        while (iterator.HasNext())
        {
            gruposList.Add(iterator.Next());
        }

        return Ok(gruposList);
    }

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Grupo>> Get(string id)
    {
        var grupo = await _gruposService.GetAsync(id);

        if (grupo is null)
        {
            return NotFound();
        }

        return grupo;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Grupo newGrupo)
    {
        await _gruposService.CreateAsync(newGrupo);

        return CreatedAtAction(nameof(Get), new { id = newGrupo.Id }, newGrupo);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Grupo updatedGrupo)
    {
        var grupo = await _gruposService.GetAsync(id);

        if (grupo is null)
        {
            return NotFound();
        }

        updatedGrupo.Id = grupo.Id;

        await _gruposService.UpdateAsync(id, updatedGrupo);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var grupo = await _gruposService.GetAsync(id);

        if (grupo is null)
        {
            return NotFound();
        }

        await _gruposService.RemoveAsync(grupo.Id);

        return NoContent();
    }
}
